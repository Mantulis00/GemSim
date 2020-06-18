using System.Xml;
using UnityEngine;

namespace Assets.Scripts.Controls
{
     class O_Camera : MonoBehaviour
    {
        public Camera cam;

       private CameraControls controls;

        private Vector3 camRotation, rotation;

        private void Start()
        {
            controls = new CameraControls();
            camRotation = cam.transform.rotation.eulerAngles;
            controls.Start();
        }
        private void Update()
        {
            RotateCamera();
        }

        private void RotateCamera()
        {
            if (controls.DetectRotation()) // if change detected rotate
            {
                 rotation = new Vector3(
                    controls.mouseMovementValue.y + camRotation.x,
                    controls.mouseMovementValue.x + camRotation.y,
                    0);

                cam.transform.rotation = Quaternion.Euler(rotation);  
               
            }
            if (controls.awaitingRegister) // if stoped rotating save rotation
            {
                camRotation = rotation;
                controls.SetMovementRegister();
            }

        }


    }
}
