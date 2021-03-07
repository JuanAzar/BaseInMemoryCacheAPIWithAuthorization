using System.Collections.Generic;
using BaseInMemoryArchitecture.Common.Models;

namespace BaseInMemoryArchitecture.Common.Contracts
{
    public interface IJsonManager<T> where T : Entity
    {
        IList<T> GetContent(string filePath);
    }
}
