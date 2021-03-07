using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using BaseInMemoryArchitecture.BusinessLogic.Contracts;
using BaseInMemoryArchitecture.Common.Contracts;
using BaseInMemoryArchitecture.Models.Models;

namespace BaseInMemoryArchitecture.BusinessLogic.Implementations
{
    public class ClientService : BaseService<Client>, IClientService
    {
        public ClientService(
            IConfiguration configuration,
            IMemoryCache memoryCache,
            IJsonManager<Client> jsonManager) : base(configuration, memoryCache, jsonManager)
        { }
    }
}
