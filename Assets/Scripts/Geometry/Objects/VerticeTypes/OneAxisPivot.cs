using Assets.Scripts.Geometry.Objects.VerticeTypes;
using Assets.Scripts.Spawn.Matricies;
using Assets.Scripts.Spawn.Structures.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Networking;

namespace Assets.Scripts.Geometry.Types
{
    internal  class OneAxisPivot : IGeometry
    {
        /// <summary>
        /// can work only if all connections are on the same axis and have same rotation
        /// ellastic connection can be streched
        /// <returns></returns>
        /// 


        public void AdjustMovement(GameObject go, GameObject goAround, List<Structure.connection> connections)
        {
            Vector3 shiftVector = Matricies.MoveToRayedPlanePossition(go, goAround);

           
            foreach (Structure.connection c in connections) // if connection is solit on moved object part
            {
                if (c.endPoint != goAround)
                {
                    Vector3 connectionsShift = new Vector3();
                    connectionsShift = c.endPoint.transform.position - go.transform.position;
                    c.endPoint.transform.position = goAround.transform.position + shiftVector + connectionsShift;
                }
            }

            go.transform.position = goAround.transform.position + shiftVector;

        }







        public Vector3 GetSolidLocation(GameObject go)
        {
            throw new NotImplementedException();
        }

    }
}
