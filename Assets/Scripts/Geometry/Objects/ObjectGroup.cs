using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Geometry.Objects
{
    public class ObjectGroup
    {
        private List<GameObject> goList;

        public ObjectGroup()
        {
            goList = new List<GameObject>();
        }

        public List<GameObject> GetList() // ?TBC to be able to have many list once
        {
            return goList;
        }

        public void Add(GameObject go)
        {
            go.GetComponent<Renderer>().material.color = Color.black; // for debugs

            if (!goList.Contains(go)) goList.Add(go);
        }

        public void Remove(GameObject go)
        {
            go.GetComponent<Renderer>().material.color = Color.white; // for debugs

            if (goList.Contains(go)) goList.Remove(go);
        }





    }
}
