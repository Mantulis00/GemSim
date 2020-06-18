﻿using UnityEngine;

namespace Assets.Scripts.Controls
{
    /// <summary>
    /// Spawn objects
    /// rotate objects
    /// delete objects
    /// ray cast
    /// </summary>


    class O_Mause : MonoBehaviour
    {
        public GameObject selectedObjet; // temp public, later selected object

        private float rotationSensitivity = 50f;

        private void Start()
        {

        }
        private void Update()
        {
            MouseOptions();
            RotateObject();
        }

        // Mouse
        private void MouseOptions()
        {
            MouseActions action = MouseControls.DetectClick();

            if (action == MouseActions.Select)
            {
                SelectObject();
            }
            else if (action == MouseActions.Unselect)
            {
                selectedObjet = null;
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


        // Keyboard
        private void RotateObject()
        {
            if (selectedObjet == null) return;

            KeyboardActions action = KeyboardControls.GetRotationInput();

            Vector3 rotation = new Vector3();
            rotation = selectedObjet.transform.rotation.eulerAngles;
            //rotation = TransformHalfToFull(rotation);

            Debug.Log(rotation);


            // horizontal
            if ((action & KeyboardActions.RotateLeft) == KeyboardActions.RotateLeft)
            {
                rotation.y += rotationSensitivity * Time.deltaTime;
            }
            else if((action & KeyboardActions.RotateRight) == KeyboardActions.RotateRight)
            {
                rotation.y -= rotationSensitivity * Time.deltaTime;
            }

            // vertical
            if ((action & KeyboardActions.RotateUp) == KeyboardActions.RotateUp)
            {
                if (rotation.z > 1)
                     rotation.x += rotationSensitivity * Time.deltaTime;
                else
                    rotation.x -= rotationSensitivity * Time.deltaTime;
            }
            else if ((action & KeyboardActions.RotateDown) == KeyboardActions.RotateDown)
            {
                if (rotation.z > 1)
                    rotation.x -= rotationSensitivity * Time.deltaTime;
                else
                    rotation.x += rotationSensitivity * Time.deltaTime;
            }



            selectedObjet.transform.rotation = Quaternion.Euler(rotation);
        }

        private Vector3 TransformHalfToFull(Vector3 rotation)
        {
            if (rotation.x >=180)
            {
                rotation.x += 360;
            }

            if (rotation.y >= 180)
            {
                rotation.y += 360;
            }


            return rotation;
        }


    }
}
