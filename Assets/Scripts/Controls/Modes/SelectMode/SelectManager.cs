

using Assets.Scripts.Controls.Modes.Groups;
using Assets.Scripts.Spawn.Structures.Setup;
using System.Collections.Generic;
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

        public void AddToList(string groupName, GameObject go)
        {
            IGroup group = groupsDictionary[groupName];

            CheckList(group);
            if (!CheckStructure(groupsDictionary[groupName], go)) return;

            group.Add(go, spawner.GetStructure(go));
        }

        public void RemoveFromList(string groupName, GameObject go)
        {
            IGroup group = groupsDictionary[groupName];

            CheckList(group);
            if (!CheckStructure(groupsDictionary[groupName], go)) return;

            group.Remove(go, spawner.GetStructure(go));
        }

        public IGroup GetGroup(string groupName)
        {
            if (groupsDictionary.ContainsKey(groupName)) return groupsDictionary[groupName];
            return null;
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

        private void CheckList(IGroup group)
        {
            Structure str = spawner.GetStructure(group.GetCheckObject());
            if (group.structure == null || str != group.structure) group.ChangeStructure(str); // if first object is of different structure than group, that mean first object changed structure
        }

        private bool CheckStructure(IGroup group, GameObject go)
        {
            if (spawner.GetStructure(go) == group.structure) return true;

            return false;
        }


    }
}
