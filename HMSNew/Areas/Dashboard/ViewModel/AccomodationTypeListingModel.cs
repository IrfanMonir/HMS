using HMSEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMSNew.Areas.Dashboard.ViewModel
{
    public class AccomodationTypeListingModel
    {
        public IEnumerable <AccomodationType>AccomodationTypes { get; set; }
        public string SearchTerm { get; set; }
    }
    public class AccomodationTypeActionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}