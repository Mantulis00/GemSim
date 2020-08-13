using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GUI.Objects.ManyTexture
{
    public  class ManyTexture
    {
       // private GameObject lastObject ;
      //  private Color lastColor;

        private struct lastOptionsMany
        {
            internal List<GameObject> lastObject;
            internal Color lastColor;
        }

        private struct lastOptionsSingle
        {
            internal GameObject lastObject;
            internal Color lastColor;
        }



        private lastOptionsMany lastOptionMany;
        private lastOptionsSingle lastOptionPivot, lastOptionPoint;

        public ManyTexture()
        {
            lastOptionMany = new lastOptionsMany();
            lastOptionMany.lastObject = null;
            lastOptionMany.lastColor = Color.white;

            lastOptionPivot = new lastOptionsSingle();
            lastOptionPivot.lastObject = null;
            lastOptionPivot.lastColor = Color.white;

            lastOptionPoint = new lastOptionsSingle();
            lastOptionPoint.lastObject = null;
            lastOptionPoint.lastColor = Color.white;
        }

        private  void ResetLastColor(lastOptionsMany many)
        {
            foreach(GameObject g in lastOptionMany.lastObject)
            {
                g.GetComponent<Renderer>().material.color = Color.white; 
            }
           
        }

        private void ResetLastColor(lastOptionsSingle single)
        {
                single.lastObject.GetComponent<Renderer>().material.color = Color.white;
        }


        public void ChangeColor(GameObject go, Color color)
        {
            /// if nothing changes do nothing
            /// if color changes remove old colors from old objects and apply new colors to new objects
            if (go == lastOptionMany.lastObject && color == lastOptionMany.lastColor) return;
            if (lastOptionMany.lastObject != null) ResetLastColor();

            foreach (GameObject g in go)
            {
                g.GetComponent<Renderer>().material.color = color;
            }


            lastOptionMany.lastObject = go;
        }


        public  void ChangeColor(List<GameObject> go, Color color)
        {
            /// if nothing changes do nothing
            /// if color changes remove old colors from old objects and apply new colors to new objects
            if (go == lastOptionMany.lastObject && color == lastOptionMany.lastColor) return; 
            if (lastOptionMany.lastObject != null) ResetLastColor(lastOptionMany);

            foreach(GameObject g in go)
            {
                g.GetComponent<Renderer>().material.color = color;
            }

            
            lastOptionMany.lastObject = go;
        }


    }
}
