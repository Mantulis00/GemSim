
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Spawn
{
    public interface IStructure
    {
         void AddElement(GameObject go, GameObject extensionRoot, SpawnOptions option);
         void AddConnection(GameObject go, GameObject from, GameObject to);



        }
}
