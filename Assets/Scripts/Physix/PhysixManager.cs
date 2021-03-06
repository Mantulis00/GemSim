﻿using Assets.Scripts.Physix.Movement;
using Assets.Scripts.Physix.TensionForces;
using Assets.Scripts.Spawn.Structures.Setup;
using Assets.Test;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Physix
{
    class PhysixManager : MonoBehaviour
    {
        public delegate void DelegateUpdatePhysix(Structure structure); // mostly for later updates TBA
        public event DelegateUpdatePhysix UpdatePhysix;

        // tests
        public CenterOfMassTest test;

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
            UpdatePhysix += positionManager.Update;
            UpdatePhysix += speedManager.Update;
            UpdatePhysix += tensionForces.Update;
            
           
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
                test.Test(structure);
                UpdatePhysix(structure);
            }
          //  UpdatePhysix( structure);
           
        }

      

    }
}
