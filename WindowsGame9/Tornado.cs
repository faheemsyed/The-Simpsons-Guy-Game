using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame9
{
    class Tornado
    {
        public class Shot
        {
            static public float delay = 10f;    //is how much time delay before the next frame starts
            static public int speed = 1;    //is how fast the sprite walks across the screen
            static public int numOfFrames = 10;
            static public int[] X = new int[10] { 0, 82, 165, 248, 332, 417, 470, 523, 574, 626 };
            static public int[] Width = new int[10] { 82, 83, 83, 85, 84, 53, 53, 52, 53, 19 };
            static public int[] Y = new int[10] { 0, 0, 0, 0, 0, 34, 34, 46, 46, 82};
            static public int[] Height = new int[10] { 94, 94, 94, 94, 94, 60, 60, 48, 48, 12};
        }
    }
}
