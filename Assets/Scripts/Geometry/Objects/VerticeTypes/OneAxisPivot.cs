using Assets.Scripts.Geometry.Objects.VerticeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Geometry.Types
{
    internal  class OneAxisPivot : IGeometry
    {
        /// <summary>
        /// can work only if all connections are on the same axis and have same rotation
        /// <returns></returns>
        /// 


        public Vector3 AdjustMovement(GameObject go, List<GameObject> connections, Vector3 wishPosition)
        {
            Vector3 solidAxis = new Vector3();
            


            if (connections.Count > 0) // check if has connections at all
            {
                solidAxis = go.transform.position - connections[0].transform.position;
            }
            else
            {
                go.transform.position = wishPosition;
                return wishPosition;
            }

            foreach (GameObject cg in connections) // check if connections are in one axis
            {
                if (cg.transform.position.y/cg.transform.position.x != solidAxis.y/solidAxis.x ||
                    cg.transform.position.z / cg.transform.position.x != solidAxis.z / solidAxis.x )
                {
                    return go.transform.position; // cannot move if 
                }
            }


            Vector3 rotation = go.transform.rotation.eulerAngles;
            Vector3 wishAngle = GetWishAngle(solidAxis);
            wishAngle.y = connections[0].transform.rotation.eulerAngles.y;
            wishAngle.z = connections[0].transform.rotation.eulerAngles.z;

            Vector3 newPos = new Vector3();


            newPos.z = (float)(solidAxis.magnitude / Math.Tan(wishAngle.y) / 180 * Math.PI);
            newPos.y = (float)(newPos.z * Math.Tan(wishAngle.x) / 180 * Math.PI);
            newPos.x = (float)(newPos.y / Math.Tan(wishAngle.z) / 180 * Math.PI);

            go.transform.position = connections[0].transform.position + newPos;

            return go.transform.position;
        }


        private static Vector3 GetWishAngle(Vector3 axis)
        {
            Vector3 wishAngle = new Vector3();

            wishAngle.x = (float)Math.Atan(axis.y / axis.z) * 180/(float)Math.PI;
           // wishAngle.z = (float)Math.Atan(axis.y / axis.x);


            return wishAngle;
        }


        public Vector3 GetSolidLocation(GameObject go)
        {
            throw new NotImplementedException();
        }

    }
}
