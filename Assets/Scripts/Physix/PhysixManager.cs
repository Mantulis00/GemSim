using Assets.Scripts.Physix.Movement;
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
       public Structure structure;

        private ForcesConnections tensionForces; //tbc to forces

        private SpeedManager speedManager;
        private PositionManager positionManager;

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
            SetupModels();
            //temp
            UpdatePhysix += tensionForces.Update;
            UpdatePhysix += speedManager.Update;
            UpdatePhysix += positionManager.Update;
        }

        private void SetupTensionForces() // TBA
        {
            tensionForces = new ForcesConnections();
        }

        private void SetupModels()
        {
            speedManager = new SpeedManager();
            positionManager = new PositionManager();
        }


        private  void Update()
        {
            /*foreach(Structure s in structures)
            {
                UpdatePhysix(frameTime, s);
            }*/
            if (structure != null)
            {
                 tensionForces.Update(structure);
                 speedManager.Update(structure);
                positionManager.Update(structure);
            }
          //  UpdatePhysix( structure);
           
        }

      

    }
}
