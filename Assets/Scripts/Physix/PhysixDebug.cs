

using Assets.Scripts.Physix.Models;
using Assets.Scripts.Spawn.Structures.Setup;

namespace Assets.Scripts.Physix
{
   public static class PhysixDebug
    {
        public static double CalculateEnergy(PhysixData data, Structure.connection connection)
        {
            double energy = 0;
            energy += GetKineticEnergy(data);
            energy += GetTensionPotentialEnergy(connection);


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
