using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Contracts.Repository
{
    public interface IUnitOfWork
    {
        //IGenericRepository<T> Repository<T>() where T : class;
        Task CompleteAsync();
    }
}