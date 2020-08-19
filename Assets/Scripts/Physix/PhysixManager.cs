using Assets.Scripts.Physix.TensionForces;
using Assets.Scripts.Spawn.Structures.Setup;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Physix
{
    class PhysixManager : MonoBehaviour
    {
        public delegate void DelegateUpdatePhysix(Structure structure); // mostly for later updates TBA
        public event DelegateUpdatePhysix UpdatePhysix;



        List<Structure> structures; // could rly evolve into go list
        Structure structure;

        private ForcesConnections tensionForces;

        private double frameTime;

        public void AddStructure(Structure structure)
        {
            structures.Add(structure);
        }

        public void SetStructure(Structure structure)
        {
            this.structure = structure;
        }

        private void Start()
        {
            structures = new List<Structure>();
            SetupTensionForces();
            //temp
            UpdatePhysix += tensionForces.Update;
        }

        private void SetupTensionForces() // TBA
        {
            tensionForces = new ForcesConnections();
        }



        private  void Update()
        {
            /*foreach(Structure s in structures)
            {
                UpdatePhysix(frameTime, s);
            }*/
            if (structure != null)
            UpdatePhysix( structure);
           
        }

      

    }
}
