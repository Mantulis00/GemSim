
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Spawn
{
    public interface IStructure
    {
         public GameObject MakeGO(Transform spawner, GameObject obModel, Dictionary<GameObject, StructureType> objectTypes);
         public void DeleteGo(GameObject go);
        

        }
}
