﻿using System;
using System.Collections.Generic;
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
            n = 1;
            LineList = new List<Line>();
        }


       public GameObject MakeGO(Transform spawner,GameObject obModel, Dictionary<GameObject, StructureType> objectTypes)
        {
            GameObject go;
            go = Instantiate(obModel) as GameObject;
            go.transform.SetParent(spawner);

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
                n = 1;
            }
            n++;


            objectTypes.Add(go, StructureType.Line);
            return go;
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
