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

         

    }
}
