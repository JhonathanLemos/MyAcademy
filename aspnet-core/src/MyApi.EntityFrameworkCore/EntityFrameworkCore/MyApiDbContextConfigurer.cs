using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MyApi.EntityFrameworkCore
{
    public static class MyApiDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MyApiDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MyApiDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
