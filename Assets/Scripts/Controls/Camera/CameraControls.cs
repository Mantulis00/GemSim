using UnityEngine;

namespace Assets.Scripts.Controls
{
    public class CameraControls
    {
        private bool noHoldLastFrame = true;
        public bool awaitingRegister {  get;  private set; }

        internal Vector2 mouseMovementValue;
        private Vector2 mouseStartPosition, mousePosition;

        private float sensitivity = 0.2f;



        internal void Start()
        {
            awaitingRegister = false;
            mousePosition = new Vector2(0, 0);
        }

        internal bool DetectRotation()
        {
            if (Input.GetMouseButton(2))
            {
                if (noHoldLastFrame) // if its first frame to hold // to avoid jerk
                {
                    mouseStartPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    noHoldLastFrame = false;
                }

                // look at value now
                mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                // compare to value last frame
                mouseMovementValue = new Vector2(mouseStartPosition.x - mousePosition.x, mouseStartPosition.y - mousePosition.y) * sensitivity;


                return !(mouseMovementValue.x == 0 && mouseMovementValue.y == 0); // if nothing changed return false
            }

            if (!noHoldLastFrame) // if stoped holding take action
            {
                awaitingRegister = true;
            }
            noHoldLastFrame = true;


            return false; // only if changes detected take action
        }

        internal void SetMovementRegister()
        {
            awaitingRegister = !awaitingRegister;
        }


    }


}
