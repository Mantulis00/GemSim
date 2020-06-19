using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

namespace Assets.Scripts.Controls
{
    class ManagerControls:MonoBehaviour
    {
        public Camera cam;


        O_Camera o_camera;
        O_Mause o_mouse;

        private void Start()
        {
            o_camera = new O_Camera(cam);
            o_mouse = new O_Mause();
        }
        private void Update()
        {
            o_camera.Update();
            o_mouse.Update();
        }


    }
}
