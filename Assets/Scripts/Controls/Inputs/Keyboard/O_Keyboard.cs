
using UnityEngine;

namespace Assets.Scripts.Controls.Keyboard
{
    class O_Keyboard
    {
        private float rotationSensitivity = 50f;
        public KeyboardAction action { get; private set; }
        public KeyboardSwitch kSwitch { get; private set; }

        internal O_Keyboard()
        {
            action = KeyboardAction.Reset;
        }

        public void Update(GameObject selectedObject)
        {
            GetKeyboardAction();
            GetKeyboardSwitch();

            if (action == KeyboardAction.Rotate)
                RotateObject(selectedObject);
            
        }

        private void GetKeyboardAction()
        {
            KeyboardAction action = KeyboardControls.GetKeyboardAction();

            // if nothing pressed dont do anything  
            //can change action state only if action state is on reset
            if (action == KeyboardAction.Hold || 
                !(this.action == KeyboardAction.Reset || action == KeyboardAction.Reset)) return;


            this.action = action;
        }


        private void GetKeyboardSwitch()
        {
            KeyboardSwitch kSwitch = KeyboardControls.GetKeyboardSwitch();

            if (kSwitch == KeyboardSwitch.Hold) return;

            this.kSwitch = kSwitch;
        }

        public void AddressSwitch()
        {
            kSwitch = KeyboardSwitch.Hold;
        }


        // ----> move somewhere else


        private void RotateObject(GameObject selectedObject)
        {
            if (selectedObject == null) return; // if there is nothing to rotate return back

            KeyboardRotations action = KeyboardControls.GetRotationInput();

            Vector3 rotation = new Vector3();
            rotation = selectedObject.transform.rotation.eulerAngles;

            // horizontal
            if ((action & KeyboardRotations.RotateLeft) == KeyboardRotations.RotateLeft)
            {
                rotation.y -= rotationSensitivity * Time.deltaTime;
            }
            else if ((action & KeyboardRotations.RotateRight) == KeyboardRotations.RotateRight)
            {
                rotation.y += rotationSensitivity * Time.deltaTime;
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
