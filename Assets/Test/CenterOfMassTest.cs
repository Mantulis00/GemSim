using Assets.Scripts.Spawn.Structures.Setup;
using UnityEngine;

namespace Assets.Test
{
    class CenterOfMassTest : MonoBehaviour
    {
        public GameObject indicator;
        private Structure structure;


        public void Test(Structure structure)
        {

            this.structure = structure;

        }

        void Update()
        {
            if (structure == null || structure.structure.Count == 0) return;

            Vector3 centerOfMass = new Vector3();
            float mass = 0;

            foreach (Structure.root r in structure.structure)
            {
                centerOfMass += r.point.transform.position * r.physixData.mass;
                mass += r.physixData.mass;
            }
            centerOfMass /= mass;
            indicator.transform.position = centerOfMass;
        }



    }
}
