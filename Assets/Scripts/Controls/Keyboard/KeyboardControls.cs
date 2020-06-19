using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controls
{
    static class KeyboardControls
    {
         public static KeyboardActions GetRotationInput()
        {
            KeyboardActions action = 0;

            if (Input.GetKey(KeyCode.A))  action += (int)KeyboardActions.RotateLeft;
            else if (Input.GetKey(KeyCode.D))  action += (int)KeyboardActions.RotateRight;
            if (Input.GetKey(KeyCode.W))  action += (int)KeyboardActions.RotateUp;
            else if (Input.GetKey(KeyCode.S))  action += (int)KeyboardActions.RotateDown;

            return action;
        }


    }
}
