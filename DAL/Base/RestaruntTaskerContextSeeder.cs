using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Base
{
    public static class RestrauntTaskerContextSeeder
    {
        public static void Seed(RestrauntTaskerContext context)
        {
            var projectsList = new List<Order>
            {
                new Order
                {
                    Title = "Web application",
                    Description = "create new web app",
                    Status = Entities.Enums.OrderStatus.Open,
                    OrderChef = new OrderUser{Name = "Anna"}
                },
                new Order
                {
                    Title = "MVC application",
                    Description = "create new MVC app",
                    Status = Entities.Enums.OrderStatus.Open,
                    OrderChef = new OrderUser{Name = "James"}
                },
                new Order
                {
                    Title = "API project",
                    Description = "create new API project",
                    Status = Entities.Enums.OrderStatus.Open,
                    OrderChef = new OrderUser{Name = "Liam"}
                },
                new Order
                {
                    Title = "Angular app",
                    Description = "create new Angular app",
                    Status = Entities.Enums.OrderStatus.Open,
                    OrderChef = new OrderUser{Name = "Jessica"}
                }
            };

            foreach (var proj in projectsList)
                context.Orders.Add(proj);

            context.SaveChanges();
        }
    }
}