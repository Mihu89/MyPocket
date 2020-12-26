using Models;
using Persistence;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MyPocket
{
    class Program
    {
        static readonly string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        static int currentOption;
        static void Main(string[] args)
        {
            Welcome();
            //Run();
            Console.WriteLine(connStr);
            UsingDatabase();
            Console.ReadKey();
        }

        private static void UsingDatabase()
        {
            UserCrud userCrud = new UserCrud(connStr);
            Console.WriteLine($"We have {userCrud.UsersCounter()} users in database");
            // insert

            var userToInsert = new User
            {
                FirstName = "John",
                LastName = "Skeet",
                Email = "Jony@mail.com",
                Mobile = "10157595",
                DateofBirth = new DateTime(1978, 10, 12)
            };
            // Console.WriteLine($" Was inserted {userCrud.InsertUser(userToInsert)} users");

            // Delete User
            //userCrud.DeleteUser(1);

            // Update User
            userCrud.UpdateUserMobile(6);

            //display all
            var userList = userCrud.DisplayAllUsers();
            foreach (var user in userList)
            {
                Console.WriteLine($"{user.Id} {user.FirstName} {user.LastName}" +
                    $" {user.Email} {user.Mobile} {user.DateofBirth} {user.CreatedBy} " +
                    $"{user.CreatedDate} {user.ModifiedBy} {user.ModifiedDate} {user.IsDeleted}");
            }


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
