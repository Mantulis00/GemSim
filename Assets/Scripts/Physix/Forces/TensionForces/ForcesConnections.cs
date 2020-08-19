using Assets.Scripts.Physix.Data;
using Assets.Scripts.Spawn.Structures.Setup;
using JetBrains.Annotations;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using Unity;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Assets.Scripts.Physix.TensionForces
{
   public class ForcesConnections
    {
        public ForcesConnections()
        {
           
        }

        public void Update(Structure structure)
        {
            ///could be optimized 2x if duplicate connections are cleared, but for the price of memory ?


            foreach(Structure.root r in structure.structure.ToArray())
            {
                foreach (Structure.connection c in r.connections.ToArray())
                {
                    if (c.dataConnection.realLenght != c.dataConnection.originalLenght)
                    {
                        RegisterForce(r, c, structure);
                    }
                }
            }
        }

        private void RegisterForce(Structure.root rootPoint, Structure.connection connection, Structure structure)
        {
            Force force = new Force();// connection.physixData.force;
            force.direction = (rootPoint.point.transform.position - connection.endPoint.transform.position) / (rootPoint.point.transform.position - connection.endPoint.transform.position).magnitude;// direction part * connection.dataConnection.stretchCoefficient;
            force.size = (float)( connection.dataConnection.originalLenght - connection.dataConnection.realLenght) * connection.dataConnection.tensionCoefficient;

            rootPoint.physixData.force = force;

          //  force = new Force();
           // force.direction = -(rootPoint.point.transform.position - connection.endPoint.transform.position) / (rootPoint.point.transform.position - connection.endPoint.transform.position).magnitude;// direction part * connection.dataConnection.stretchCoefficient;
           // force.size = (float)(connection.dataConnection.originalLenght - connection.dataConnection.realLenght) * connection.dataConnection.tensionCoefficient;

          //  structure.GetRootsFromPoints(connection.endPoint).physixData.force = force;
        }


    }
}
