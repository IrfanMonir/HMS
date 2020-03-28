using HMS.Data;
using HMSEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class AccomodationPackageService
    {
        public IEnumerable<AccomodationPackage> GetAllAccomodationPackage()
        {
            var context = new HMSContext();
            return context.AccomodationPackages.ToList();
        }
        public IEnumerable<AccomodationPackage> SearchAccomodationPackage(string searchTerm)
        {
            var context = new HMSContext();
            var accomodationPackages = context.AccomodationPackages.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodationPackages = accomodationPackages.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            return accomodationPackages.ToList();
        }
        public AccomodationPackage GetAccomodationPackageId(int Id)
        {
            var context = new HMSContext();
            return context.AccomodationPackages.Find(Id);
        }
        public bool SaveAccomodationPackage(AccomodationPackage accomodationPackage)
        {
            var context = new HMSContext();
            context.AccomodationPackages.Add(accomodationPackage);
            return context.SaveChanges() > 0;

        }
        public bool UpdateAccomodationPackage(AccomodationPackage accomodationPackage)
        {
            var context = new HMSContext();
            context.Entry(accomodationPackage).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges() > 0;

        }
        public bool DeleteAccomodationPackage(AccomodationPackage accomodationPackage)
        {
            var context = new HMSContext();
            context.Entry(accomodationPackage).State = System.Data.Entity.EntityState.Deleted;
            return context.SaveChanges() > 0;

        }
    }
}
