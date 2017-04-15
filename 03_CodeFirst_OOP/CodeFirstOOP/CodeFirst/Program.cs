using CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new UsersContext();
            context.Database.Initialize(true);

            using (context)
            {
                //TODO: needs a valid entity
                var newUser = new User();
                newUser.Username = "Pesho";
                newUser.Password = "BestPassword";
                newUser.Age = 10;

                try
                {
                    context.Users.Add(newUser);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
