﻿using HMSEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMSNew.ViewModels
{
    public class HomeViewModel
    {
         
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }

    }
}