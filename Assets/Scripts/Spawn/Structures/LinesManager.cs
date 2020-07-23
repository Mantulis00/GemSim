using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using UnityEngine;

namespace Assets.Scripts.Spawn
{
    public  class LinesManager : MonoBehaviour, IStructure
    {
        private List<Line> LineList;
        private Line currentLine;
        private byte n;
        

        private struct Line
        {
            internal GameObject start;
            internal GameObject finish;
            internal GameObject connect;
        }
        internal LinesManager()
        {
            n = 0;
            LineList = new List<Line>();
        }


       public GameObject MakeGO(Transform spawner,GameObject obModel, bool extension, Dictionary<GameObject, StructureType> objectTypes)
        {
            n++;
            if (extension)
            {
                currentLine = new Line();
                currentLine.start = obModel;
               // objectTypes.Add(obModel, StructureType.Line);
                return null;
            }
            else
            {
                GameObject go;
                go = Instantiate(obModel) as GameObject;
                go.transform.SetParent(spawner);

                go.name = LineList.Count.ToString();
                go.name += (n - 1).ToString();

                if (n == 1)
                {
                    currentLine = new Line();
                    currentLine.start = go;
                }
                else if (n == 2)
                {
                    currentLine.finish = go;
                }
                else
                {
                    currentLine.connect = go;
                    LineList.Add(currentLine);
                    n = 0;
                }
                objectTypes.Add(go, StructureType.Line);
                return go;
            }
        }

        public GameObject MakeGO2(Transform spawner, GameObject obModel, Dictionary<GameObject, StructureType> objectTypes) //needs renaming
        {
            GameObject go;
            go = Instantiate(obModel) as GameObject;
            go.transform.SetParent(spawner);

            objectTypes.Add(go, StructureType.Line);
            return go;
        }


            public void DeleteGo(GameObject go)
        {
            foreach (Line line in LineList)
            {
                if (line.start == go || line.finish == go)
                {
                    Destroy(line.connect);
                    if (line.start == go)
                    {
                        Destroy(line.start);
                      //  PointList.Add(line.finish);
                    }
                    else
                    {
                        Destroy(line.finish);
                      //  PointList.Add(line.start);
                    }
                    LineList.Remove(line);
                    break;
                }
            }
        }


        internal GameObject FindConnector(GameObject go)
        {
            foreach (Line line in LineList)
            {
                if (line.start == go || line.finish == go)
                {
                    return line.connect;
                }
            }
            return null;
        }

        internal bool CheckConnector(GameObject go)
        {
            foreach (Line line in LineList)
            {
                if (line.connect == go)
                {
                    return true;
                }
            }
            return false;
        }


        internal Transform FindSecondPointLocation(GameObject go)
        {
            foreach (Line line in LineList)
            {
                if (line.start == go)
                {
                    return line.finish.transform;
                }
                else if (line.finish == go)
                {
                    return line.start.transform;
                }
            }
            return null;
        }

       


    }
}
