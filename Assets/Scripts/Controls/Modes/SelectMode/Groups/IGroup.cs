using Assets.Scripts.Spawn.Structures.Setup;
using System.Collections.Generic;
using UnityEngine;

interface IGroup
{
     Structure structure { get; }

   
     List<GameObject> GetList();
     void Add(GameObject go, Structure structure);// ?TBC not rly a place or is it ?

     void Remove(GameObject go, Structure structure);

    void ChangeStructure(Structure structure);
    GameObject GetCheckObject();




    }
