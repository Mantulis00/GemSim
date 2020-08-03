using UnityEngine;
using System;

namespace Assets.Scripts.Spawn.Matricies
{
    static class Matricies
    {
        public static double DotProduct(Vector3 vector1, Vector3 vector2)
        {
            return vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z;
        }

        public static double VectorsAngle(Vector3 vector1, Vector3 vector2)
        {
            double dot = DotProduct(vector1, vector2);

            return Math.Acos(dot / (vector1.magnitude * vector2.magnitude));

        }



    }
}
