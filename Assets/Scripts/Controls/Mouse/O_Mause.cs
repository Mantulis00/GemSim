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
        private struct spawnDetails
        {
            int n_element;
            Vector3 coords;
        };


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
            MouseActions action = MouseControls.DetectClick(false);

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
                    a_que |= ActionsQue.SpawnStart;
                    noHoldLastFrame = false;
                }
            }

            else if (!noHoldLastFrame)
            {
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
