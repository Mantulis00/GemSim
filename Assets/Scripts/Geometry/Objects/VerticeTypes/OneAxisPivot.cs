using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Geometry.Types
{
    internal static class OneAxisPivot 
    {
        /// <summary>
        /// can work only if all connections are on the same axis and have same rotation
        /// <returns></returns>
        internal static Vector3 AdjustMovement(GameObject go, List<GameObject> connections, Vector3 wishPosition)
        {
            Vector3 solidAxis = new Vector3();
            


            if (connections.Count > 0)
            {
                solidAxis = go.transform.position - connections[0].transform.position;
            }
            else
            {
                go.transform.position = wishPosition;
                return wishPosition;
            }

            foreach (GameObject cg in connections)
            {
                if (cg.transform.position.y/cg.transform.position.x != solidAxis.y/solidAxis.x ||
                    cg.transform.position.z / cg.transform.position.x != solidAxis.z / solidAxis.x )
                {
                    return go.transform.position; // cannot move if 
                }
            }


            Vector3 rotation = go.transform.rotation.eulerAngles;
            Vector3 wishAngle = GetWishAngle(solidAxis);






            return go.transform.position;
        }


        private static Vector3 GetWishAngle(Vector3 axis)
        {
            Vector3 wishAngle = new Vector3();

            wishAngle.y = (float)Math.Atan(axis.x / axis.z);
            wishAngle.x = (float)Math.Atan(axis.x / axis.y);

            return wishAngle;
        }



    }
}
