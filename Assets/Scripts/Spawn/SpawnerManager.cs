using Assets.Map2Math;
using Assets.Scripts.Spawn;
using Assets.Scripts.Spawn.Structures.Setup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;


public class SpawnerManager : MonoBehaviour
{
    //Dictionary<GameObject, StructureType> objectTypes;
    Dictionary<GameObject, Structure> structures;

    GameObject extension;

    public float spawnDistance = 10;
    private LinesManager linesManager;
    private StructureManager structureManager;


    public SpawnerManager()
    {
        linesManager = new LinesManager();
        structureManager = new StructureManager();
      //  objectTypes = new Dictionary<GameObject, StructureType>();

        structures = new Dictionary<GameObject, Structure>();
    }


    internal GameObject MakeGO (GameObject obModel, GameObject extension, SpawnOptions option) // make go from object ( if null not extension, what object)
    {
        if (option == SpawnOptions.Start)
        {
            if (extension != null)
            {
                this.extension = extension; // to check if start != finish
                return null;
            }
                
            else // create new structure
            {
                Debug.Log("created");
                GameObject go = structureManager.MakeGO(this.transform, obModel, extension, SpawnOptions.Start);
                Structure str = new Structure(go);
                structures.Add(go, str);

                this.extension = go;
                return go;
            }
        }

        else if (option == SpawnOptions.Finish)
        {
            if (extension != null)
            {

                this.extension = null; // to cancel spawning connection
                return extension;// connect structures
            }
            else
            {
                Debug.Log(this.extension);
                //Structure str = structures[this.extension];
                GameObject go = structureManager.MakeGO(this.transform, obModel, extension, SpawnOptions.Start);

               // str.AddElement(go, this.extension);

                return go;
            }// find structure, add endpoint to root element
        }
        else 
        {
          //  Structure str = structures[this.extension];
            GameObject go = structureManager.MakeGO(this.transform, obModel, extension, SpawnOptions.Start);

           // str.AddElement(go, this.extension);

            return go;
        }

   
    }






    internal void DeleteGo(GameObject go)
    {
        linesManager.DeleteGo(go);
    }

    internal GameObject FindConnector(GameObject go)
    {


        return linesManager.FindConnector(go);
    }

    internal bool CheckConnector(GameObject go)
    {
        return linesManager.CheckConnector(go);
    }

        internal Transform FindSecondPointLocation(GameObject go)
    {
       return linesManager.FindSecondPointLocation(go);
    }




        public Vector3 MovePoint(GameObject go, Transform cameraPos, Vector2 mouseLocation)
    {

        // position
        Vector3 goPosition = TangentProjection.SetupSpawnDistance(cameraPos, mouseLocation, spawnDistance); // location + mqpw

        if (go!=null)
             go.transform.position = goPosition;

        // rotation
        Vector3 goRotation = cameraPos.transform.rotation.eulerAngles;
        goRotation.x = 0;

        if (go != null)
            go.transform.rotation = Quaternion.Euler(goRotation);

        return goPosition;
    }

    public void MoveConnection(GameObject go, Vector3 start, Vector3 finish)
    {
        // set location
        Vector3 lineDistance = new Vector3();

        lineDistance =  finish - start;
        go.transform.position = lineDistance / 2 + start;

        // set rotation
        Vector3 lineRotation = new Vector3();

        lineRotation.y = ((float)Math.Atan(lineDistance.x / lineDistance.z) + (float)Math.PI/2);
        lineRotation.z = (float)Math.Atan(lineDistance.y / Linear.Pythagoras(lineDistance.x, lineDistance.z));

        if (lineDistance.z >=0) lineRotation.z *= -1;
        go.transform.rotation = Quaternion.Euler(lineRotation * 180 / (float)Math.PI);


        // set scale
        Vector3 scales = new Vector3();
        scales.y = 0.1f;//go.transform.localScale.y/10;
        scales.z = 0.1f;//go.transform.localScale.z/10;
        scales.x = Linear.Pythagoras3(lineDistance);
        go.transform.localScale = scales;

    }

   

 


}
