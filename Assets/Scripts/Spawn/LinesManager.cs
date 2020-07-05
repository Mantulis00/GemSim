using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Spawn
{
    public  class LinesManager : MonoBehaviour
    {
        List<Line> LineList;
        Line currentLine;

        struct Line
        {
            public GameObject start;
            public GameObject finish;
            public GameObject connect;
        }
        internal LinesManager()
        {
            LineList = new List<Line>();
        }


        internal GameObject MakeGO(Transform spawner,GameObject obModel, byte n)
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
            }

            return go;
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

        internal void DeleteGo(GameObject selectedObject)
        {

        }


    }
}
