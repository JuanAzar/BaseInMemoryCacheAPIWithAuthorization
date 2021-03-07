using BaseInMemoryArchitecture.Models.Models;

namespace BaseInMemoryArchitecture.BusinessLogic.Contracts
{
    public interface IUserService : IBaseService<User>
    {
        User Login(string email, string password);
    }
}
