using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Geometry.Objects.VerticeTypes
{
    public interface IGeometry
    {
         Vector3 AdjustMovement(GameObject go, List<GameObject> connections, Vector3 wishPosition);
         Vector3 GetSolidLocation(GameObject go);
    }
}
