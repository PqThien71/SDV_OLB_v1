using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace SDV_OLB_v1
{
    public class cHdevProcedure
    {
        public HDevProcedure HdevProSendPLC { get; set; }
        public HDevProcedure HdevProRecPLC { get; set; }
        public HDevProcedure HdevProCheckBOT { get; set; }
        public HDevProcedure HdevProCheckTOP { get; set; }
        public HDevProcedure HdevProCheckLEFT { get; set; }
        public HDevProcedure HdevProCheckRIGHT { get; set; }
        public HDevProcedure HdevProCheckVacantUnder { get; set; }
        public HDevProcedure HdevProCheckVacantOver { get; set; }

    }
}
