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
                        r.physixData.force.size -= r.physixData.speed.magnitude * 2000f * (float) Time.deltaTime;//(float)Math.Pow(0.001, Time.deltaTime);
                        if (r.physixData.force.size < 0) r.physixData.force.size = 0;


                        r.physixData.speed += ((r.physixData.force.direction * r.physixData.force.size) / r.physixData.mass) * Time.deltaTime;
                        // temp friction
                        Debug.Log(r.physixData.force.direction);
                      
                    }
                }
                else if (r.dataPoint.type == StructurePointType.Fixed)
                {
                    r.physixData.speed = Vector3.zero;
                }

               // Debug.Log(r.point.name + " " + r.physixData.force.size);
            }
            SpawnerManager.MoveConnectors(structure);
        }



    }
}
