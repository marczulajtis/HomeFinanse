using HomeFinanse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanse.ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            string salt = RijndaelEncryption.GetSalt();

            string encrypted = RijndaelEncryption.EncryptRijndael("czarus777", salt);

            Console.WriteLine(encrypted);
            Console.WriteLine(salt);

            Console.ReadKey();
        }
    }
}
