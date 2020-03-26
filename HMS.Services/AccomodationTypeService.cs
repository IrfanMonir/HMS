using HMS.Data;
using HMSEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
     public class AccomodationTypeService
    {
        public IEnumerable<AccomodationType> GetAllAccomodationType()
        {
            var context = new HMSContext();
            return context.AccomodationTypes.ToList();
        }
        public AccomodationType GetAllAccomodationTypeId(int Id)
        {
            var context = new HMSContext();
            return context.AccomodationTypes.Find(Id);
        }
        public bool SaveAccomodationType(AccomodationType accomodationType)
        {
            var context = new HMSContext();
            context.AccomodationTypes.Add(accomodationType);
            return context.SaveChanges()>0;
           
        }
        public bool UpdateAccomodationType(AccomodationType accomodationType)
        {
            var context = new HMSContext();
            context.Entry(accomodationType).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges() > 0;

        }
    }
}
