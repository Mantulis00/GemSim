using Assets.Scripts.Geometry.Objects.VerticeTypes;
using Assets.Scripts.Spawn.Matricies;
using Assets.Scripts.Spawn.Structures.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Assets.Scripts.Geometry.Types
{
    internal  class OneAxisPivot : IGeometry
    {
        /// <summary>
        /// can work only if all connections are on the same axis and have same rotation
        /// ellastic connection can be streched
        /// <returns></returns>
        /// 


        public void AdjustMovement(GameObject go, GameObject goAround, List<GameObject> points )
        {
            Vector3 shiftVector = Matricies.MoveToRayedPlanePossition(go, goAround);


            /*   foreach (Structure.root c in structure.structure.ToList()) // if connection is solit on moved object part
               {
                   if (c.endPoint != goAround)
                   {
                       Vector3 connectionsShift = new Vector3();
                       connectionsShift = c.endPoint.transform.position - go.transform.position;
                       c.endPoint.transform.position = goAround.transform.position + shiftVector + connectionsShift;
                   }
               }*/

            

            foreach (GameObject g in points) // getconnectedpoints could be done once per go until changes ?
            {
                Vector3 connectionsShift = new Vector3();
                connectionsShift = g.transform.position - go.transform.position;
                g.transform.position = goAround.transform.position + shiftVector + connectionsShift;
            }

            
         //   structure.GetConnectedPoints(go, goAround);


            go.transform.position = goAround.transform.position + shiftVector;

        }







        public Vector3 GetSolidLocation(GameObject go)
        {
            throw new NotImplementedException();
        }

    }
}
