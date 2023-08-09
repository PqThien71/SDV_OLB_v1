using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDV_OLB_v1.ClassSave
{
    public class cBubbleSetting
    {
        public int MaskWidth { get; set; }
        public int MaskHeight { get; set; }
        public int ValueOffset { get; set; }
        public string LightDark { get; set; }
        public double MinSize { get; set; }
        public double MaxSize { get; set; }
        public string CamType { get; set; }
    }

    public class cKeoSetting
    {
        
        public int MaskWidthTop { get; set; }
        public int MaskHeightTop { get; set; }
        public int ValueOffsetTop { get; set; }

        public int MaskWidthBot { get; set; }
        public int MaskHeightBot { get; set; }
        public int ValueOffsetBot { get; set; }

        public int MaskWidthLeft { get; set; }
        public int MaskHeightLeft { get; set; }
        public int ValueOffsetLeft { get; set; }

        public int MaskWidthRight { get; set; }
        public int MaskHeightRight { get; set; }
        public int ValueOffsetRight { get; set; }
        public int PositionCheckBot { get; set; }
        public int PositionCheckTop { get; set; }
        public int PositionCheckLeft { get; set; }
        public int PositionCheckRight { get; set; }
        public double MinScore { get; set; }

        public int BlobMin { get; set; }
        public int BlobMax { get; set; }
        public int BlobHeightMin { get; set; }
        public int BlobHeightMax { get; set; }
        public string CamType { get; set; }
    }
}
