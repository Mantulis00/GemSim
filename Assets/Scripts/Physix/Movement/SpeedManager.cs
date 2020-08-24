using Assets.Scripts.Spawn.Structures.Setup;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Physix.Movement
{
    public class SpeedManager // temp name lol
    {

        public SpeedManager()
        {

        }

        public void Update(Structure structure)
        {
            foreach(Structure.root r in structure.structure.ToArray())
            {
                if (r.dataPoint.type != StructurePointType.Fixed)
                {
                    if (Math.Abs(r.physixData.force.size) > 0.01f) // Constants here 
                    {

                        r.physixData.speed += r.physixData.force.direction * (( r.physixData.force.size) / r.physixData.mass) * (Time.deltaTime)/2;
                        r.physixData.speed *= (float)Math.Pow(0.99, Time.deltaTime); // temp energy coneservation error fix

                    }
                }
                else if (r.dataPoint.type == StructurePointType.Fixed)
                {
                    r.physixData.speed = Vector3.zero;
                }

            }
            SpawnerManager.MoveConnectors(structure);
        }



    }
}
