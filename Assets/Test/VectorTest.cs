
using UnityEngine;

namespace Assets.Test
{
    class VectorTest : MonoBehaviour
    {
        public Vector3 direction;
        public bool run;
        public GameObject go;

        private Vector3 startLocation;
        public float slowmode, maxDistance;

        private void Start()
        {
            this.direction = direction;
            run = false;
            startLocation = Camera.main.transform.position;
        }


        public VectorTest(Vector3 direction)
        {

        }

        private void Update()
        {
            if (run)
            {
                if (!this.enabled) this.enabled = true;

                Vector3 goThere = go.transform.position;
                goThere += direction * Time.deltaTime / slowmode;
                go.transform.position = goThere;

                if ((goThere - startLocation).magnitude >= maxDistance)
                {
                    go.transform.position = startLocation;
                }

            }
            else
            {
                if (this.enabled) this.enabled = false;
            }
        }







    }
}
