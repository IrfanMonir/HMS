﻿using HMS.Data;
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
        public IEnumerable<AccomodationType> SearchAccomodationType(string searchTerm)
        {
            var context = new HMSContext();
            var accomodationTypes = context.AccomodationTypes.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodationTypes= accomodationTypes.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            return accomodationTypes.ToList();
        }
        public AccomodationType GetAccomodationTypeId(int Id)
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
        public bool DeleteAccomodationType(AccomodationType accomodationType)
        {
            var context = new HMSContext();
            context.Entry(accomodationType).State = System.Data.Entity.EntityState.Deleted;
            return context.SaveChanges() > 0;

        }
    }
}
