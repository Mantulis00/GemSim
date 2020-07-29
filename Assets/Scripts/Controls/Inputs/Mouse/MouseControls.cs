using UnityEngine;

namespace Assets.Scripts.Controls
{
    static class MouseControls 
    {
        public static  MouseActions DetectClick(bool hold)
        {
            if (hold)
            {
                if (Input.GetMouseButton(0)) return MouseActions.Select;
            }

            if (Input.GetMouseButtonDown(0)) return MouseActions.Select;
            else if (Input.GetMouseButtonDown(1)) return MouseActions.Unselect;


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
