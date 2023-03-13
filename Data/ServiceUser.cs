using Dapper;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace ProtectedApp.Data
{
    public class ServiceUser : IServiceUser
    {
        private IConfiguration _iconfig;
        private string _connectstring;

        public ServiceUser(IConfiguration iconfig)
        {
            this._iconfig = iconfig;
            _connectstring = _iconfig.GetConnectionString("DataUser");
        }

        public async Task<bool> Create(User user)
        {
            var parametr = new DynamicParameters();
            parametr.Add("UserId", Guid.NewGuid(), DbType.Guid);
            parametr.Add("Name", user.Name, DbType.String);
            parametr.Add("Surname", user.Surname, DbType.String);
            parametr.Add("Familia", user.Familia, DbType.String);
            parametr.Add("Birthday", user.Birthday, DbType.Date);
            parametr.Add("Sex", user.Sex, DbType.String);
            using (var _dbconnect = new Microsoft.Data.SqlClient.SqlConnection(_connectstring))
            {
                if (_dbconnect.State == ConnectionState.Closed)
                    await _dbconnect.OpenAsync();

                try
                {
                    await _dbconnect.ExecuteAsync(
                        "Insert Into Users VALUES (@UserId,@Name,@Surname,@Familia,@Birthday,@Sex)", parametr,null,null,
                        CommandType.StoredProcedure);
                    return true;
                }
                catch (Exception e)
                {
                    throw;
                }
                finally
                {
                    if (_dbconnect.State == ConnectionState.Closed)
                        await _dbconnect.CloseAsync();
                }
            }
        }

        public async Task<bool> Delete(int id)
        {
            var parametr = new DynamicParameters();
            parametr.Add("UserID", id, DbType.Guid);

            using (var _dbconcect = new Microsoft.Data.SqlClient.SqlConnection(_connectstring))
            {
                if (_dbconcect.State == ConnectionState.Closed)
                    await _dbconcect.OpenAsync();
                try
                {
                    await _dbconcect.ExecuteAsync("Delete Users where Id_user=@UserID", parametr, null,null,
                        CommandType.StoredProcedure);
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (_dbconcect.State == ConnectionState.Open)
                        await _dbconcect.CloseAsync();
                }
            }

            return true;
        }

        public async Task<User> GetUserById(int id)
        {
            var parametr = new DynamicParameters();
            parametr.Add("UserID", id, DbType.Guid);

            using (var _dbconcect = new Microsoft.Data.SqlClient.SqlConnection(_connectstring))
            {
                if (_dbconcect.State == ConnectionState.Closed)
                    await _dbconcect.OpenAsync();
                try
                {
                    return await _dbconcect.QueryFirstOrDefaultAsync<User>("Select * From Users where Id_user=@UserId",
                        parametr, null, null, CommandType.StoredProcedure);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (_dbconcect.State == ConnectionState.Open)
                        await _dbconcect.CloseAsync();
                }
            }
        }

        public async Task<IEnumerable<User>> GetUser()
        {
            using (var _dbconect = new Microsoft.Data.SqlClient.SqlConnection(_connectstring))
            {
                if (_dbconect.State == ConnectionState.Closed)
                    await _dbconect.OpenAsync();
                try
                {
                    return await _dbconect.QueryAsync<User>("Select * From Users", CommandType.StoredProcedure);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (_dbconect.State == ConnectionState.Open)
                        await _dbconect.CloseAsync();
                }
            }
        }

        public async Task<bool> Update(User user)
        {
            var parametr = new DynamicParameters();
            parametr.Add("UserId", user.Id_user, DbType.Guid);
            parametr.Add("Name", user.Name, DbType.String);
            parametr.Add("Surname", user.Surname, DbType.String);
            parametr.Add("Familia", user.Familia, DbType.String);
            parametr.Add("Birthday", user.Birthday, DbType.Date);
            parametr.Add("Sex", user.Sex, DbType.String);
            using (var _dbconcect = new Microsoft.Data.SqlClient.SqlConnection(_connectstring))
            {
                if (_dbconcect.State == ConnectionState.Closed)
                    await _dbconcect.OpenAsync();
                try
                {
                    await _dbconcect.ExecuteAsync(
                        "Update Users Set Name=@Name,Surname=@Surname,Familia=@Familia,Birthday=@Birthday,Sex=@Sex Where Id=@UserId",
                        parametr, null, null, CommandType.StoredProcedure);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    if (_dbconcect.State == ConnectionState.Open)
                        await _dbconcect.CloseAsync();
                }
            }

            return true;
        }
    }
}