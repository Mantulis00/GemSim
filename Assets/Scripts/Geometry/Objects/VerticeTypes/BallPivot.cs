using Assets.Scripts.Geometry.Objects.VerticeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Geometry.Types
{
    internal  class BallPivot : IGeometry  // ball joint - connectors have to stay same lenght
    {
        public Vector3 GetSolidLocation(GameObject go)
        {
            throw new NotImplementedException();
        }



        public void AdjustMovement(GameObject go, GameObject goAround)
        {
            throw new NotImplementedException();
        }
    }
}
