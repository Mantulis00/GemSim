using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using UnityEngine;

namespace Assets.Scripts.Spawn.Structures.Setup
{
    public class Structure // List of points, each point can have connections
    {
        public List<root> structure; // structure is made out of root elements
        private root lastRoot;
        public List<GameObject> connectors;



        public class root // every root element has main object (root) and connections (point to which connection is led to)
        {
            public GameObject point; // main point
            public List<connection> connections; // this points extensions
        }

       public  class connection // if we have two elements both of them will have connections to each other
        {
           public GameObject endPoint;
           public GameObject connector;
        }

        public Structure(GameObject go)
        {
            structure = new List<root>();
            connectors = new List<GameObject>();
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

        private void FillConnections(root rt, GameObject go, SpawnOptions option)
        {

            if  (option == SpawnOptions.Finish) // 
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


        public void AddElement(GameObject go, GameObject extensionRoot, SpawnOptions option) // object to add, object to extend, option
        {
            foreach(root r in structure.ToList())
            {
                if (r.point == extensionRoot)
                {
                    root rt;

                    if (option == SpawnOptions.Finish)
                    {
                        rt = NewRoot(go); // prefilled root for new object 
                        structure.Add(rt);
                    }
                    else // else for connectors
                    {
                        rt = lastRoot;
                        connectors.Add(go);
                    }
  
                    if (option == SpawnOptions.Finish)
                         FillConnections(rt, extensionRoot, option); // modify roots of newly added element
                    else
                        FillConnections(rt, go, option);
                    // create new element and add it to structure and add partially filled connection to it
                    // add connector to element from which element expended


                    FillConnections(r, go, option);// modify roots connections of expended element

                    lastRoot = rt;
                    break;
                }
            }
        }

        public void AddConnection(GameObject go, GameObject from, GameObject to)
        {
            if (from == to) return;

            foreach (root r in structure.ToList())
            {
                bool toDone = false, fromDone = false;


                if (r.point == from  && !fromDone)
                {
                    FillConnections(r, to, SpawnOptions.Finish);
                    FillConnections(r, go, SpawnOptions.Connection);
                    fromDone = true;
                }
                if (r.point == to && !toDone)
                {
                    FillConnections(r, from, SpawnOptions.Finish);
                    FillConnections(r, go, SpawnOptions.Connection);
                    toDone = true;
                }
                if (toDone && fromDone) break;
            }
        }


        internal bool CheckConnector(GameObject go)
        {
            foreach (GameObject g in connectors.ToList())
            {
                if (g == go)
                {
                    return true;
                }
            }

            return false;
        }





    }
}
