

using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Assets.Scripts.Spawn
{
    class PointManager : MonoBehaviour
    {
        private List<GameObject> PointList;

        public PointManager()
        {
            PointList = new List<GameObject>();
        }

        public GameObject MakeGO(Transform spawner, GameObject obModel, bool extension, Dictionary<GameObject, StructureType> objectTypes)
        {
            GameObject go;
            go = Instantiate(obModel) as GameObject;
            go.transform.SetParent(spawner);

            objectTypes.Add(go, StructureType.Point);
            return go;
        }

        public void DeleteGo(GameObject go)
        {
            PointList.Remove(go);
            Destroy(go);
        }

    }
}
