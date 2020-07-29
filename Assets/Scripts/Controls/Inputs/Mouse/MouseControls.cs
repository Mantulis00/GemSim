using UnityEngine;

namespace Assets.Scripts.Controls
{
    static class MouseControls 
    {
        public static  MouseAction DetectClick(bool hold)
        {
            if (hold)
            {
                if (Input.GetMouseButton(0)) return MouseAction.Select;
            }

            if (Input.GetMouseButtonDown(0)) return MouseAction.Select;
            else if (Input.GetMouseButtonDown(1)) return MouseAction.Unselect;


            return 0;
        }

        public static Vector2 GetMouseLocation()
        {
            Vector2 coords = new Vector2();
            coords.x = Input.mousePosition.x;
            coords.y = Input.mousePosition.y;


            return coords;
        }

         

    }
}
