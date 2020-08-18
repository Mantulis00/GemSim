using Assets.Scripts.Spawn.Structures.Setup;
using System.Collections.Generic;
using UnityEngine;

interface IGroup
{
    public List<GameObject> GetList();
    public void Add(GameObject go, Structure structure);// ?TBC not rly a place or is it ?

    public void Remove(GameObject go, Structure structure);




    }
