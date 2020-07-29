
using Assets.Scripts.Geometry.Types;
using Assets.Scripts.Spawn;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Geometry
{
    public  class GeometryManager
    {
         Dictionary<GameObject, IGeometry> geometryType;

        public GeometryManager()
        {
            geometryType = new Dictionary<GameObject, IGeometry>();

            /*GameObject go = null;
            BallPivot b = new BallPivot() ;
            geometryType.Add(go, b);*/
        }

        public void AddElement()
        {

        }

    }
}
