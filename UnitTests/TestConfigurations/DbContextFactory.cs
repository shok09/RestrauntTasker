using System;
using System.Collections.Generic;
using System.Text;
using DAL.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;

namespace RestrauntTasker.UnitTests.TestConfigurations
{
    public static class DbContextFactory
    {
        public static RestrauntTaskerContext Create()
        {
            var options = new DbContextOptionsBuilder<RestrauntTaskerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new RestrauntTaskerContext(options);

            RestrauntTaskerContextSeeder.Seed(context);

            return context;
        }

        public static void RemoveContext(RestrauntTaskerContext context)
        {
            context.Database.EnsureCreated();
            context.Dispose();
        }
    }
}