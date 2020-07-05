using Assets.Map2Math;
using Assets.Scripts.Spawn;
using System;
using System.Collections.Generic;
using UnityEngine;


public class SpawnerManager : MonoBehaviour
{


    float spawnDistance = 10;
    LinesManager linesManager;



    void Start()
    {
        linesManager = new LinesManager();
    }

    internal GameObject MakeGO (GameObject obModel, byte n)
    {
        return linesManager.MakeGO(this.transform, obModel, n);
    }

    internal GameObject FindConnector(GameObject go)
    {
        return linesManager.FindConnector(go);
    }

    internal Transform FindSecondPointLocation(GameObject go)
    {
       return linesManager.FindSecondPointLocation(go);
    }

    internal void DeleteGo(GameObject selectedObject)
    { 
    }


        public Vector3 MakeEndPoints(GameObject go, Transform cameraPos, Vector2 mouseLocation)
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

    public void ConnectEndPoints(GameObject go, Vector3 start, Vector3 finish)
    {
        // set location
        Vector3 lineDistance = new Vector3();

        lineDistance =  finish - start;
        go.transform.position = lineDistance / 2 + start;

        // set rotation
        Vector3 lineRotation = new Vector3();

        lineRotation.y = ((float)Math.Atan(lineDistance.x / lineDistance.z) + 1.5708f);
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
