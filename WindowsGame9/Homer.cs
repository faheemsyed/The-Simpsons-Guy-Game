using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame9
{
    class Homer
    {
        public class standing
        {
            static public float delay = 40f;    //is how much time delay before the next frame starts
            static public int speed = 5;    //is how fast the sprite walks across the screen
            static public int numOfFrames = 1;
            static public int[] X = new int[1] { 9 };
            static public int[] Y = new int[1] { 4 };
            static public int[] Width = new int[1] { 62 };
            static public int[] Height = new int[1] { 65 };
        }
        public class walking
        {
            static public float delay = 80f;
            static public int speed = 5;    //is how fast the sprite walks across the screen
            static public int numOfFrames = 9;
            static public int[] X = new int[9] { 9, 87, 169, 246, 326, 403, 478, 554, 629 };
            static public int[] Y = new int[9] { 4, 5, 5, 5, 5, 6, 5, 4, 4 };
            static public int[] Width = new int[9] { 62, 63, 61, 61, 61, 60, 60, 61, 61 };
            static public int[] Height = new int[9] { 65, 64, 64, 63, 63, 62, 64, 65, 65 };
        }
        public class hurt
        {
            static public float delay = 60f;
            static public int speed = 5;
            static public int numOfFrames = 1;
            static public int[] X = new int[1] { 6 };
            static public int[] Y = new int[1] { 74 };
            static public int[] Width = new int[1] { 38 };
            static public int[] Height = new int[1] { 64 };
        }
        public class dead
        {
            static public float delay = 100f;
            static public int speed = 1;
            static public int numOfFrames = 1;
            static public int[] X = new int[1] { 234 };
            static public int[] Width = new int[1] { 63 };
            static public int[] Y = new int[1] { 109 };
            static public int[] Height = new int[1] { 30 };
        }
        public class firing
        {
            static public float delay = 40f;    //is how much time delay before the next frame starts
            static public int speed = 5;    //is how fast the sprite walks across the screen
            static public int numOfFrames = 1;
            static public int[] X = new int[1] { 9 };
            static public int[] Y = new int[1] { 4 };
            static public int[] Width = new int[1] { 62 };
            static public int[] Height = new int[1] { 65 };
        }
    }
}