using HMSEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMSNew.ViewModels
{
    public class AccomodationViewModel
    {
       // public int accomodationTypeId { get; set; }
        public  AccomodationType AccomodationTypes { get; set; }
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
        public IEnumerable<Accomodation> Accomodations { get; set; }
        public int selectedAccomodationPackageId { get; internal set; }
    }
}