using HMS.Data;
using HMSEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public IEnumerable<AccomodationPackage> GetAllAccomodationPackageByAccomodationType(int accomodationTypeId)
        {
            var context = new HMSContext();
            return context.AccomodationPackages.Where(x=>x.AccomodationTypeId==accomodationTypeId).ToList();
        }
        public IEnumerable<AccomodationPackage> SearchAccomodationPackage(string searchTerm,int? accomodationTypeId,int page,int recordSize)
        {
            var context = new HMSContext();
            var accomodationPackages = context.AccomodationPackages.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodationPackages = accomodationPackages.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (accomodationTypeId.HasValue && accomodationTypeId.Value>0)
            {
                accomodationPackages = accomodationPackages.Where(a=>a.AccomodationTypeId==accomodationTypeId.Value);
            }
            var skip = (page - 1) * recordSize;
            return accomodationPackages.OrderBy(x=>x.AccomodationTypeId).Skip(skip).Take(recordSize).ToList();
        }

        public int SearchAccomodationPackageCount(string searchTerm, int? accomodationTypeId)
        {
            var context = new HMSContext();
            var accomodationPackages = context.AccomodationPackages.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodationPackages = accomodationPackages.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (accomodationTypeId.HasValue && accomodationTypeId.Value > 0)
            {
                accomodationPackages = accomodationPackages.Where(a => a.AccomodationTypeId == accomodationTypeId.Value);
            }
            
            return accomodationPackages.Count();
        }
        public AccomodationPackage GetAccomodationPackageId(int Id)
        {
            using (var context = new HMSContext())
            {
                return context.AccomodationPackages.Find(Id);
            }
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
        public List<AccomodationPackagePicture> GetPictureByAccomodationPackageId(int accomodationPackageId)
        {
            var context = new HMSContext();
            return context.AccomodationPackages.Find(accomodationPackageId).AccomodationPackagePictures.ToList();
        }
    }
}
