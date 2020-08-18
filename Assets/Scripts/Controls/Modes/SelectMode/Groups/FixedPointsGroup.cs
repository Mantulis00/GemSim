using Assets.Scripts.Spawn;
using Assets.Scripts.Spawn.Structures.Setup;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controls.Modes.Groups
{
    public class FixedPointsGroup : IGroup
    {
        private List<GameObject> goList;
        public Structure structure { get; private set; }

        public FixedPointsGroup()
        {
            goList = new List<GameObject>();
            this.structure = null;
        }

        public List<GameObject> GetList() // ?TBC to be able to have many list once
        {
            return goList;
        }

        public void Add(GameObject go, Structure structure) // ?TBC not rly a place or is it ?
        {
            if (goList.Contains(go)) return;
            if (!CheckStructure(structure)) return;




            structure.GetRootsFromPoints(go).dataPoint.type = StructurePointType.Fixed;


            go.GetComponent<Renderer>().material.color = Color.black; // for debugs

            goList.Add(go);
        }

        public void Remove(GameObject go, Structure structure)
        {
            if (!goList.Contains(go)) return;

            structure.GetRootsFromPoints(go).dataPoint.type = StructurePointType.Loose;

            go.GetComponent<Renderer>().material.color = Color.white; // for debugs

            goList.Remove(go);
        }

        private bool CheckStructure(Structure structure) // check if group structure is the same as first objects structure, becouse structure of object could have changed
        {
            if (goList.Count != 0 && this.structure != structure) // check if group is of same structure as given object
            {
                return false;
            }
            return true;
        }

        public void ChangeStructure(Structure structure) 
        {
            this.structure = structure;
        }

        public GameObject GetCheckObject()
        {
            if (goList.Count != 0) return goList[0];
            else return null;
        }



    }
}
