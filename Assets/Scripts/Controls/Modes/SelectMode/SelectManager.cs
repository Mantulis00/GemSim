

using Assets.Scripts.Controls.Modes.Groups;
using System.Collections.Generic;

namespace Assets.Scripts.Controls.Modes.SelectMode
{
    class SelectManager
    {

        Dictionary<string, IGroup> listsDictionary;

        public SelectManager()
        {
            listsDictionary = new Dictionary<string, IGroup>();
        }


        public void CreateList(ListType type, string listName)
        {
            if (type == ListType.FixedPoints)
            {
                IGroup group = new FixedPointsGroup();

                if (!listsDictionary.ContainsKey(listName)) listsDictionary.Add(listName, group); // check if list already has that member
            }
        }

        public void AddToList()
        {

        }

        public void RemoveFromList()
        {

        }

        public IGroup GetList(string listName)
        {
            if (listsDictionary.ContainsKey(listName)) return listsDictionary[listName];
            return null;
        }

        public void RenameList()
        {

        }

        public void RemoveList()
        {

        }


    }
}
