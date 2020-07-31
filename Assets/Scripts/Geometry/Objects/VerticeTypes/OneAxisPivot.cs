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

            

           /* foreach (GameObject cg in connections) // check if connections are in one axis
            {
                Vector3 cgT = new Vector3();
                cgT = cg.transform.position - go.transform.position;

                if (cgT.y/cgT.x != solidAxis.y/solidAxis.x ||
                    cgT.z / cgT.x != solidAxis.z / solidAxis.x )
                {
                    return go.transform.position; // cannot move if 
                }
            }*/
           

           // Vector3 rotation = go.transform.rotation.eulerAngles;

             Vector3 wishAngle = new Vector3();
            // wishAngle = connections[0].transform.position - wishPosition;
             wishAngle = GetWishAngle( wishPosition - connections[0].transform.position );
            
           //  wishAngle.y = connections[0].transform.rotation.eulerAngles.y;
            //  wishAngle.z = connections[0].transform.rotation.eulerAngles.z;

            Vector3 newPos = new Vector3();


            /* newPos.x = (float)(solidAxis.magnitude / Math.Sqrt(1+ Math.Abs(Math.Pow(Math.Tan(wishAngle.y / 180 * Math.PI), 2)) + Math.Abs(Math.Pow(Math.Tan(wishAngle.y / 180 * Math.PI), 2)) * Math.Abs(Math.Pow(Math.Tan(wishAngle.x / 180 * Math.PI), 2))) );
             newPos.y = (float)(newPos.x * Math.Tan(wishAngle.y / 180 * Math.PI) * Math.Tan(wishAngle.x / 180 * Math.PI));
             newPos.z = newPos.x * (float)Math.Tan(wishAngle.y / 180 * Math.PI);*/




           
           /* if ((wishPosition - connections[0].transform.position).y < 0 && (wishPosition - connections[0].transform.position).x < 0) // temp. need 2b cleaned (for adjusting to unity angles)
            {
                newPos.x = -solidAxis.magnitude * (float)Math.Cos(wishAngle.x / 180 * Math.PI) * (float)Math.Sin(wishAngle.y / 180 * Math.PI);
                newPos.z = solidAxis.magnitude * (float)Math.Cos(wishAngle.x / 180 * Math.PI) * (float)Math.Cos(wishAngle.y / 180 * Math.PI);
                newPos.y = solidAxis.magnitude * (float)Math.Sin(wishAngle.x / 180 * Math.PI);
            }
            else
            {
                newPos.y = solidAxis.magnitude * (float)Math.Sin(wishAngle.x / 180 * Math.PI);
                newPos.x = solidAxis.magnitude * (float)Math.Cos(wishAngle.x / 180 * Math.PI) * (float)Math.Sin(wishAngle.y / 180 * Math.PI);
                newPos.z = solidAxis.magnitude * (float)Math.Cos(wishAngle.x / 180 * Math.PI) * (float)Math.Cos(wishAngle.y / 180 * Math.PI);
           }*/



           // newPos.y = solidAxis.magnitude * (float)Math.Sin(wishAngle.x / 180 * Math.PI);
            //newPos.x *= (float)Math.Sin(wishAngle.x / 180 * Math.PI);

            // Debug.Log(wishAngle);
            //newPos.y = (float)(newPos.z * Math.Tan(wishAngle.x / 180 * Math.PI) );
            //newPos.x = (float)(newPos.y / Math.Tan(wishAngle.z / 180 * Math.PI) );

            go.transform.position = connections[0].transform.position + newPos;
            // Debug.Log(wishAngle);
            

            


            return go.transform.position;
        }


        public static Vector3 Short(Vector3 fromC, Vector3 toC, Vector3 vec)
        {
            Vector3 coor = new Vector3();
            float lambda =
                vec.x * (toC.x - fromC.x) +
                vec.y * (toC.y - fromC.y) +
                vec.z * (toC.z - fromC.z);

            lambda /=
                vec.x * vec.x +
                vec.y * vec.y +
                vec.z * vec.z;

            coor.x = fromC.x + lambda * vec.x;
            coor.y = fromC.y + lambda * vec.y;
            coor.z = fromC.z + lambda * vec.z;

            Debug.Log(coor);

            return coor;
        }


        private static Vector3 GetWishAngle(Vector3 axis)
        {
            Debug.Log(axis);
            Vector3 vec = new Vector3(axis.x, axis.y, axis.z);

            //vec.x = (float)Math.Atan(axis.y / axis.z ) * 180 / (float)Math.PI;
             vec.y = Math.Abs((float)Math.Atan(axis.x / axis.z) * 180/(float)Math.PI) ;
            vec.x = Math.Abs((float)Math.Atan(axis.y / axis.x) * 180 / (float)Math.PI);
        //    vec.y = 0;
            vec.z = 0;
            // wishAngle.z = (float)Math.Atan(axis.y / axis.x);


            if (axis.y < 0) vec.x *= -1;
            else if (axis.x < 0) vec.y *= -1;









            return vec;
        }


        public Vector3 GetSolidLocation(GameObject go)
        {
            throw new NotImplementedException();
        }

    }
}
