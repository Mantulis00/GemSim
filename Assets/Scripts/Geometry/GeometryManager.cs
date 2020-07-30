
using Assets.Scripts.Geometry.Objects.VerticeTypes;
using Assets.Scripts.Geometry.Types;
using Assets.Scripts.Spawn;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Geometry
{
    public  class GeometryManager
    {
         Dictionary<GameObject, IGeometry> geometryTypes;

        public GeometryManager()
        {
            geometryTypes = new Dictionary<GameObject, IGeometry>();

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
            {
                OneAxisPivot geo = new OneAxisPivot();
                geometryTypes.Add(go, geo);
            }
                
        }

        internal void AdjustMovement(GameObject go, List<GameObject> connections, Vector3 wishPosition)
        {
            if (geometryTypes[go] is  OneAxisPivot)
            {
                go.transform.position = geometryTypes[go].AdjustMovement(go, connections, wishPosition);
            }
        }


    }
}
