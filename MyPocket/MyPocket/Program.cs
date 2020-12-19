using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MyPocket
{
    class Program
    {
        static int currentOption;
        static void Main(string[] args)
        {
            Welcome();
            Run();
            Console.ReadKey();
        }

        private static void Welcome()
        {
            Console.WriteLine("Welcome to My Pocket App.");
        }

        static void Run()
        {
        StartMenu:
            Console.WriteLine("To login into the App press 1");
            Console.WriteLine("To register into the App press 2");
            Console.WriteLine("To exit press 3");

            Console.WriteLine("Choose an option from 1 to 3");
            currentOption = Int32.Parse(Console.ReadLine());

            switch (currentOption)
            {
                case 1:
                    {
                        // login
                        Console.WriteLine("Login");
                        break;
                    }
                case 2:
                    {
                        // register
                        Console.WriteLine("Register");
                        break;
                    }
                case 3:
                    {
                        Environment.Exit(0);
                        break;
                    }
                default:
                    Console.WriteLine("Option is not valid");
                    goto StartMenu;
                    break;
            }
        }
    }
}
