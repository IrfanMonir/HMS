using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSEntities
{
    public class AccomodationPackagePicture
    {
        public int Id { get; set; }
        public int AccomodationPackageId { get; set; }
        public int PictureId { get; set; }
        public virtual Picture Picture { get; set; }
    }
}
