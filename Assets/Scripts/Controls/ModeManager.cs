using Assets.Misq;
using Assets.Scripts.Controls.Keyboard;
using Assets.Scripts.Controls.Modes;
using Assets.Scripts.Controls.Modes.SelectMode;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controls
{
    class ModeManager : MonoBehaviour
    {
        public SpawnerManager spawner;
        internal Mode CurrentMode;

        private  O_Camera o_camera;
       private O_Mause o_mouse;
       private O_Keyboard o_keyboard;

       private EditMode editMode;
       private SimulationMode simulationMode;
        private SelectMode selectMode;


        private void Start()
        {
            o_camera = new O_Camera(Camera.main);
            o_mouse = new O_Mause();
            o_keyboard = new O_Keyboard();

            editMode = new EditMode(o_mouse, o_camera.cam.transform, spawner);
            simulationMode = new SimulationMode(o_mouse, o_camera.cam.transform, spawner);
            selectMode = new SelectMode(o_mouse, spawner);
            selectMode.CreateGroup("temp", ListType.FixedPoints); // temmp


            CurrentMode = Mode.Edit; // temp // or no ?
        }
        private void Update()
        {
            CheckMode();
            UpdateOStatus();

            if (o_mouse.a_que != 0)
                PerformActions();

        }

        private bool UpdateOStatus()
        {
            bool actionsExist = false;
            o_camera.Update();
            o_keyboard.Update(o_mouse.selectedObject);
            o_mouse.Update(o_keyboard.action);

            return actionsExist;
        }

        private void CheckMode()
        {
            if (o_keyboard.kSwitch != KeyboardSwitch.Hold)
            {
                if (o_keyboard.kSwitch == KeyboardSwitch.ChangeMode)
                {
                    CurrentMode  = (Mode)MisqEnum.RotateEnumValues((int)CurrentMode, typeof(Mode));
                    o_keyboard.AddressSwitch();
                }
            }
        }

        private void PerformActions()
        {
           if (CurrentMode == Mode.Edit)
            {
                if (o_mouse.selectedObject != null)
                {
                   // if (o_keyboard.action == KeyboardAction.Spawn)
                        editMode.Spawn();

                     if (o_keyboard.action == KeyboardAction.Move) // completely unnecessary 
                    {
                        editMode.Move();
                        MoveAdjustConnections(spawner.GetConnections(o_mouse.selectedObject)); // should be just b4 action addressed x inside mode
                    }
                        
                }
            }
           else if (CurrentMode == Mode.Simulate)
            {
                // if (o_keyboard.action == KeyboardAction.Move)
                // {



                /// pass GO around which it will move, 
                ///pass connection lenght between objects
                ///
                MoveAdjustConnections(spawner.GetConnections(o_mouse.selectedObject));
               if (selectMode.secondarySelect != null)
                simulationMode.Move(selectMode.secondarySelect, spawner.GetStructure(o_mouse.selectedObject)); // to be changed to select goAround
                    
                   
                    //simulationMode.Enlist(o_mouse.selectedObjet);
                   // MoveAdjustConnections(spawner.GetConnections(o_mouse.selectedObjet));

                //}

                



            }

           else if (CurrentMode == Mode.Select)
            {
                selectMode.SelectSecondary();
                if (o_mouse.selectedObject != null)
                    selectMode.AddToGroup("temp", o_mouse.selectedObject); //temp
            }

            
        }

        private void MoveAdjustConnections(List<Spawn.Structures.Setup.Structure.connection> connections) // change for edit and sim modes
        {
            foreach (Spawn.Structures.Setup.Structure.connection c in connections.ToList())
            {

                /// gtfo this somewhere else
                double lenght = Math.Round((c.endPoint.transform.position - o_mouse.selectedObject.transform.position).magnitude, 4);
                Spawn.Structures.Setup.Structure.connection ce = spawner.GetStructure(c.endPoint).FindOtherSideOfConnection(o_mouse.selectedObject, c.endPoint);

                ce.dataConnection.originalLenght = lenght; // does nothing
                ce.dataConnection.realLenght = lenght;

                Spawn.Structures.Setup.Structure.connection co = spawner.GetStructure(c.endPoint).FindOtherSideOfConnection( c.endPoint, o_mouse.selectedObject);
                co.dataConnection.originalLenght = lenght;
                co.dataConnection.realLenght = lenght;

                //co.connector.GetComponent<Renderer>().material.color = Color.white;
                ////

                SpawnerManager.MoveConnection( // do this for every connector object has
                      c.connector,
                      c.endPoint.transform.position,
                      o_mouse.selectedObject.transform.position);

            }
           
        }

    }
}
