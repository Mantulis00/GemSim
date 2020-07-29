using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Geometry.Types
{
    class BallPivot : IGeometry // ball joint - connectors have to stay same lenght
    {
        public Vector3 AdjustMovement(GameObject go, List<GameObject> connections, Vector3 wishPosition)
        {
            throw new NotImplementedException();
        }
    }
}
