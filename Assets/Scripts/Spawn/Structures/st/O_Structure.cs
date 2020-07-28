using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Spawn
{
    public class O_Structure
    {
        struct str_structure
        {
           internal GameObject ob;
           internal ObjectType type;
           internal Vector3 rotation;
        }
        List<GameObject> obList;

        internal O_Structure()
        {
            obList = new List<GameObject>();
        }

        internal void AddObject(GameObject ob, Vector3 rotation)
        {
            str_structure structure;
            structure.ob = ob;
            structure.type = ObjectType.Line;
            structure.rotation = rotation;

            obList.Add(ob);
        }
        internal void AddObject(GameObject ob)
        {

            str_structure structure;
            structure.ob = ob;
            structure.type = ObjectType.Point;
            structure.rotation = new Vector3();

            obList.Add(ob);
            
        }


    }
}
