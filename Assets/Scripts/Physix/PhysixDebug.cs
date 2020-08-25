

using Assets.Scripts.Physix.Models;
using Assets.Scripts.Spawn.Structures.Setup;
using UnityEngine;

namespace Assets.Scripts.Physix
{
   public static class PhysixDebug
    {
        public static double CalculateEnergy(Structure.root root)
        {
            double energy = 0;
            energy += GetKineticEnergy(root.physixData);
            double tensionEnergy = 0;
            foreach(Structure.connection c in root.connections)
            {
                tensionEnergy = GetTensionPotentialEnergy(c);//+= GetTensionPotentialEnergy(c);
            }
            Debug.Log("Speed " + energy + "  Tension: " + tensionEnergy + " Energy: " + (energy + tensionEnergy));


            return energy;
        }

        private static double GetKineticEnergy(PhysixData data)
        {
             
            return (data.speed.magnitude * data.speed.magnitude * data.mass / 2);
        }

        private static double GetTensionPotentialEnergy(Structure.connection connection)
        {
            double x = connection.dataConnection.originalLenght - connection.dataConnection.realLenght;


            return connection.dataConnection.tensionCoefficient * x * x / 2;
        }


    }
}
