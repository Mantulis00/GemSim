using Assets.Scripts.Physix.Models;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Spawn.Structures.Setup
{
    public class Structure : IStructure // List of points, each point can have connections
    {
        public List<root> structure { get; private set; }// structure is made out of root elements
        private root lastRoot;
        public List<GameObject> connectors { get; private set; }

        public Dictionary<GameObject, connection> EndPoinsOfConnection;



        public class root // every root element has main object (root) and connections (point to which connection is led to)
        {
            public GameObject point; // main point
            public pointData dataPoint;
            public List<connection> connections; // this points extensions
            public PhysixData physixData;
        }

        public class connection // if we have two elements both of them will have connections to each other
        {
            public GameObject endPoint;
            public GameObject connector;
            public connectionData dataConnection;
            public PhysixData physixData;
        }

        public class connectionData // elastic, solid // lenght, streach etc. ?TBA
        {
            public double originalLenght;
            public double realLenght;
            public float tensionCoefficient;

            public connectionData()
            {
                tensionCoefficient = 1f; // !CALH 
            }
        }

        public class pointData // fixed / movable etc ?TBA
        {
           public StructurePointType type;
           public Vector3 originalPossition;

            public pointData(Vector3 location, StructurePointType type)
            {
                this.type = type;
                originalPossition = location;
            }
        }




        public Structure(GameObject go)
        {
            structure = new List<root>();
            connectors = new List<GameObject>();
            EndPoinsOfConnection = new Dictionary<GameObject, connection>();
            NewRoot(go);
        }



        private root NewRoot(GameObject go)
        {
            root initRoot = new root();
            initRoot.point = go;
            initRoot.connections = new List<connection>();

            initRoot.dataPoint = new pointData(
                initRoot.point.transform.position,
                StructurePointType.Loose); // !CALH - constants are left here

            //physix
            initRoot.physixData = new PhysixData();



            structure.Add(initRoot);

            return initRoot;
        }

        private void FillConnections(root rt, GameObject go, SpawnOptions option)
        {

            if (option == SpawnOptions.Finish) // spawned new point -> give point root -> (give point connection to point from which it was spawned)
            {
                connection floatingConnection = new connection();
                floatingConnection.endPoint = go;
                rt.connections.Add(floatingConnection);

            }
            else // give just spawner point connector object
            {
                connection lastConnection = rt.connections[rt.connections.Count - 1];

                lastConnection.connector = go; // to add object reference to connector

                // datas
                lastConnection.dataConnection = GetConnectionData(rt.point, lastConnection.endPoint); // to fill connection data
                //physics
                lastConnection.physixData = new PhysixData();
            }
        }

        private connectionData GetConnectionData(GameObject from, GameObject to)
        {
            connectionData cData = new connectionData();
            double lenght = System.Math.Round((from.transform.position - to.transform.position).magnitude, 4); //constans lol
            cData.originalLenght = lenght;
            cData.realLenght = lenght;

            return cData;
        }


        public void AddElement(GameObject go, GameObject extensionRoot, SpawnOptions option) // object to add, object to extend, option
        {
            foreach (root r in structure.ToList())
            {
                if (r.point == extensionRoot)
                {
                    root rt;

                    if (option == SpawnOptions.Finish) // spawned new point -> give point root
                    {
                        rt = NewRoot(go); // prefilled root for structure

                        //connectionData cData = new connectionData();



                        FillConnections(rt, extensionRoot, option); // extension root is structures point from which we expand
                    }
                    else // else for connectors // spawned new point -> give point root -> give point connector object
                    {
                        rt = lastRoot; // last frame created root saved it as last root
                        connectors.Add(go); // go is my new connector connecting last root and now selected root

                        FillConnections(rt, go, option); // gives connection to last time filled rt
                    }


                    FillConnections(r, go, option);// give point from which we expended connection to point which we expended or give connector to now selected point

                    lastRoot = rt;
                    break;
                }
            }
        }

        public void AddConnection(GameObject go, GameObject from, GameObject to)
        {
            if (from == to) return;

            foreach (root r in structure.ToList()) // goes through all structure
            {
                bool toDone = false, fromDone = false;


                if (r.point == from && !fromDone) // when finds point given point
                {
                    FillConnections(r, to, SpawnOptions.Finish); // gives him connection to other given object
                    FillConnections(r, go, SpawnOptions.Connection); // gives connector (just object) to connection
                    fromDone = true;
                }
                if (r.point == to && !toDone) // same with other object (if a has connection to b -> b has to a)
                {
                    FillConnections(r, from, SpawnOptions.Finish);
                    FillConnections(r, go, SpawnOptions.Connection);
                    toDone = true;
                }
                if (toDone && fromDone) break;
            }
        }


        internal bool CheckConnector(GameObject go) // check if object is connector type
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


       
       
        private void RemoveDuplicates(List<GameObject> goList) // mayb e not needed
        {
            for (int x=0; x<goList.Count; x++)
            {
                for (int y=x+1; y<goList.Count; y++)
                {
                    if (goList[x] == goList[y]) goList.RemoveAt(y);
                }
            }
        }

        private void SearchWide(List<GameObject> inList, GameObject go) // simple search wide for structure connection endpoints
        {

            foreach (root r in structure.ToList())
            {
                if (r.point == go)
                {
                    foreach (connection c in r.connections.ToList())
                    {
                        if (!inList.ToList().Contains(c.endPoint))
                        {
                            inList.Add(c.endPoint);
                            SearchWide(inList, c.endPoint);
                        }

                    }

                    return;
                }
            }

        }


        internal List<GameObject> GetConnectedPoints(GameObject go, GameObject goAround) // get whole branch of connections
        {
            List<GameObject> inList = new List<GameObject>();
            inList.Add(goAround);
            inList.Add(go);

            SearchWide(inList, go);

            inList.Remove(goAround);
            inList.Remove(go);


            return inList;
        }

      

        internal List<root> GetRootsFromPoints(List<GameObject> goList) // if you have go, find root whichs point is your go
        {
            List<root> rootList = new List<root>();

            foreach (GameObject go in goList)
            {
                foreach(root r in structure)
                {
                    if (r.point == go)
                    {
                        rootList.Add(r);
                        break;
                    }
                }
            }
            return rootList;
        }

        internal root GetRootsFromPoints(GameObject go) //overloaded version for 1 go not list
        {
                foreach (root r in structure)
                {
                    if (r.point == go)
                    {
                        return r;
                    }
                }
            
            return null;
        }




        internal connection FindOtherSideOfConnection(GameObject from, GameObject to) // if you have to object A -> B, find connection B -> A
        {
            foreach(root r in structure.ToList())
            {
                if (r.point == to)
                {
                    foreach(connection c in r.connections.ToList())
                    {
                        if (c.endPoint == from)
                            return c;
                    }
                }
            }
            return null;
        }





    }
}
