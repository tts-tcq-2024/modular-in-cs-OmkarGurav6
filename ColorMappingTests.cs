using System;
using System.Diagnostics;
using System.Drawing;

namespace TelCo.ColorCoder
{
   
    class Program
    {
        private static ColorPair GetColorFromPairNumber(int pairNumber)
        {
            int minorSize = colorMapMinor.Length;
            int majorSize = colorMapMajor.Length;
            if (pairNumber < 1 || pairNumber > minorSize * majorSize)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("Argument PairNumber:{0} is outside the allowed range", pairNumber));
            }
            
            int zeroBasedPairNumber = pairNumber - 1;
            int majorIndex = zeroBasedPairNumber / minorSize;
            int minorIndex = zeroBasedPairNumber % minorSize;

            ColorPair pair = new ColorPair() { majorColor = colorMapMajor[majorIndex],
                minorColor = colorMapMinor[minorIndex] };
            
            return pair;
        }
       
        private static int GetPairNumberFromColor(ColorPair pair)
        {
            int majorIndex = -1;
            for (int i = 0; i < colorMapMajor.Length; i++)
            {
                if (colorMapMajor[i] == pair.majorColor)
                {
                    majorIndex = i;
                    break;
                }
            }

            int minorIndex = -1;
            for (int i = 0; i < colorMapMinor.Length; i++)
            {
                if (colorMapMinor[i] == pair.minorColor)
                {
                    minorIndex = i;
                    break;
                }
            }
            
            if (majorIndex == -1 || minorIndex == -1)
            {
                throw new ArgumentException(
                    string.Format("Unknown Colors: {0}", pair.ToString()));
            }

            return (majorIndex * colorMapMinor.Length) + (minorIndex + 1);
        }
       
     public class ColorMappingTests
    {
        public static void RunTests()
        {
            ValidateColorMapping(4, Color.White, Color.Brown);
            ValidateColorMapping(5, Color.White, Color.SlateGray);
            ValidateColorMapping(23, Color.Violet, Color.Green);
            ValidatePairNumberMapping(Color.Yellow, Color.Green, 18);
            ValidatePairNumberMapping(Color.Red, Color.Blue, 6);
        }
        private static void ValidateColorMapping(int pairNumber, Color expectedMajor, Color expectedMinor)
        {
            ColorPair colorPair = ColorMap.GetColorFromPairNumber(pairNumber);
            Console.WriteLine("[In]Pair Number: {0},[Out] Colors: {1}\n", pairNumber, colorPair);
            Debug.Assert(colorPair.MajorColor == expectedMajor);
            Debug.Assert(colorPair.MinorColor == expectedMinor);
        }

        private static void ValidatePairNumberMapping(Color majorColor, Color minorColor, int expectedPairNumber)
        {
            ColorPair colorPair = new ColorPair(majorColor, minorColor);
            int pairNumber = ColorMap.GetPairNumberFromColor(colorPair);
            Console.WriteLine("[In]Colors: {0}, [Out] PairNumber: {1}\n", colorPair, pairNumber);
            Debug.Assert(pairNumber == expectedPairNumber);
        }
    }
}
