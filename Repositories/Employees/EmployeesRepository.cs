using Dapper;
using SmartwayTestTask.Database.Context;
using SmartwayTestTask.Models.Base;
using SmartwayTestTask.Models.Employee;
using SmartwayTestTask.Models.Request.Update;
using SmartwayTestTask.Repositories.Employees.Interface;
using System.Data;

namespace SmartwayTestTask.Repositories.Employees
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private DatabaseContext _databaseContext;

        public EmployeesRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<T> Create<T>(T model) where T : IModel
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            string query = """
                INSERT INTO Employees (Name, Surname, Phone, CompanyId, PassportId, DepartmentId)
                VALUES (@Name, @Surname, @Phone, @CompanyId, @PassportId, @DepartmentId)
                returning *;
                """;
            return await connection.QuerySingleAsync<T>(query, model);
        }

        public async Task<T> GetOrCreate<T>(T model) where T : class, IModel
        {
            return await GetOrDefault(model) ?? await Create(model);
        }

        public async Task Update<T>(T model) where T : IModel
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            var query = """
                UPDATE Employees 
                SET Name = @Name,
                    Surname = @Surname, 
                    Phone = @Phone, 
                    CompanyId = @CompanyId,
                    PassportId = @PassportId, 
                    DepartmentId = @DepartmentId
                WHERE Id = @Id
            """;
            UpdateRequestModel updateModel = (model as UpdateRequestModel)!;
            await connection.ExecuteAsync(query, new {updateModel.Name, updateModel .Surname, updateModel.Phone, updateModel.CompanyId, PassportId = updateModel.Passport!.Id, DepartmentId = updateModel.Department!.Id, updateModel.Id});
        }
  
        public async Task<T?> GetOrDefault<T>(T model) where T : class, IModel
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            string query = """
                SELECT * FROM Employees WHERE Name=@Name AND Surname=@Surname AND Phone=@Phone AND CompanyId = @CompanyId
                """
            ;
            return await connection.QuerySingleOrDefaultAsync<EmployeeModel>(query, model) as T;
        }

        public async Task<EmployeeModel?> GetEmployeeModelById(int id)
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            string query = """
                    SELECT * FROM Employees 
                    WHERE Id = @id
                """;
            return await connection.QuerySingleOrDefaultAsync<EmployeeModel>(query, new {id});
        }

        public async Task<List<EmployeeModel>> GetEmployeeModelsByCompanyId(int companyId)
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            var query = """
                SELECT * FROM Employees 
                WHERE CompanyId = @companyId
            """;
            return (List<EmployeeModel>) await connection.QueryAsync<EmployeeModel>(query, new { companyId });
        }

        public async Task<List<EmployeeModel>> GetEmployeeModelsByDepartmentId(int departmentId)
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            var query = """
                SELECT * FROM Employees
                FULL OUTER JOIN Department ON Department.Id = Employees.Id
                WHERE DepartmentId = @departmentId
            """;
            return (List<EmployeeModel>) await connection.QueryAsync<EmployeeModel>(query, new { departmentId });
        }

        public async Task Delete(int id)
        {
            using IDbConnection connection = _databaseContext.CreateConnection();
            var query = """
                DELETE FROM Employees 
                WHERE Id = @id
            """;
            await connection.ExecuteAsync(query, new {id});
        }
    }
}
