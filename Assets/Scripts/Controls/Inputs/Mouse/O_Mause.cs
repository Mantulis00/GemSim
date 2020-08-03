﻿using System;
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


        public void Update(KeyboardAction action)
        {

            if (action == KeyboardAction.Spawn && selectedObjet != null)
            {
                SpawnOptions();
            }
            else if (action == KeyboardAction.Move && selectedObjet != null)
            {
                MoveOptions();
            }
            else
            {
                SelectionOptions(); // space to select object
            }


        }

        // Mouse
        private void SelectionOptions()
        {
            MouseAction action = MouseControls.DetectClick(false); // to be done if holdind press or hit press

            if (action == MouseAction.Select)
            {
                SelectObject();
            }
            else if (action == MouseAction.Unselect)
            {
                selectedObjet = null;
            }
        }

        private void MoveOptions()
        {
            if (MouseControls.DetectClick(true) == MouseAction.Select) // til holding click
            {
                if ((a_que & ActionsQue.Move) != ActionsQue.Move)
                {
                    clickCoords_s = MouseControls.GetMouseLocation();

                    a_que |= ActionsQue.Move;
                }
            }
        }

        private void SpawnOptions()
        {
            if (MouseControls.DetectClick(true) == MouseAction.Select) // til holding click
            {
                if (noHoldLastFrame) // after 1st frame doesnt enter
                {
                    //SelectionOptions(); // to check if try to spawn on same object


                    clickCoords_s = MouseControls.GetMouseLocation();

                    a_que |= ActionsQue.SpawnStart;
                    noHoldLastFrame = false;
                }
            }

            else if (!noHoldLastFrame) // when stopped clicking
            {
                clickCoords_f = MouseControls.GetMouseLocation();

                a_que |= ActionsQue.SpawnFinish;
                noHoldLastFrame = true;
            }
            
        }

        internal GameObject  CheckForObject()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); ;
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
                return hit.transform.gameObject;
            
            return null;
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