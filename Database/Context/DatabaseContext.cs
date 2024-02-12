using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

namespace SmartwayTestTask.Database.Context
{
    public class DatabaseContext
    {
        private readonly IConfiguration Configuration;

        public DatabaseContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new SqliteConnection(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public async Task InitDatabase()
        {
            using IDbConnection connection = CreateConnection();
            string query = """
                CREATE TABLE IF NOT EXISTS 
                Passport (
                    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    Type TEXT,
                    Number TEXT
                );

                CREATE TABLE IF NOT EXISTS 
                Department (
                    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    Name TEXT,
                    Phone TEXT
                );

                CREATE TABLE IF NOT EXISTS 
                Employees (
                    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                	Name TEXT,
                	Surname TEXT,
                	Phone TEXT,
                    CompanyId INTEGER,
                	PassportId INTEGER,
                	DepartmentID INTEGER
                );
            """;
            await connection.ExecuteAsync(query);
        }
    }
}
