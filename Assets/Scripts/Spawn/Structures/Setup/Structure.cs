using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

namespace Assets.Scripts.Spawn.Structures.Setup
{
    public class Structure // List of points, each point can have connections
    {
        public List<root> structure;


        public class root
        {
            public GameObject point; // main point
            public List<connection> connections; // this points extensions
        }

       public  class connection
        {
           public GameObject endPoint;
           public GameObject connector;
        }

        public Structure(GameObject go)
        {
            structure = new List<root>();
            NewRoot(go);


        }

        private root NewRoot(GameObject go)
        {
            root initRoot = new root();
            initRoot.point = go;
            initRoot.connections = new List<connection>();
            structure.Add(initRoot);

            return initRoot;
        }

        private void FillConnections(root rt, GameObject go)
        {
            Debug.Log("he" + rt);
            if (rt == null) return;

            if  (rt.connections.Count == 0 || rt.connections[rt.connections.Count-1].connector != null) // 
            {
                connection floatingConnection = new connection();
                floatingConnection.endPoint = go;
                rt.connections.Add(floatingConnection);
            }
            else
            {
                rt.connections[rt.connections.Count - 1].connector = go;
            }
        }

        private root FindRoot(GameObject go)
        {
            foreach(root rt in structure)
            {
                if (rt.point == go)
                {
                    return rt;
                }
            }

            return null;
        }

        public void AddElement(GameObject go, GameObject extensionRoot) // object to add, object to extend
        {
            foreach(root r in structure.ToList())
            {
                if (r.point == extensionRoot)
                {
                    root rt = NewRoot(go); // empty root for new object
                    structure.Add(rt);
                    FillConnections(r, go); // modify roots connections
                    Debug.Log(r.connections.Count);
                    break;
                }
            }
        }



    }
}
