using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GUI.Objects.SingleTexture
{
    public class SingleTexture
    {
        // private GameObject lastObject ;
        //  private Color lastColor;

        private struct lastOptions
        {
            internal GameObject lastObject;
            internal Color lastColor;
        }

        lastOptions lastOption;

        public SingleTexture()
        {
            lastOption = new lastOptions();
            lastOption.lastObject = null;
            lastOption.lastColor = Color.white;
        }

        public void ResetLastColor()
        {
            lastOption.lastObject.GetComponent<Renderer>().material.color = Color.white;
        }

        public void ChangeColor(GameObject go, Color color)
        {
            /// if nothing changes do nothing
            /// if color changes remove old colors from old objects and apply new colors to new objects
            if (go == lastOption.lastObject && color == lastOption.lastColor) return;
            if (lastOption.lastObject != null) ResetLastColor();


             go.GetComponent<Renderer>().material.color = color;
            


            lastOption.lastObject = go;
        }


    }
}
