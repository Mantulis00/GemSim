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


    public void Start()
    {
        linesManager = new LinesManager();
        structureManager = new StructureManager();
      //  objectTypes = new Dictionary<GameObject, StructureType>();

        structures = new Dictionary<GameObject, Structure>();
    }
    // only connections
    // merge 2 structures
    // merge point to structure

    SpawnStates state; // to know if only connect two points







    internal GameObject MakeGO (GameObject obModel, GameObject extension, SpawnOptions option) // make go from object ( if  extension not null , to what object)
    {
        if (option == SpawnOptions.Start)
        {
            if (extension != null)
            {
                state = SpawnStates.Connect;
                this.extension = extension; // to check if start != finish
                return null;
            }

            else // create new structure
            {
                state = SpawnStates.Ordinary;
                GameObject go = structureManager.MakeGO(this.transform, obModel, SpawnOptions.Start);
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
                if (structures.ContainsKey(extension) && structures.ContainsKey(this.extension))
                {
                    if (structures[extension] == structures[this.extension])
                        state = SpawnStates.Connect;
                    else
                    {
                        Debug.Log(structures[this.extension].structure.Count);

                        structureManager.MergeStructures(structures[extension], structures[this.extension]);

                        Debug.Log("bf " + structures[this.extension].structure.Count);

                        structures[extension] = structures[this.extension];
                        state = SpawnStates.Merge;
                    }
                       
                }
                else
                    state = SpawnStates.Connect;


                if (extension == this.extension)
                    state = SpawnStates.Error;
                //   this.extension = null; // to cancel spawning connection
                return null;// connect structures
            }
            else
            {
                state = SpawnStates.Ordinary;

                GameObject go = structureManager.MakeGO(this.transform, obModel, SpawnOptions.Finish);

                if (structures.ContainsKey(this.extension))// extra safety
                {
                    var str = structures[this.extension];
                    str.AddElement(go, this.extension, SpawnOptions.Finish);
                    structures.Add(go, str);
                }


               

                    state = SpawnStates.Ordinary;

                return go;
            }// find structure, add endpoint to root element
        }
        else
        {
            if (state != SpawnStates.Error)
            {
                GameObject go = structureManager.MakeGO(this.transform, obModel, SpawnOptions.Connection);

                if (state == SpawnStates.Connect || state == SpawnStates.Merge)
                {
                    var str = structures[this.extension];
                    str.AddConnection(go, this.extension, extension);
                }

                else if (structures.ContainsKey(this.extension)) // extra safety
                {
                    var str = structures[this.extension];
                    str.AddElement(go, this.extension, SpawnOptions.Connection);
                    structures.Add(go, str);
                }


                return go;
            }
            return null;
        }

   
    }


    internal List<Structure.connection> GetConnections(GameObject go)
    {
        if (structures.ContainsKey(go))
            return structureManager.GetConnections(go, structures[go]);
        else
            return null;
    }



    internal void DeleteGo(GameObject go)
    {
        //linesManager.DeleteGo(go);
    }


    internal bool CheckConnector(GameObject go)
    {
        if (structures.ContainsKey(go))
            return structureManager.CheckConnector(go, structures[go]);
        else
            return false;
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
