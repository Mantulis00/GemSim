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

        public void CreateList(ListType type, string listName)
        {

        }
        

 

        public void AddToList(GameObject go, string listName)
        {
            IGroup list = selectManager.GetGroup(listName);
            if (list == null) return;

            if ((o_mouse.a_que & ActionsQue.ListAdd) == ActionsQue.ListAdd)
            {
                list.Add(go, spawner.GetStructure(go));
                o_mouse.ActionAddressed(ActionsQue.ListAdd);
            }
            else if ((o_mouse.a_que & ActionsQue.ListRemove) == ActionsQue.ListRemove)
            {
                list.Remove(go, spawner.GetStructure(go));
                o_mouse.ActionAddressed(ActionsQue.ListRemove);
            }
        }

        public void RemoveFromList()
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
