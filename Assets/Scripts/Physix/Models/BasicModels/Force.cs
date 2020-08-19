using UnityEngine;

namespace Assets.Scripts.Physix.Data
{
    public class Force
    {
        public Vector3 direction;
        public float size; // useless ?

        public Force()
        {
            direction = new Vector3();
            size = 0;
        }

    }
}
