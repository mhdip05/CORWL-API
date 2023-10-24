using CORWL_API.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORWL_UNIT_TESTS.TEST_Utility
{
    public static class DatabaseContextUtility
    {
        public static async Task<DataContext> GetDataContext<T>(DataContext context, DbSet<T> dbset, int numberOfItems, Func<int, T> createItem) where T : class
        {
            if (!await dbset.AnyAsync())
            {
                for (int i = 0; i < numberOfItems; i++)
                {
                    dbset.Add(createItem(i));
                }
                await context.SaveChangesAsync();
            }

            return context;
        }


        public static DataContext GetInMemoryDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();

            return databaseContext;
        }
    }
}
