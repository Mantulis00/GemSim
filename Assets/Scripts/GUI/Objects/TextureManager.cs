using UnityEngine;

namespace Assets.Scripts.GUI.Objects.TextureManager
{
    public  class TextureManager
    {
        private GameObject lastObject ;

        public TextureManager()
        {
            lastObject = null;
        }

        public  void ResetLastColor()
        {
            lastObject.GetComponent<Renderer>().material.color = Color.white;
        }

        public  void ChangeColor(GameObject go, Color color)
        {
            if (go == lastObject) return;
            if (lastObject != null) ResetLastColor();

            go.GetComponent<Renderer>().material.color = color;
            Debug.Log(go + " color green");
            lastObject = go;
        }


    }
}
