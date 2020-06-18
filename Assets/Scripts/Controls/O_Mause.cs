using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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


        private void Start()
        {
            
        }
        private void Update()
        {
            MouseControls.DetectClick();
        }



    }
}
