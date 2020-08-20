using Assets.Scripts.Controls.Modes.Groups;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Assets.Scripts.Controls.Modes.SelectMode
{
    class SelectMode
    {
        private O_Mause o_mouse;

        private SelectManager selectManager;

        public GameObject secondarySelect { get; private set; } // temp

        public SelectMode(O_Mause o_mouse, SpawnerManager spawner)
        {
            this.o_mouse = o_mouse;
            selectManager = new SelectManager(spawner);
        }

        public void CreateGroup( string groupName, ListType type)
        {
            selectManager.CreateList(groupName, ListType.FixedPoints);
        }
        
        public void SelectSecondary()
        {
            if ((o_mouse.a_que & ActionsQue.Select) == ActionsQue.Select)
            {
                if (secondarySelect != null) secondarySelect.GetComponent<Renderer>().material.color = Color.white;
                secondarySelect = o_mouse.selectedObject;
                o_mouse.selectedObject.GetComponent<Renderer>().material.color = Color.cyan;
                o_mouse.ActionAddressed(ActionsQue.Select);

            }
        }

        public void AddToGroup(string groupName, GameObject go)
        {

            if ((o_mouse.a_que & ActionsQue.GroupAdd) == ActionsQue.GroupAdd)
            {
                selectManager.Add(groupName, go);
                o_mouse.ActionAddressed(ActionsQue.GroupAdd);
            }
            else if ((o_mouse.a_que & ActionsQue.GroupRemove) == ActionsQue.GroupRemove)
            {
                selectManager.Remove(groupName, go);
                o_mouse.ActionAddressed(ActionsQue.GroupRemove);
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
