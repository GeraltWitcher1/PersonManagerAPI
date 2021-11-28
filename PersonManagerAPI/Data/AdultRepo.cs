using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonManagerAPI.Models;
using PersonManagerAPI.Persistence;

namespace PersonManagerAPI.Data
{
    public class AdultRepo : IAdultService
    {
        private readonly PersonDbContext _dbContext;

        public AdultRepo(PersonDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Adult> AddAdult(Adult adult)
        {
            var added = await _dbContext.AddAsync(adult);
            await _dbContext.SaveChangesAsync();
            return added.Entity;
        }

        public async Task RemoveAdult(int id)
        {
            var toRemove = await _dbContext.Adults.FirstOrDefaultAsync(adult => adult.Id == id);
            if (toRemove != null)
            {
                _dbContext.Adults.Remove(toRemove);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Adult> UpdateAdult(Adult adult)
        {
            try
            {
                _dbContext.Update(adult);
                await _dbContext.SaveChangesAsync();
                return adult;
            }
            catch (Exception)
            {
                throw new Exception($"Did not find adult with an id #{adult.Id}");
            }
        }

        public async Task<Adult> GetAdult(int id)
        {
            try
            {
                return await _dbContext.Adults
                    .Include(adult => adult.JobTitle)
                    .FirstAsync(adult => adult.Id == id);
            }
            catch (Exception)
            {
                throw new Exception($"Did not find an adult with id #{id}");
            }
        }

        public async Task<IList<Adult>> GetAll()
        {
            return await _dbContext.Adults.Include(adult => adult.JobTitle).ToListAsync();
        }
    }
}