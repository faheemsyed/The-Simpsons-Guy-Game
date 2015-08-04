using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame9
{
    class GiantChickenMeasurements
    {
        public class standing
        {
            static public float delay = 40f;    //is how much time delay before the next frame starts
            static public int speed = 1;    //is how fast the sprite walks across the screen
            static public int numOfFrames = 1;
            static public int[] X = new int[1] { 0 };
            static public int[] Y = new int[1] { 0};
            static public int[] Width = new int[1] { 91 };
            static public int[] Height = new int[1] { 117};
        }
        public class hurt
        {
            static public float delay = 1f;    //is how much time delay before the next frame starts
            static public int speed = 5;    //is how fast the sprite walks across the screen
            static public int numOfFrames = 2;
            static public int[] X = new int[2] { 428, 529};
            static public int[] Y = new int[2] { 0, 0};
            static public int[] Width = new int[2] { 93, 110};
            static public int[] Height = new int[2] { 117, 117};
        }
        public class walking
        {
            static public float delay = 50f;
            static public int speed = 5;
            static public int numOfFrames = 1;
            static public int[] X = new int[1] {0};
            static public int[] Width = new int[1] {91};
            static public int[] Y = new int[1] { 0};
            static public int[] Height = new int[1] { 117};
        }
        public class firing
        {
            static public float delay = 50F;    //is how much time delay before the next frame starts
            static public int speed = 1;    //is how fast the sprite walks across the screen
            static public int numOfFrames = 4;
            static public int[] X = new int[4] { 0, 90, 186, 315};
            static public int[] Y = new int[4] { 0, 0, 0, 0 };
            static public int[] Width = new int[4] {91, 95, 130, 112};
            static public int[] Height = new int[4] { 117, 117, 117, 117 };
        }
        public class dead
        {
            static public float delay = 250f;    //is how much time delay before the next frame starts
            static public int speed = 1;    //is how fast the sprite walks across the screen
            static public int numOfFrames = 7;
            static public int[] X = new int[7] { 0, 159,319,478,647,782,899 };
            static public int[] Y = new int[7] { 117,117,117,117,117,117,117 };
            static public int[] Width = new int[7] { 158,159,158,168,134,116,116 };
            static public int[] Height = new int[7] { 122,122,122,122,122,122,122 };
        }
    }
}
