
using UnityEngine;

namespace Assets.Scripts.Controls.Keyboard
{
    class O_Keyboard
    {
        private float rotationSensitivity = 50f;
        public KeyboardActions action { get; private set; }


        public void Update(GameObject selectedObject)
        {
            GetKeyboardAction();

            if (action == KeyboardActions.Rotate)
                RotateObject(selectedObject);
            
        }

        private void GetKeyboardAction()
        {
            KeyboardActions action = KeyboardControls.GetKeyboardAction();

            // if nothing pressed dont do anything  
            //can change action state only if action state is on reset
            if (action == KeyboardActions.Hold || 
                !(this.action == KeyboardActions.Reset || action == KeyboardActions.Reset)) return;


            this.action = action;
        }

        private void RotateObject(GameObject selectedObject)
        {
            if (selectedObject == null) return; // if there is nothing to rotate return back

            KeyboardRotations action = KeyboardControls.GetRotationInput();

            Vector3 rotation = new Vector3();
            rotation = selectedObject.transform.rotation.eulerAngles;

            // horizontal
            if ((action & KeyboardRotations.RotateLeft) == KeyboardRotations.RotateLeft)
            {
                rotation.y += rotationSensitivity * Time.deltaTime;
            }
            else if ((action & KeyboardRotations.RotateRight) == KeyboardRotations.RotateRight)
            {
                rotation.y -= rotationSensitivity * Time.deltaTime;
            }

            // vertical
            if ((action & KeyboardRotations.RotateUp) == KeyboardRotations.RotateUp)
            {
                if (rotation.z > 1) // adjusted for "Unity angles"
                    rotation.x += rotationSensitivity * Time.deltaTime;
                else
                    rotation.x -= rotationSensitivity * Time.deltaTime;
            }
            else if ((action & KeyboardRotations.RotateDown) == KeyboardRotations.RotateDown)
            {
                if (rotation.z > 1)
                    rotation.x -= rotationSensitivity * Time.deltaTime;
                else
                    rotation.x += rotationSensitivity * Time.deltaTime;
            }



            selectedObject.transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
