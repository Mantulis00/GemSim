
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Spawn
{
    public interface IStructure
    {
          GameObject MakeGO(Transform spawner, GameObject obModel, bool extension, Dictionary<GameObject, StructureType> objectTypes);
          void DeleteGo(GameObject go);
        

        }
}
