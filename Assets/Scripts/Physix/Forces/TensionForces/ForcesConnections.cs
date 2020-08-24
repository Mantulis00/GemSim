using Assets.Scripts.Physix.Data;
using Assets.Scripts.Spawn.Structures.Setup;
using JetBrains.Annotations;
using System;
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
                r.physixData.force.direction = Vector3.zero;
                r.physixData.force.size = 0;

                foreach (Structure.connection c in r.connections.ToArray())
                {
                    if (c.dataConnection.realLenght != c.dataConnection.originalLenght)
                    {
                        RegisterForce(r, c, structure);
                    }
                }
               // if (r.physixData.force.size > r.physixData.maxForce) r.physixData.maxForce = r.physixData.force.size;
               // Debug.Log(r.physixData.maxForce);
            }
        }

        private void RegisterCompression(Structure.connection connection)
        {
            if (connection.dataConnection.originalLenght < connection.dataConnection.realLenght) connection.dataConnection.compressed = true;
            else connection.dataConnection.compressed = false;
        }

        private void RegisterForce(Structure.root rootPoint, Structure.connection connection, Structure structure)
        {
            Force force = new Force();// connection.physixData.force;
            force.size = (float)(connection.dataConnection.originalLenght - connection.dataConnection.realLenght) * connection.dataConnection.tensionCoefficient;
            // force.size = (float)(connection.dataConnection.originalLenght - connection.dataConnection.realLenght ) * connection.dataConnection.tensionCoefficient;
            force.direction = (rootPoint.point.transform.position - connection.endPoint.transform.position) / 
                (rootPoint.point.transform.position - connection.endPoint.transform.position).magnitude;// direction part * connection.dataConnection.stretchCoefficient;
            

            Vector3 newForce = force.direction * force.size;

            

          //  if (Math.Abs(connection.dataConnection.originalLenght - connection.dataConnection.realLenght) > 0.01f) // CLH
          //  {

                rootPoint.physixData.force.direction = ((rootPoint.physixData.force.direction * rootPoint.physixData.force.size) + newForce); ///
                   rootPoint.physixData.force.size = rootPoint.physixData.force.direction.magnitude;
                rootPoint.physixData.force.direction /= rootPoint.physixData.force.size;





          //  }

            ///rootPoint.physixData.force.size -= rootPoint.physixData.speed.magnitude * 200f * Time.deltaTime;//(float)Math.Pow(0.001, Time.deltaTime);
            if (rootPoint.physixData.force.size < 0) rootPoint.physixData.force.size = 0;

            // if (rootPoint.physixData.force.size != 0) Debug.Log(rootPoint.physixData.force.size);
            if (rootPoint.physixData.speed.magnitude > 0.01f) PhysixDebug.CalculateEnergy(rootPoint);
            //rootPoint.physixData.force = force;

            //  force = new Force();
            // force.direction = -(rootPoint.point.transform.position - connection.endPoint.transform.position) / (rootPoint.point.transform.position - connection.endPoint.transform.position).magnitude;// direction part * connection.dataConnection.stretchCoefficient;
            // force.size = (float)(connection.dataConnection.originalLenght - connection.dataConnection.realLenght) * connection.dataConnection.tensionCoefficient;

            //  structure.GetRootsFromPoints(connection.endPoint).physixData.force = force;

            


        }


    }
}
