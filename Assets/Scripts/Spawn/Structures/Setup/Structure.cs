using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

namespace Assets.Scripts.Spawn.Structures.Setup
{
    public class Structure // List of points, each point can have connections
    {
        public List<root> structure;

        connection floatingConnection;

        public class root
        {
            public GameObject point; // main point
            public List<connection> connections; // this points extensions
        }

       public  struct connection
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
            if (rt == null) return;

            if (rt.connections.Count == 0 || rt.connections[rt.connections.Count-1].endPoint == null) // 
            {
                floatingConnection = new connection();
                floatingConnection.endPoint = go;
            }
            else
            {
                floatingConnection.connector = go;
                rt.connections.Add(floatingConnection);
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
                    root rt = NewRoot(go);
                    FillConnections(r, go);
                    structure.Add(rt);
                }
            }
        }



    }
}
