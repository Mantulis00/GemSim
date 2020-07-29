using System.Xml;
using UnityEngine;

namespace Assets.Scripts.Controls
{
     class O_Camera
    {
        private Camera cam;
        private CameraControls controls;

        private Vector3 camRotation, rotation;

        public  O_Camera(Camera cam)
        {
            this.cam = cam;
            controls = new CameraControls();
            camRotation = cam.transform.rotation.eulerAngles;
            controls.Start();
        }
        public void Update()
        {
            RotateCamera();
        }

        private void RotateCamera()
        {
            if (controls.DetectRotation()) // if change detected rotate
            {
                 rotation = new Vector3(
                    -controls.mouseMovementValue.y + camRotation.x,
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
