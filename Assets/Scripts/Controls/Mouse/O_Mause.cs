using System;
using UnityEngine;

namespace Assets.Scripts.Controls
{
    /// <summary>
    /// Spawn objects
    /// rotate objects
    /// delete objects
    /// ray cast
    /// </summary>

    class O_Mause
    {
        public GameObject selectedObjet { get; private set; }
        private bool noHoldLastFrame = true;

        public ActionsQue a_que { get; private set; }
        public Vector2 clickCoords_s, clickCoords_f; // s - start clicking , f - finish clicking


        public O_Mause()
        {
            a_que = 0;
        }

        public void ActionAddressed(ActionsQue action)
        {
            Debug.Log(action);
            a_que -= action;

        }


        public void Update(KeyboardActions action)
        {

            if (action == KeyboardActions.Spawn)
            {
                SpawnOptions();
            }

            else
            {
                SelectionOptions();
            }


        }

        // Mouse
        private void SelectionOptions()
        {
            MouseActions action = MouseControls.DetectClick(false); // to be done if holdind press or hit press

            if (action == MouseActions.Select)
            {
                SelectObject();
            }
            else if (action == MouseActions.Unselect)
            {
                selectedObjet = null;
            }
        }

        private void SpawnOptions()
        {
            if (MouseControls.DetectClick(true) == MouseActions.Select)
            {
                if (noHoldLastFrame)
                {
                    clickCoords_s = MouseControls.GetMouseLocation();

                    a_que |= ActionsQue.SpawnStart;
                    noHoldLastFrame = false;
                }
            }

            else if (!noHoldLastFrame)
            {
                clickCoords_f = MouseControls.GetMouseLocation();

                a_que |= ActionsQue.SpawnFinish;
                noHoldLastFrame = true;
            }
            
        }


        private void SelectObject()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); ;
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                 selectedObjet = hit.transform.gameObject;
            }
        }



    }
}
