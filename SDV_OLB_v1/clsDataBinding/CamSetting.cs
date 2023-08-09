using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDV_OLB_v1
{
    class  CamSetting
    {
        public int CamIndex { get; set; }
        public int ExposureTime { get; set; }
        public int Gain { get; set; }
        public string Interface { get; set; }
        public string Device { get; set; }
        public double CalibScale { get; set; }
    }
}
