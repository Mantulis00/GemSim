﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controls
{
    static class MouseControls 
    {
        public static  void DetectClick()
        {
            if (Input.GetMouseButtonDown(0))
                Debug.Log("Pressed primary button.");
        }

         

    }
}
