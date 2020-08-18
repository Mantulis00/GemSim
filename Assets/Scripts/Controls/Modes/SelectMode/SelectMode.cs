using Assets.Scripts.Controls.Modes.Groups;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Assets.Scripts.Controls.Modes.SelectMode
{
    class SelectMode
    {
        private O_Mause o_mouse;
        private SpawnerManager spawner;

        private SelectManager selectManager;
         
        public SelectMode(O_Mause o_mouse, SpawnerManager spawner)
        {
            this.o_mouse = o_mouse;
            this.spawner = spawner;

            selectManager = new SelectManager(spawner);
        }

        public void CreateGroup( string groupName, ListType type)
        {
            selectManager.CreateList(groupName, ListType.FixedPoints);
        }
        
 

        public void AddToGroup(string groupName, GameObject go)
        {
            //if ((o_mouse.a_que & (ActionsQue.ListAdd | ActionsQue.ListRemove)) != (ActionsQue.ListAdd | ActionsQue.ListRemove)) return;

            //IGroup group = selectManager.GetGroup(groupName);
            //if (group == null) return;

            if ((o_mouse.a_que & ActionsQue.ListAdd) == ActionsQue.ListAdd)
            {
                selectManager.Add(groupName, go);
                o_mouse.ActionAddressed(ActionsQue.ListAdd);
            }
            else if ((o_mouse.a_que & ActionsQue.ListRemove) == ActionsQue.ListRemove)
            {
                selectManager.Remove(groupName, go);
                o_mouse.ActionAddressed(ActionsQue.ListRemove);
            }
        }

        public void RemoveFromGroup()
        {

        }

        public void GetList()
        {

        }

        public void RenameList()
        {

        }

        public void RemoveList()
        {

        }





    }
}
