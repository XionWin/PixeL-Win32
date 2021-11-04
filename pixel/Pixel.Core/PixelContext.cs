using System;
using System.Collections.Generic;

namespace Pixel.Core
{
    public class PixelContext
    {
		public IParam Param { get; }
		public IEnumerable<float> Commands { get; }
		public float Commandx { get; set; }
		public float Commandy { get; set; }
		//[NVG_MAX_STATES];
		// public NVGstate[] states;
		// public NVGpathCache cache;
		public float TessTol { get; set; }
		public float DistTol { get; set; }
		public float FringeWidth { get; set; }
		public float DevicePxRatio { get; set; }
		// public FONScontext fs;
		//[NVG_MAX_FONTIMAGES];
		public IEnumerable<float> FontImages { get; }
		public int FontImageIdx { get; set; }
		public int DrawCallCount { get; set; }
		public int FillTriCount { get; set; }
		public int StrokeTriCount { get; set; }
		public int TextTriCount { get; set; }
    }


    public class PixelState
    {
        public PixelCompositeOperationState compositeOperation { get; set; }
        public PixelPaint Fill { get; set; }
        public PixelPaint Stroke { get; set; }
        public float StrokeWidth { get; set; }
        public float MiterLimit { get; set; }
        public int LineJoin { get; set; }
        public int LineCap { get; set; }
        public float Alpha { get; set; }
        //[6];
        public float[] Transform { get; } = new float[6];
        public PixelScissor Scissor{ get; set; }
        public float fontSize { get; set; }
        public float LetterSpacing { get; set; }
        public float LineHeight { get; set; }
        public float FontBlur { get; set; }
        public int TextAlign { get; set; }
        public int FontId { get; set; }
    }

	public struct PixelCompositeOperationState
	{
		public int SrcRGB { get; set; }
		public int DstRGB { get; set; }
		public int SrcAlpha { get; set; }
		public int DstAlpha { get; set; }
	}

    public class PixelPaint
	{
		//[6];
		public float[] Transform { get; } = new float[6];
		//[2];
		public float[] Extent { get; } = new float[2];
		public float Radius { get; set; }
		public float Feather { get; set; }
		public IColor InnerColor { get; set; }
		public IColor OuterColor { get; set; }
		public nint Image { get; set; }
    }

	
    public class PixelScissor
	{
		//[6];
		public float[] Transform { get; } = new float[6];
		//[2];
		public float[] Extent { get; } = new float[2];
	}
}
