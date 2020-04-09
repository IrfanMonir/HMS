using HMSEntities;
using HMSNew.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMSNew.Areas.Dashboard.ViewModel
{
    public class AccomodationPackageListingModel
    {
       // public IEnumerable<Accomodation> Accomodations { get; set; }//used for accomodationlistingmodel
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
        public string SearchTerm { get; set; }
        public IEnumerable<AccomodationType> AccomodationTypes { get; internal set; }
        public int? AccomodationTypeId { get; set; }

       // public int? AccomodationPackageId { get; set; }//used for accomodationlistingmodel
        public Pager Pager { get; set; }
    }
    public class AccomodationPackageActionModel
    {
        public int Id { get; set; }
        public int AccomodationTypeId { get; set; }
        public AccomodationType AccomodationType { get; set; }
        public string Name { get; set; }
        public int NoOfRoom { get; set; }
        public decimal FeePerNight { get; set; }
        public string PictureIds { get; set; }
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
        public List<AccomodationPackagePicture> AccomodationPackagePictures { get; set; }
    }
    
}