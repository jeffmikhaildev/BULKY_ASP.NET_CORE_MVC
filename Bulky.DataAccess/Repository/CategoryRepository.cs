﻿using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.Irepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {

        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        // implementing the methods from ICategoryRepository
        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
