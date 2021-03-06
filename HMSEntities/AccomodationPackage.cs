﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSEntities
{
    public class AccomodationPackage
    {
        public int Id { get; set; }
        public int AccomodationTypeId { get; set; }
        public virtual AccomodationType AccomodationType { get; set; }
        public string Name { get; set; }
        public int NoOfRoom { get; set; }
        public decimal FeePerNight { get; set; }
        public string Description { get; set; }
        public virtual List<AccomodationPackagePicture> AccomodationPackagePictures { get; set; }
    }
}
