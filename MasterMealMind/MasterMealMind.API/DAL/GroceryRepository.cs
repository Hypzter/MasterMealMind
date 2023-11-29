﻿using MasterMealMind.DAL;
using MasterMealMind.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Reflection.Metadata.Ecma335;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MasterMealMind.API.DAL
{
    public class GroceryRepository : IGroceryRepository
    {
        private readonly MyDbContext _context;

        public GroceryRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Grocery>> GetAllGroceries()
        {
            return await _context.Groceries.ToListAsync();
        }

        public async Task<Grocery> GetOneGrocery(int id)
        {
            var grocery = await _context.Groceries.FirstOrDefaultAsync(g => g.Id == id);

            return grocery != null ? grocery : null;
        }

        public async Task CreateGrocery(Grocery grocery)
        {
            await _context.Groceries.AddAsync(grocery);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGrocery(Grocery updatedGrocery)
        {
            _context.Entry(updatedGrocery).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public void DeleteGrocery(int id)
        {
            var grocery = _context.Groceries.Find(id);

            if (grocery != null)
            {
                _context.Groceries.Remove(grocery);
                _context.SaveChanges();
            }
        }

        public async Task<bool> GroceryExists(int id)
        {
            return await _context.Groceries.AnyAsync(g => g.Id == id);
        }
    }
}
