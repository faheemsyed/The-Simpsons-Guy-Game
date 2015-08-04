using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame9
{
    class HealthBar
    {
        public class Bar
        {
            static public int numOfFrames = 10;
            static public int[] X = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            static public int[] Width = new int[10] { 122, 118, 108, 94, 77, 59, 41, 25, 12, 3};
            static public int[] Y = new int[10] {0, 18, 36, 54, 72, 90, 108, 126, 144, 162};
            static public int[] Height = new int[10] { 14, 14, 14, 14, 14, 14, 14, 14, 14, 14};
        }
    }
}
