using Assets.Scripts.Geometry.Objects.VerticeTypes;
using Assets.Scripts.Spawn.Matricies;
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


        public void AdjustMovement(GameObject go, GameObject goAround)
        {
           go.transform.position = goAround.transform.position + Matricies.MoveToRayedPlanePossition(go, goAround);
        }







        public Vector3 GetSolidLocation(GameObject go)
        {
            throw new NotImplementedException();
        }

    }
}
