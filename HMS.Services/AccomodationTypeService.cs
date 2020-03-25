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
        public bool SaveAccomodationType(AccomodationType accomodationType)
        {
            var context = new HMSContext();
            context.AccomodationTypes.Add(accomodationType);
            return context.SaveChanges()>0;
           
        }
    }
}
