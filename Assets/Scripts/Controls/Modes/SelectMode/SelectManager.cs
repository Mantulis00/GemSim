

using Assets.Scripts.Controls.Modes.Groups;
using Assets.Scripts.Spawn.Structures.Setup;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;

namespace Assets.Scripts.Controls.Modes.SelectMode
{
    class SelectManager
    {

        private Dictionary<string, IGroup> groupsDictionary;
        private SpawnerManager spawner;

        public SelectManager(SpawnerManager spawner)
        {
            groupsDictionary = new Dictionary<string, IGroup>();
            this.spawner = spawner;
        }


        public void CreateList(string groupName, ListType type )
        {
            if (type == ListType.FixedPoints)
            {
                IGroup group = new FixedPointsGroup();

                if (!groupsDictionary.ContainsKey(groupName)) groupsDictionary.Add(groupName, group); // check if list already has that member
            }
        }

        public void Add(string groupName, GameObject go)
        {
            IGroup group = groupsDictionary[groupName];

            CheckGroup(group);
            if (!CheckStructure(groupsDictionary[groupName], go)) return;
            
            foreach(Structure.root r in spawner.GetStructure(go).structure) // temp
            {
                foreach(Structure.connection c in r.connections)
                {
                    c.dataConnection.tensionCoefficient = 50f;
                }
            }


            group.Add(go, spawner.GetStructure(go));
        }

        public void Remove(string groupName, GameObject go)
        {
            IGroup group = groupsDictionary[groupName];

            CheckGroup(group);
            if (!CheckStructure(groupsDictionary[groupName], go)) return;


            foreach (Structure.root r in spawner.GetStructure(go).structure) //temp
            {
                foreach (Structure.connection c in r.connections)
                {
                    c.dataConnection.tensionCoefficient = 1f;
                }
            }



            group.Remove(go, spawner.GetStructure(go));
        }

        public IGroup GetGroup(string groupName)
        {
            if (groupsDictionary.ContainsKey(groupName)) return groupsDictionary[groupName];
            return null;
        }

        public List<string> GetGroups()
        {
            List<string> list = new List<string>();
            foreach(string s in groupsDictionary.Keys)
            {
                list.Add(s);
            }
            return list;
        }

        public void RenameList(string groupName, string newgroupName)
        {
            if (groupsDictionary.ContainsKey(groupName))
            {
                groupsDictionary.Add(newgroupName, groupsDictionary[groupName]);
                RemoveList(groupName);
            }
        }

        public void RemoveList(string groupName)
        {
            if (groupsDictionary.ContainsKey(groupName)) groupsDictionary.Remove(groupName);
        }



        private void CheckGroup(IGroup group)
        {
            if (group.GetCheckObject() == null) return;
            Structure str = spawner.GetStructure(group.GetCheckObject());
            if (group.structure == null || str != group.structure) group.ChangeStructure(str); // if first object is of different structure than group, that mean first object changed structure
        }

        private bool CheckStructure(IGroup group, GameObject go)
        {
            Structure goStructure = spawner.GetStructure(go);

            if (goStructure == group.structure) return true;
            else if (group.structure == null)
            {
                group.ChangeStructure(goStructure);
                return true;
            }


            return false;
        }


    }
}
