using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositories.CartRepositories
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
    }
}
