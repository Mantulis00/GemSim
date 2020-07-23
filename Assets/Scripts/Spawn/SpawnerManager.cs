using Assets.Map2Math;
using Assets.Scripts.Spawn;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEditorInternal;
using UnityEngine;


public class SpawnerManager : MonoBehaviour
{
    Dictionary<GameObject, StructureType> objectTypes;

    private float spawnDistance = 10;
    private LinesManager linesManager;

    private int enableSpawning;

    public SpawnerManager()
    {
        enableSpawning = 0;
        linesManager = new LinesManager();
        objectTypes = new Dictionary<GameObject, StructureType>();
    }

    internal GameObject MakeGO (GameObject obModel)
    {

        if (objectTypes.ContainsKey(obModel)) // need fixes
        {
            if (enableSpawning > 0)
            {
                enableSpawning++ ;
                if (enableSpawning == 3) enableSpawning = 0;

                return linesManager.MakeGO(this.transform, obModel, objectTypes); // new line
            }

            else
            {
                enableSpawning = 1 ; 
                return obModel;
            }
        }



        return linesManager.MakeGO(this.transform, obModel, objectTypes); // line from obj
    }
    internal void DeleteGo(GameObject go)
    {
        linesManager.DeleteGo(go);
    }

    internal GameObject FindConnector(GameObject go)
    {
        return linesManager.FindConnector(go);
    }

    internal Transform FindSecondPointLocation(GameObject go)
    {
       return linesManager.FindSecondPointLocation(go);
    }




        public Vector3 MovePoint(GameObject go, Transform cameraPos, Vector2 mouseLocation)
    {

        // position
        Vector3 goPosition = TangentProjection.SetupSpawnDistance(cameraPos, mouseLocation, spawnDistance); // location + mqpw
        go.transform.position = goPosition;

        // rotation
        Vector3 goRotation = cameraPos.transform.rotation.eulerAngles;
        goRotation.x = 0;
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
        scales.y = go.transform.localScale.y;
        scales.z = go.transform.localScale.z;
        scales.x = Linear.Pythagoras3(lineDistance);
        go.transform.localScale = scales;

    }

   

 


}
