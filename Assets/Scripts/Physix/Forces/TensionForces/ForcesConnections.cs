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

            foreach(Structure.root r in structure.structure.ToList())
            {
                foreach(Structure.connection c in r.connections)
                {
                    if (c.dataConnection.realLenght != c.dataConnection.originalLenght)
                    {
                        RegisterForce( r.point, c);
                    }
                }
            }
        }

        private void RegisterForce(GameObject rootPoint, Structure.connection connection)
        {
            Force force = connection.force;
            force.direction = (rootPoint.transform.position - connection.endPoint.transform.position) / (rootPoint.transform.position - connection.endPoint.transform.position).magnitude;// direction part * connection.dataConnection.stretchCoefficient;

           
            //Debug.Log((float)force.direction.magnitude);
        }


    }
}
