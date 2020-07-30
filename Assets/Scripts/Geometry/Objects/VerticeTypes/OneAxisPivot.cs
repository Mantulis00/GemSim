using Assets.Scripts.Geometry.Objects.VerticeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations;

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
                Vector3 cgT = new Vector3();
                cgT = cg.transform.position - go.transform.position;

                if (cgT.y/cgT.x != solidAxis.y/solidAxis.x ||
                    cgT.z / cgT.x != solidAxis.z / solidAxis.x )
                {
                    return go.transform.position; // cannot move if 
                }
            }
           

           // Vector3 rotation = go.transform.rotation.eulerAngles;

             Vector3 wishAngle = new Vector3();
            // wishAngle = connections[0].transform.position - wishPosition;
             wishAngle = GetWishAngle(connections[0].transform.position - wishPosition);
            
           //  wishAngle.y = connections[0].transform.rotation.eulerAngles.y;
            //  wishAngle.z = connections[0].transform.rotation.eulerAngles.z;

            Vector3 newPos = new Vector3();


            /* newPos.x = (float)(solidAxis.magnitude / Math.Sqrt(1+ Math.Abs(Math.Pow(Math.Tan(wishAngle.y / 180 * Math.PI), 2)) + Math.Abs(Math.Pow(Math.Tan(wishAngle.y / 180 * Math.PI), 2)) * Math.Abs(Math.Pow(Math.Tan(wishAngle.x / 180 * Math.PI), 2))) );
             newPos.y = (float)(newPos.x * Math.Tan(wishAngle.y / 180 * Math.PI) * Math.Tan(wishAngle.x / 180 * Math.PI));
             newPos.z = newPos.x * (float)Math.Tan(wishAngle.y / 180 * Math.PI);*/

            newPos.x = solidAxis.magnitude * (float)Math.Cos(wishAngle.x/180*Math.PI);
            newPos.y = solidAxis.magnitude * (float)Math.Sin(wishAngle.x / 180 * Math.PI);

            // Debug.Log(wishAngle);
            //newPos.y = (float)(newPos.z * Math.Tan(wishAngle.x / 180 * Math.PI) );
            //newPos.x = (float)(newPos.y / Math.Tan(wishAngle.z / 180 * Math.PI) );

            go.transform.position = connections[0].transform.position + newPos;
            Debug.Log(wishAngle);

            


            return go.transform.position;
        }


        private static Vector3 GetWishAngle(Vector3 axis)
        {

            //axis.x = (float)Math.Atan(axis.y / axis.z ) * 180 / (float)Math.PI;
            //axis.y = (float)Math.Atan(axis.x / axis.z) * 180/(float)Math.PI ;
            axis.x = (float)Math.Atan(axis.y / axis.x) * 180 / (float)Math.PI;
            axis.y = 0;
            axis.z = 0;
            // wishAngle.z = (float)Math.Atan(axis.y / axis.x);


            return axis;
        }


        public Vector3 GetSolidLocation(GameObject go)
        {
            throw new NotImplementedException();
        }

    }
}
