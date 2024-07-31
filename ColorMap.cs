using System;
using System.Drawing;

namespace TelCo.ColorCoder
{
    public static class ColorMap
    {
        private static Color[] colorMapMajor;
        private static Color[] colorMapMinor;

        static ColorMap()
        {
            ColorCodeManual.Initialize(out colorMapMajor, out colorMapMinor);
        }

        public static ColorPair GetColorFromPairNumber(int pairNumber)
        {
            int minorSize = colorMapMinor.Length;
            int majorSize = colorMapMajor.Length;
            if (pairNumber < 1 || pairNumber > minorSize * majorSize)
            {
                throw new ArgumentOutOfRangeException(
                    $"Argument PairNumber:{pairNumber} is outside the allowed range");
            }
            
            int zeroBasedPairNumber = pairNumber - 1;
            int majorIndex = zeroBasedPairNumber / minorSize;
            int minorIndex = zeroBasedPairNumber % minorSize;

            return new ColorPair(colorMapMajor[majorIndex], colorMapMinor[minorIndex]);
        }

        public static int GetPairNumberFromColor(ColorPair pair)
        {
            int majorIndex = Array.IndexOf(colorMapMajor, pair.MajorColor);
            int minorIndex = Array.IndexOf(colorMapMinor, pair.MinorColor);

            if (majorIndex == -1 || minorIndex == -1)
            {
                throw new ArgumentException($"Unknown Colors: {pair}");
            }

            return (majorIndex * colorMapMinor.Length) + (minorIndex + 1);
        }
    }
}
