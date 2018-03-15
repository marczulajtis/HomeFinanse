using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeFinanse.Models
{
    public class Consts
    {
        public const int MinPasswordLength = 6;

        public const int MaxPasswordLength = 50;

        /// <summary>
        /// Gets hash set of special characters
        /// </summary>
        public static HashSet<char> SpecialCharactersSet
        {
            get
            {
                return new HashSet<char>() { '~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '=', '+', '{', '}', '[', ']', '|', ';', ':', '"', '<', ',', '>', '.', '?' };
            }
        }
    }
}