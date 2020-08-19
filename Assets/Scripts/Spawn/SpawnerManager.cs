using Assets.Map2Math;
using Assets.Scripts.Spawn;
using Assets.Scripts.Spawn.TangentProjection;
using Assets.Scripts.Spawn.Structures.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.ComponentModel.Design.Serialization;

public class SpawnerManager : MonoBehaviour
{
    //Dictionary<GameObject, StructureType> objectTypes;
    Dictionary<GameObject, Structure> structures;

    GameObject extension; // object that was selected last call (extension in methods -> object selected this frame)

    public float spawnDistance = 10;
    private StructureManager structureManager;


    public void Start()
    {
        structureManager = new StructureManager();
        structures = new Dictionary<GameObject, Structure>();
    }
    // only connections
    // merge 2 structures
    // merge point to structure

    SpawnStates lastState; // to know if only connect two points


    internal GameObject MakeGO (GameObject obModel, GameObject extension, SpawnOptions option) // make go from object ( if  extension not null , to what object)
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
                    if (structures[extension] == structures[this.extension]) // if last and this objects are at same structure just connect them
                        lastState = SpawnStates.Connect;
                    else // if they are in different structures, merge structures
                    { 
                        structureManager.MergeStructures(structures[this.extension], structures[extension]);
                        structures[this.extension] = structures[extension];
                        lastState = SpawnStates.Merge;
                    }
                       
                }
                else
                    lastState = SpawnStates.Connect;


                if (extension == this.extension)
                    lastState = SpawnStates.Error;

                return null;// connect structures
            }
            else
            {
                GameObject go = structureManager.MakeGO(this.transform, obModel, SpawnOptions.Finish);

                if (structures.ContainsKey(this.extension))// extra safety
                {
                    var str = structures[this.extension];
                    str.AddElement(go, this.extension, SpawnOptions.Finish);
                    structures.Add(go, str);
                }

                    lastState = SpawnStates.Ordinary;

                return go;
            }// find structure, add endpoint to root element
        }
        else // option  => connection
        {
            if (lastState != SpawnStates.Error)
            {
                GameObject go = structureManager.MakeGO(this.transform, obModel, SpawnOptions.Connection); // create connection

                if (lastState == SpawnStates.Connect || lastState == SpawnStates.Merge)
                {
                    var str = structures[this.extension]; // add connection between this.extension and extension -> last selected and this selected objects
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

    internal Structure GetStructure (GameObject go)
    {
        return structures[go];
    }



    internal void DeleteGo(GameObject go) // TBA?
    {
        //linesManager.DeleteGo(go);
    }


    internal bool CheckConnector(GameObject go)
    {
        if (structures.ContainsKey(go))
            return structures[go].CheckConnector(go);
        else
            return false;
    }





        public  Vector3 MovePoint(GameObject go, Transform cameraPos, Vector2 mouseLocation)
    {

        // position
        Vector3 goPosition = TangentProjection.SetupSpawnDistance(cameraPos, mouseLocation, spawnDistance); // location + mqpw

        if (go != null)
        {
            go.transform.position = goPosition;

            // rotation
            Vector3 goRotation = cameraPos.transform.rotation.eulerAngles;
            goRotation.x = 0;


             go.transform.rotation = Quaternion.Euler(goRotation);
        }

        return goPosition;
    }

    public static void MoveConnection(GameObject go, Vector3 start, Vector3 finish) 
    {
        if (go == null) return;
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
        Vector3 scales = new Vector3(); // ?TBC constants -- > xd
        scales.y = 0.1f;//go.transform.localScale.y/10;
        scales.z = 0.1f;//go.transform.localScale.z/10;
        scales.x = (float)Linear.Pythagoras3(lineDistance);//lineDistance.magnitude;
        go.transform.localScale = scales;

    }

    public static void MoveConnectors(List<GameObject> points, Structure structure)
    {
        if (points.Count == 0) return;

        //structure.RemoveDuplicates(points);
        List<Structure.root> roots = structure.GetRootsFromPoints(points); // setup could be done once



        foreach(Structure.root r in roots.ToList())
        {
            foreach (Structure.connection c in r.connections.ToList())
            {
                MoveConnection(c.connector, r.point.transform.position, c.endPoint.transform.position); // ?TBC add check if can strech functionality 

                /// gtfo this somewhere else
                // magnitude not rly that accurate lol
                c.dataConnection.realLenght = (double)Math.Round((c.endPoint.transform.position - r.point.transform.position).magnitude, 4);//Linear.Pythagoras3(c.endPoint.transform.position - r.point.transform.position); // refreash real lenght

                if (c.dataConnection.realLenght > c.dataConnection.originalLenght) c.connector.GetComponent<Renderer>().material.color = Color.yellow;
                else if (c.dataConnection.realLenght < c.dataConnection.originalLenght) c.connector.GetComponent<Renderer>().material.color = Color.cyan;
                else c.connector.GetComponent<Renderer>().material.color = Color.white;

                ///

            }
        }

    }

    public static void MoveConnectors( Structure structure)
    {
        foreach (Structure.root r in structure.structure.ToList())
        {
            foreach (Structure.connection c in r.connections.ToList())
            {
                MoveConnection(c.connector, r.point.transform.position, c.endPoint.transform.position); // ?TBC add check if can strech functionality 

                /// gtfo this somewhere else
                // magnitude not rly that accurate lol
                c.dataConnection.realLenght = (double)Math.Round((c.endPoint.transform.position - r.point.transform.position).magnitude, 4);//Linear.Pythagoras3(c.endPoint.transform.position - r.point.transform.position); // refreash real lenght

                if (c.dataConnection.realLenght > c.dataConnection.originalLenght) c.connector.GetComponent<Renderer>().material.color = Color.yellow;
                else if (c.dataConnection.realLenght < c.dataConnection.originalLenght) c.connector.GetComponent<Renderer>().material.color = Color.cyan;
                else c.connector.GetComponent<Renderer>().material.color = Color.white;

                ///

            }
        }

    }






}
