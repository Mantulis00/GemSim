
using Assets.Scripts.Geometry.Objects.VerticeTypes;
using Assets.Scripts.Geometry.Types;
using Assets.Scripts.Spawn;
using Assets.Scripts.Spawn.TangentProjection;
using Assets.Test;
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

        }

        //temp test


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



        internal void AdjustMovement(GameObject go, GameObject goAround)
        {
            if (!geometryTypes.ContainsKey(go))
            {
                GiveType(go, GeometryType.SingleAxis); // temp
            }

           // Debug.Log(geometryTypes[go]);

         //   if (geometryTypes[go] is  OneAxisPivot)
         //   {


            geometryTypes[go].AdjustMovement(go, goAround);
          //  }
        }


    }
}
