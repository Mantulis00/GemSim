﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Geometry.Objects.VerticeTypes
{
    public interface IGeometry
    {
         void AdjustMovement(GameObject go, GameObject goAround);
         Vector3 GetSolidLocation(GameObject go);


    }
}
