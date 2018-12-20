using HomeFinanse.Areas.Categories.Models;
using HomeFinanse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeFinanse.Areas.Categories.Controllers
{
    public class CategoryController : Controller
    {
        private HomeBudgetDBEntities context;

        public CategoryController(HomeBudgetDBEntities context)
        {
            this.context = context;
        }

        public ActionResult AddCategory()
        {
            return PartialView("AddCategory", new CategoryNotNullable());
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryNotNullable newCategory)
        {
            try
            {
                if (this.context != null)
                {
                    if (this.context.Categories != null)
                    {
                        if (!(this.CategoryNameAlreadyExists(newCategory.CategoryName)))
                        {
                            this.context.Categories.Add(new Category
                            {
                                CategoryName = newCategory.CategoryName,
                                NFLAG = newCategory.NFLAGNotNullable
                            });

                            this.context.SaveChanges();

                            this.ViewData["CategoryAdded"] = "Category successfully added.";
                        }
                        else
                        {
                            this.ModelState.AddModelError("CategoryExists", "This category already exists.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("CategoryAddingError", 
                    string.Format("An exception occured while adding category. \n Exception : {0}", ex.Message));
            }

            return PartialView("CategoriesTable", this.context.Categories.ToList());
        }

        private bool CategoryNameAlreadyExists(string categoryName)
        {
            var cat = this.context.Categories.ToList().Where(x => x.CategoryName == categoryName).SingleOrDefault();

            if (cat != null)
            {
                return true;
            }

            return false;
        }

        [HttpGet]
        public ActionResult ShowCategories()
        {
            return PartialView(this.context.Categories.ToList());
        }
        
        [HttpDelete]
        public ActionResult DeleteCategory(int categoryID)
        {
            if (this.context != null)
            {
                Category categoryToDelete = this.context.Categories.Where(cat => cat.CategoryID == categoryID).SingleOrDefault();

                if (categoryToDelete != null)
                {
                    // delete category from database
                    this.context.Categories.Remove(categoryToDelete);
                    this.context.SaveChanges();
                }
            }
            else
            {
                this.ModelState.AddModelError("", "No database data loaded.");
            }

            return PartialView("CategoriesTable", this.context.Categories);
        }

        public ActionResult EditCategory(int categoryID)
        {
            Category cat = new Category();

            if (this.context != null)
            {
                cat = this.context.Categories.Where(X => X.CategoryID == categoryID).SingleOrDefault();
            }

            return PartialView(cat);
        }

        public ActionResult GetCategoryDetailsForModal(int categoryID)
        {
            Category categoryDetails = new Category();

            if (this.context != null)
            {
                if (this.context.Categories != null)
                {
                    categoryDetails = this.context.Categories.Where(cat => cat.CategoryID == categoryID).SingleOrDefault();
                }
            }

            return PartialView("EditCategory", categoryDetails);
        }

        [HttpPost]
        public ActionResult SaveNewCategory(Category newCategory)
        {
            if (this.context != null && newCategory != null)
            {
                if (this.context.Categories != null)
                {
                    Category existingCategory = this.context.Categories.Where(cat => cat.CategoryID == newCategory.CategoryID).SingleOrDefault();
                    existingCategory.CategoryName = newCategory.CategoryName;

                    this.context.SaveChanges();

                    return View("ShowCategories", this.context.Categories.ToList());
                }
            }

            this.ModelState.AddModelError("", "No categories found.");

            return PartialView("ShowCategories");
        }

        [HttpPost]
        public ActionResult ReloadCategories()
        {
            return PartialView("ShowCategories", this.context.Categories.ToList());
        }
    }
}