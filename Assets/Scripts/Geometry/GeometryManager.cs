
using Assets.Scripts.Geometry.Types;
using Assets.Scripts.Spawn;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Geometry
{
    public  class GeometryManager
    {
         Dictionary<GameObject, GeometryType> geometryTypes;

        public GeometryManager()
        {
            geometryTypes = new Dictionary<GameObject, GeometryType>();

            /*GameObject go = null;
            BallPivot b = new BallPivot() ;
            geometryType.Add(go, b);*/
        }

        internal void GiveType(GameObject go, GeometryType type)
        {
            if (geometryTypes.ContainsKey(go))
            {
                geometryTypes.Remove(go);
            }

            if (type == GeometryType.SingleAxis)
                geometryTypes.Add(go, type);
        }

        internal void AdjustMovement(GameObject go, List<GameObject> connections, Vector3 wishPosition)
        {
            if (geometryTypes[go] == GeometryType.BallAxis)
            {
                go.transform.position = OneAxisPivot.AdjustMovement(go, connections, wishPosition);
            }
        }


    }
}
