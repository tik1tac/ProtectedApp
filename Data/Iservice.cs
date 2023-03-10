namespace ProtectedApp.Data
{
    public interface IService
    {
        Task<IEnumerable<User>> GetUser();
        Task<User> GetUserById(int id);
        void Create();
        void Update();
        void Delete();
    }
}