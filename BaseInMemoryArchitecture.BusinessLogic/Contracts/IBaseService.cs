using System.Collections.Generic;
using BaseInMemoryArchitecture.Common.Models;

namespace BaseInMemoryArchitecture.BusinessLogic.Contracts
{
    public interface IBaseService<T> where T : Entity
    {
        void Add(T entity);
        T GetById(int entityId);
        IList<T> GetAll();
        void RemoveById(int entityId);
        void Modify(T entity);
    }
}
