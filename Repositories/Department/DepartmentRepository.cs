using Dapper;
using SmartwayTestTask.Database.Context;
using SmartwayTestTask.Models.Base;
using SmartwayTestTask.Models.Department;
using SmartwayTestTask.Repositories.Department.Interface;
using System.Data;

namespace SmartwayTestTask.Repositories.Department
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private DatabaseContext _databaseContext;

        public DepartmentRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<T> Create<T>(T model) where T : IModel
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            string query = """
                INSERT INTO Department (Name, Phone)
                VALUES (@Name, @Phone)
                returning *;
                """;
            return await connection.QuerySingleAsync<T>(query, model);
        }

        public async Task<T> GetOrCreate<T>(T departmentModel) where T : class, IModel
        {
            return await GetOrDefault(departmentModel) ?? await Create(departmentModel);
        }

        public async Task Update<T>(T model) where T : IModel
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            var query = """
                UPDATE Department 
                SET Name = @Name,
                    Surname = @Surname, 
                WHERE Id = @Id
            """;
            await connection.ExecuteAsync(query, model);
        }

        public async Task<T?> GetOrDefault<T>(T model) where T : class, IModel
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            string query = """
                SELECT * FROM Department WHERE Name=@Name AND Phone=@Phone
                """
            ;
            return await connection.QuerySingleOrDefaultAsync<DepartmentModel>(query, model) as T;
        }

        public async Task<DepartmentModel?> GetDepartmentModelById(int id)
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            string query = """
                SELECT * FROM Department WHERE Id=@id
                """
            ;
            return await connection.QuerySingleOrDefaultAsync<DepartmentModel>(query, new { id });
        }

        public async Task<DepartmentModel?> GetDepartmentModelByName(string name)
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            string query = """
                SELECT * FROM Department WHERE Name=@name
                """
            ;
            return await connection.QuerySingleOrDefaultAsync<DepartmentModel>(query, new {name});
        }

        public async Task Delete(int id)
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            var query = """
                DELETE FROM Department 
                WHERE Id = @Id
            """;
            await connection.ExecuteAsync(query, id);
        }
    }
}
