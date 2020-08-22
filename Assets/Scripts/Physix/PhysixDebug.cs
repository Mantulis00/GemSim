

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
            foreach(Structure.connection c in root.connections)
            {
                energy = GetTensionPotentialEnergy(c);//+= GetTensionPotentialEnergy(c);
            }
            Debug.Log("Energy " + energy);


            return energy;
        }

        private static double GetKineticEnergy(PhysixData data)
        {
            Debug.Log("Speed " + data.speed.magnitude * data.speed.magnitude * data.mass / 2);
            return (data.speed.magnitude * data.speed.magnitude * data.mass / 2);
        }

        private static double GetTensionPotentialEnergy(Structure.connection connection)
        {
            double x = connection.dataConnection.originalLenght - connection.dataConnection.realLenght;

            
            return connection.dataConnection.tensionCoefficient * x * x / 2;
        }


    }
}
