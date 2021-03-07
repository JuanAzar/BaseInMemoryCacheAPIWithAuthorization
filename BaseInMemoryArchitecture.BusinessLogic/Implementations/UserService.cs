using BaseInMemoryArchitecture.BusinessLogic.Contracts;
using BaseInMemoryArchitecture.Common.Contracts;
using BaseInMemoryArchitecture.Models.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace BaseInMemoryArchitecture.BusinessLogic.Implementations
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(
            IConfiguration configuration,
            IMemoryCache memoryCache,
            IJsonManager<User> jsonManager) : base(configuration, memoryCache, jsonManager)
        { }

        public User Login(string email, string password)
        {
            var user = _entitiesList
                        .Where(x => x.Email == email)
                        .SingleOrDefault();

            if (user != null)
                if (user.Password == password)
                    return user;

            return null;
        }
    }
}
