using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Geometry
{
    public interface IGeometry
    {
         Vector3 AdjustMovement(GameObject go, List<GameObject> connections, Vector3 wishPosition);




    }
}
