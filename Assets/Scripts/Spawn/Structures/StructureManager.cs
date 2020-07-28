using Assets.Scripts.Spawn.Structures.Setup;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.Scripts.Spawn.Structures.Setup.Structure;

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

        internal GameObject FindConnector(Structure structure, GameObject goFrom, GameObject goTo)
        {
            foreach(root r in structure.structure.ToList())
            {
                if(r.point == goFrom)
                {
                    foreach(connection c in r.connections.ToList())
                    {
                        if (c.endPoint == goTo)
                        {
                            return c.connector;
                        }
                    }
                }
            }

            return null;
        }

        internal List<connection> GetConnections(GameObject go, Structure structure)
        {
            List<connection> connectionsList = new List<connection>();

            foreach(root r in structure.structure.ToList())
            {
               
                if (r.point == go)
                {
                    connectionsList = r.connections.ToList();
                    Debug.Log(connectionsList.Count);
                    return connectionsList;
                }
            }



            return null;
        }


        internal bool CheckConnector(GameObject go)
        {
            return false;
        }

        internal Transform FindSecondPointLocation(GameObject go)
        {
            return null;
        }





    }
}
