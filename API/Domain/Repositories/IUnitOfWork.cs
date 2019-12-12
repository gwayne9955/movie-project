using System;
using System.Threading.Tasks;

namespace movie_project.API.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
