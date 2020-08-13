using Assets.Scripts.Geometry.Objects.VerticeTypes;
using Assets.Scripts.Geometry.Types;
using Assets.Scripts.GUI.Objects.PointTexturer;
using Assets.Scripts.Spawn.Structures.Setup;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Geometry
{
    public  class GeometryManager
    {
         Dictionary<GameObject, IGeometry> geometryTypes;

        private PointTexturer manyTexture;


        public GeometryManager()
        {
            geometryTypes = new Dictionary<GameObject, IGeometry>();
            manyTexture = new PointTexturer();

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



        internal void AdjustMovement(GameObject go, GameObject goAround, Structure structure)
        {
            if (!geometryTypes.ContainsKey(go))
            {
                GiveType(go, GeometryType.SingleAxis); // temp
            }



            // Debug.Log(geometryTypes[go]);

            //   if (geometryTypes[go] is  OneAxisPivot)
            //   {

            List<GameObject> points = structure.GetConnectedPoints(go, goAround).ToList();

            geometryTypes[go].AdjustMovement(go, goAround, points);


            SpawnerManager.MoveConnectors(points, structure);

            manyTexture.ChangeColor(go, goAround, points, Color.green, Color.red, Color.blue);
           

            //  }
        }


    }
}
