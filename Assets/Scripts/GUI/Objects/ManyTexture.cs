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

        private int singleSize = 2;
        private lastOptionsSingle[] lastOption;

        public ManyTexture()
        {
            lastOptionMany = new lastOptionsMany();
            lastOptionMany.lastObject = null;
            lastOptionMany.lastColor = Color.white;

            lastOption = new lastOptionsSingle[singleSize];

            for(int x=0; x<lastOption.Length; x++)
            {
                lastOption[x] = new lastOptionsSingle();
                lastOption[x].lastObject = null;
                lastOption[x].lastColor = Color.white;
            }
            
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


      /*  private void ClearAllOlds()
        {
            if (lastOptionPoint.lastObject != null) ResetLastColor(lastOptionPoint);
            if (lastOptionPivot.lastObject != null) ResetLastColor(lastOptionPivot);
            if (lastOptionMany.lastObject != null) ResetLastColor(lastOptionMany);
        }*/

        /// <summary>
        /// can paint one object, two objects, structure or object and structure or two objects and structure
        /// can be expended to more objects, with small reworks to many structures too
        /// </summary>
        /// <param name="go">  go you want to paint </param>
        /// <param name="color"> select color for your object - > 1 Point - > 2 Pivot - > 3 structure list </param>
        /// 


        public void ChangeColor(GameObject go, Color color, int element = 0)
        {
            /// if nothing changes do nothing
            /// if color changes remove old colors from old objects and apply new colors to new objects
            if (go == lastOption[element].lastObject && color == lastOption[element].lastColor) return ;
            if (lastOption[element].lastObject != null && go != lastOption[element].lastObject) ResetLastColor(lastOption[element]);

            go.GetComponent<Renderer>().material.color = color;


            lastOption[element].lastObject = go;
        }

        public void ChangeColor(List<GameObject> go, Color color)
        {

            if (go == lastOptionMany.lastObject && color == lastOptionMany.lastColor) return ;
            if (lastOptionMany.lastObject != null) ResetLastColor(lastOptionMany);

            foreach (GameObject g in go)
            {
                g.GetComponent<Renderer>().material.color = color;
            }

            lastOptionMany.lastObject = go;

        }

        public void ChangeColor(GameObject point, GameObject pivot, Color colorPoint, Color colorPivot)
        {
            ChangeColor(point, colorPoint, 0);
            ChangeColor(pivot, colorPivot, 1);
        }
        public void ChangeColor(GameObject point, List<GameObject> structure, Color colorPoint, Color colorStructure)
        {
            ChangeColor(point, colorPoint, 0);
            ChangeColor(structure, colorStructure);
        }

        public void ChangeColor(GameObject point, GameObject pivot, List<GameObject> structure, Color colorPoint, Color colorPivot, Color colorStructure)
        {
            ChangeColor(point, colorPoint, 0);
            ChangeColor(pivot, colorPivot, 1);
            ChangeColor(structure, colorStructure);

        }





       


    }
}
