

using Assets.Scripts.Spawn.Structures.Setup;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Assets.Scripts.Physix.Movement
{
    public class PositionManager
    {
        public PositionManager()
        {

        }

        public void Update(Structure structure)
        {
            foreach ( Structure.root r in structure.structure.ToArray())
            {
                r.point.transform.position = r.point.transform.position + r.physixData.speed * Time.deltaTime;
            }
        }


    }
}
