﻿using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.ApplicationDbContexts;
using E_Commerce.Infrastructure.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositories.ProductRepositories
{
    public class ProductRepository(ApplicationDbContext appDbContext) :
        GenericRepository<Product>(appDbContext),
        IProductRepository
    {
    }
}
