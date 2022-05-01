using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LbAssgLibrary
{
    public class Librarymenu
    {
        string option;
        public string MainMenu()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Welcome to County Library System");
                Console.WriteLine("    User Login <Press L>");
                Console.WriteLine("    Register New User <Press R>");
                Console.WriteLine("    Press E for Exit");
                option = Console.ReadLine();
                if (option == "L" || option == "R" || option=="E")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Option, Please Enter L/R/E");
                }
            }
            return option;
        }

    }
}
