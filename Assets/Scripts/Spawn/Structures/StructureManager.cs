using Assets.Scripts.Spawn.Structures.Setup;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.Scripts.Spawn.Structures.Setup.Structure;

namespace Assets.Scripts.Spawn
{
    class StructureManager : MonoBehaviour
    {
        public GameObject MakeGO(Transform spawner, GameObject obModel,  SpawnOptions option)
        {
            GameObject go;
            go = Instantiate(obModel) as GameObject;
            go.transform.SetParent(spawner);

             go.name = option.ToString();


            return go;
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

                    return connectionsList;
                }
            }
        return null;
        }


        internal bool CheckConnector(GameObject go, Structure structure)
        {
            return structure.CheckConnector(go);
        }

        internal void MergeStructures(Structure from, Structure to)
        {
            foreach(root r in from.structure)
            {
                to.structure.Add(r);
            }
            foreach(GameObject c in from.connectors)
            {
                to.connectors.Add(c);
            }
            from.structure.Clear();
            from.connectors.Clear();
        }

    }
}
