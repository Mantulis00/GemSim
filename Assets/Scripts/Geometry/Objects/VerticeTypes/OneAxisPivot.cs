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


        public void AdjustMovement(GameObject go, GameObject goAround, List<Structure.root> roots )
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

            

            foreach (Structure.root r in roots) // getconnectedpoints could be done once per go until changes ?
            {
                Vector3 connectionsShift = new Vector3();
                connectionsShift = r.point.transform.position- go.transform.position;
                r.point.transform.position = goAround.transform.position + shiftVector + connectionsShift;
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
