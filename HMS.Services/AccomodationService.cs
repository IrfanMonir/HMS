using HMS.Data;
using HMSEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class AccomodationService
    {
        public IEnumerable<Accomodation> GetAllAccomodation()
        {
            var context = new HMSContext();
            return context.Accomodations.ToList();
        }
        public IEnumerable<Accomodation> GetAllAccomodationByAccomodationPackage(int accomodationPackageId)
        {
            var context = new HMSContext();
            return context.Accomodations.Where(x=>x.AccomodationPackageId==accomodationPackageId).ToList();
        }
        public IEnumerable<Accomodation> SearchAccomodation(string searchTerm, int? accomodationPackageId, int page, int recordSize)
        {
            var context = new HMSContext();
            var accomodations = context.Accomodations.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodations = accomodations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (accomodationPackageId.HasValue && accomodationPackageId.Value > 0)
            {
                accomodations = accomodations.Where(a => a.AccomodationPackageId == accomodationPackageId.Value);
            }
            var skip = (page - 1) * recordSize;
            return accomodations.OrderBy(x => x.AccomodationPackageId).Skip(skip).Take(recordSize).ToList();
        }

        public int SearchAccomodationCount(string searchTerm, int? accomodationPackageId)
        {
            var context = new HMSContext();
            var accomodations = context.Accomodations.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodations = accomodations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (accomodationPackageId.HasValue && accomodationPackageId.Value > 0)
            {
                accomodations = accomodations.Where(a => a.AccomodationPackageId == accomodationPackageId.Value);
            }

            return accomodations.Count();
        }
        public Accomodation GetAccomodationId(int Id)
        {
            using (var context = new HMSContext())
            {
                return context.Accomodations.Find(Id);
            }
        }
        public bool SaveAccomodation(Accomodation accomodation)
        {
            var context = new HMSContext();
            context.Accomodations.Add(accomodation);
            return context.SaveChanges() > 0;

        }
        public bool UpdateAccomodation(Accomodation accomodation)
        {
            var context = new HMSContext();
            context.Entry(accomodation).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges() > 0;

        }
        public bool DeleteAccomodation(Accomodation accomodation)
        {
            var context = new HMSContext();
            context.Entry(accomodation).State = System.Data.Entity.EntityState.Deleted;
            return context.SaveChanges() > 0;

        }
    }
}
