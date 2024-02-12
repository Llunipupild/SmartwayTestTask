using Dapper;
using SmartwayTestTask.Database.Context;
using SmartwayTestTask.Models.Base;
using SmartwayTestTask.Models.Passport;
using SmartwayTestTask.Repositories.Passport.Interface;
using System.Data;

namespace SmartwayTestTask.Repositories.Passport
{
    public class PassportRepository : IPassportRepository
    {
        private DatabaseContext _databaseContext;

        public PassportRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<T> Create<T>(T employeeModel) where T : IModel
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            string query = """
                INSERT INTO Passport (Type, Number)
                VALUES (@Type, @Number)
                returning *;
                """;
            return await connection.QuerySingleAsync<T>(query, employeeModel);
        }

        public async Task<T> GetOrCreate<T>(T model) where T : class, IModel
        {
            return await GetOrDefault(model) ?? await Create(model);
        }

        public async Task Update<T>(T model) where T : IModel
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            var query = """
                UPDATE Passport 
                SET Type = @Type,
                    Number = @Number, 
                WHERE Id = @Id
            """;
            await connection.ExecuteAsync(query, model);
        }

        public async Task<T?> GetOrDefault<T>(T model) where T : class, IModel
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            string query = """
                SELECT * FROM Passport WHERE Type=@Type AND Number=@Number
                """
            ;
            return await connection.QuerySingleOrDefaultAsync<PassportModel>(query, model) as T;
        }

        public async Task<PassportModel?> GetPassportModelById(int id)
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            string query = """
                SELECT * FROM Passport WHERE Id=@id
                """
            ;
            return await connection.QuerySingleOrDefaultAsync<PassportModel>(query, new { id });
        }

        public async Task Delete(int id)
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            var query = """
                DELETE FROM Passport 
                WHERE Id = @id
            """;
            await connection.ExecuteAsync(query, new {id});
        }
    }
}
