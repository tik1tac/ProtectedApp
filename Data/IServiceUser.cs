namespace ProtectedApp.Data
{
    public interface IServiceUser
    {
        Task<IEnumerable<User>> GetUser();
        Task<User> GetUserById(int id);
        Task<bool> Create(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(int id);
    }
}