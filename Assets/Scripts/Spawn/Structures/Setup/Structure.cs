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

        public void AddElement(GameObject go, GameObject extensionRoot, SpawnOptions option) // object to add, object to extend
        {
            foreach(root r in structure.ToList())
            {
                if (r.point == extensionRoot)
                {
                    root rt;

                    if (option == SpawnOptions.Finish)
                    {
                        rt = NewRoot(go); // empty root for new object
                        structure.Add(rt);
                    }
                    else
                        rt = lastRoot;

                  

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



    }
}
