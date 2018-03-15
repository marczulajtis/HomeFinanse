using HomeFinanse.Models;
using Ninject;
using Ninject.Web.Common.WebHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HomeFinanse
{
    public class MvcApplication : NinjectHttpApplication
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css",
                      "~/admin-lte/css/AdminLTE.css",
                      "~/admin-lte/css/skins/skin-blue.css",
                      "~/admin-lte/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"
                      ));

            bundles.Add(new ScriptBundle("~/admin-lte/js").Include(
                    "~/admin-lte/js/adminlte.js",
                    "~/admin-lte/plugins/fastclick/fastclick.js",
                    "~/admin-lte/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"
                ));

            bundles.Add(new StyleBundle("~/Content/login_form").Include(
                    "~/Content/login_form/vendor/animate/animate.css",
                    "~/Content/login_form/vendor/css-hamburgers/hamburgers.min.css",
                    "~/Content/login_form/vendor/animsition/css/animsition.min.css",
                    "~/Content/login_form/vendor/select2/select2.min.css",
                    "~/Content/login_form/vendor/daterangepicker/daterangepicker.css",
                    "~/Content/login_form/css/util.css",
                    "~/Content/login_form/css/main.css"
                    ));

            bundles.Add(new ScriptBundle("~/Content/login_form/js").Include(
                    "~/Content/login_form/vendor/animsition/js/animsition.min.js",
                    "~/Content/login_form/vendor/bootstrap/js/popper.js",
                    "~/Content/login_form/vendor/select2/select2.min.js",
                    "~/Content/login_form/vendor/daterangepicker/moment.min.js",
                    "~/Content/login_form/vendor/daterangepicker/daterangepicker.js",
                    "~/Content/login_form/vendor/countdowntime/countdowntime.js",
                    "~/Content/login_form/js/main.js"
                ));
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        private void RegisterServices(IKernel kernel)
        {
            kernel.Bind<HomeBudgetDBEntities>().ToSelf();
        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterBundles(BundleTable.Bundles);
        }
    }
}