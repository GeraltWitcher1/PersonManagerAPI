using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonManagerAPI.Models;
using PersonManagerAPI.Persistence;

namespace PersonManagerAPI.Data
{
    
    public class AdultManager : IAdultService
    {
        private FileContext fileContext;
        private IList<Adult> adults;
        
        public AdultManager(FileContext fileContext)
        {
            this.fileContext = fileContext;
            adults = fileContext.Adults;
        }
        
        public async Task<Adult> GetAdult(int id)
        {
            return getAdult(id);
        }
        
        public async Task<Adult> AddAdult(Adult adult)
        {
            int max;
            try
            {
                max = adults.Max(ad => ad.Id);
            }
            catch (InvalidOperationException)
            {
                max = 1;
            }
            
            adult.Id += ++max;
            adults.Add(adult);
            fileContext.SaveChanges();
            return adult;
        }

        public async Task RemoveAdult(int id)
        {
            adults.Remove(getAdult(id));
            fileContext.SaveChanges();
        }

        public async Task<Adult> UpdateAdult(Adult adult)
        {
            Adult toUpdate = getAdult(adult.Id);
            toUpdate.FirstName = adult.FirstName;
            toUpdate.LastName = adult.LastName;
            toUpdate.Age = adult.Age;
            toUpdate.Weight = adult.Weight;
            toUpdate.Height = adult.Height;
            toUpdate.HairColor = adult.HairColor;
            fileContext.SaveChanges();
            return adult;
        }


        private Adult getAdult(int id)
        {
            return adults.First(ad => ad.Id == id);
        }
        
        public async Task<IList<Adult>> GetAll()
        {
            return new List<Adult>(adults);
        }
    }
}