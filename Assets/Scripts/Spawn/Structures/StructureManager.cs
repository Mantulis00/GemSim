using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Spawn
{
    class StructureManager : MonoBehaviour
    {
        public GameObject MakeGO(Transform spawner, GameObject obModel, GameObject extension, SpawnOptions option)
        {
            GameObject go;
            go = Instantiate(obModel) as GameObject;
            go.transform.SetParent(spawner);

            return go;

            // go.name = LineList.Count.ToString();
            // go.name += (n - 1).ToString();
        }


    }
}
