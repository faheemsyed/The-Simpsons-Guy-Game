using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame9
{
    class Peter
    {
        public class standing
        {
            static public float delay = 40f;    //is how much time delay before the next frame starts
            static public int speed = 5;    //is how fast the sprite walks across the screen
            static public int numOfFrames = 1;
            static public int[] X = new int[1] { 0 };
            static public int[] Y = new int[1] { 0 };
            static public int[] Width = new int[1] { 71 };
            static public int[] Height = new int[1] { 81 };
        }
        public class walking
        {
            static public float delay = 60f;
            static public int speed = 5;    //is how fast the sprite walks across the screen
            static public int numOfFrames = 7;
            static public int[] X = new int[7] { 74, 148, 228, 306, 376, 453, 529 };
            static public int[] Y = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
            static public int[] Width = new int[7] { 69, 72, 75, 70, 69, 69, 69 };
            static public int[] Height = new int[7] { 81, 81, 81, 81, 81, 81, 81 };
        }
        public class hurt
        {
            static public float delay = 60f;
            static public int speed = 5;
            static public int numOfFrames = 1;
            static public int[] X = new int[1] { 0 };
            static public int[] Y = new int[1] { 213 };
            static public int[] Width = new int[1] { 52 };
            static public int[] Height = new int[1] { 81 };
        }
        public class dead
        {
            static public float delay = 100f;
            static public int speed = 1;
            static public int numOfFrames = 1;
            static public int[] X = new int[1] { 314 };
            static public int[] Width = new int[1] { 77 };
            static public int[] Y = new int[1] { 213 };
            static public int[] Height = new int[1] { 81 };
        }
        public class firing
        {
            static public float delay = 40f;    //is how much time delay before the next frame starts
            static public int speed = 5;    //is how fast the sprite walks across the screen
            static public int numOfFrames = 1;
            static public int[] X = new int[1] { 0 };
            static public int[] Y = new int[1] { 0 };
            static public int[] Width = new int[1] { 71 };
            static public int[] Height = new int[1] { 81 };
        }
    }
}
