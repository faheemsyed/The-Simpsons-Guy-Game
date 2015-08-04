using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame9
{
    class WhiteChickenMeasurements
    {
        public class standing
        {
            static public float delay = 40f;    //is how much time delay before the next frame starts
            static public int speed = 3;    //is how fast the sprite walks across the screen
            static public int numOfFrames = 1;
            static public int[] X = new int[1] { 25 };
            static public int[] Y = new int[1] { 12 };
            static public int[] Width = new int[1] { 65 };
            static public int[] Height = new int[1] { 175 };
        }
        public class walking
        {
            static public float delay = 100f;
            static public int speed = 3;    //is how fast the sprite walks across the screen
            static public int numOfFrames = 4;
            static public int[] X = new int[4] { 25, 126, 220, 126 };
            static public int[] Y = new int[4] { 12, 8, 12, 8 };
            static public int[] Width = new int[4] { 65, 60, 65, 60 };
            static public int[] Height = new int[4] { 175, 179, 175, 179 };
        }
        public class punching
        {
            static public float delay = 40f;    //is how much time delay before the next frame starts
            static public int speed = 3;    //is how fast the sprite walks across the screen
            static public int numOfFrames = 4;
            static public int[] X = new int[4] { 415, 512, 638, 512 };
            static public int[] Y = new int[4] { 10, 10, 10, 10 };
            static public int[] Width = new int[4] { 61, 77, 91, 77 };
            static public int[] Height = new int[4] { 177, 177, 177, 177 };
        }
        public class hurt
        {
            static public float delay = 1f;    //is how much time delay before the next frame starts
            static public int speed = 5;    //is how fast the sprite walks across the screen
            static public int numOfFrames = 1;
            static public int[] X = new int[1] { 25 };
            static public int[] Y = new int[1] { 12 };
            static public int[] Width = new int[1] { 65 };
            static public int[] Height = new int[1] { 175 };
        }

    }
}
