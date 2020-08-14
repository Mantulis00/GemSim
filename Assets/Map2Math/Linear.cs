using System;
using UnityEngine;

namespace Assets.Map2Math
{
    static class Linear
    {
        public static float Pythagoras(float x, float y)
        {
            return (float)Math.Sqrt(x * x + y * y);
        }

        public static float Pythagoras3(float x, float y, float z)
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }
        public static double Pythagoras3(Vector3 location)
        {
            return Math.Sqrt(
                location.x * location.x +
                location.y * location.y +
                location.z * location.z);
        }


    }
}
