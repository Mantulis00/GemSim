using Assets.Map2Math;
using Assets.Scripts.Spawn;
using System;
using System.Collections.Generic;
using UnityEngine;


public class SpawnerManager : MonoBehaviour
{
    List<GameObject> objectList;

    float spawnDistance = 10;

    void Start()
    {
        objectList = new List<GameObject>();
    }




    public Vector3 MakeEndPoints(Transform cameraPos, GameObject obModel, Vector2 mouseLocation)
    {
        GameObject go;
        go = Instantiate(obModel) as GameObject;
        go.transform.SetParent(this.transform);


        Vector3 goPosition = TangentProjection.SetupSpawnDistance(cameraPos, mouseLocation, spawnDistance); // location + mqpw
        go.transform.position = goPosition;

        Vector3 goRotation = cameraPos.transform.rotation.eulerAngles;
        goRotation.x = 0;
        go.transform.rotation = Quaternion.Euler(goRotation);

        return goPosition;
    }

    public void ConnectEndPoints(GameObject obModel, Vector3 start, Vector3 finish)
    {
        GameObject go;
        go = Instantiate(obModel) as GameObject;
        go.transform.SetParent(this.transform);

        Vector3 lineDistance = new Vector3();

        lineDistance =  finish - start;

       

        Vector3 lineRotation = new Vector3();

        lineRotation.y = ((float)Math.Atan(lineDistance.x / lineDistance.z) + 1.5708f);
        //lineRotation.x = (float)Math.Atan(lineDistance.y / lineDistance.z) ;
        lineRotation.z = (float)Math.Atan(lineDistance.y / Linear.Pythagoras(lineDistance.x, lineDistance.z));

        Debug.Log(lineDistance);

        go.transform.rotation = Quaternion.Euler(lineRotation * 180 / (float)Math.PI);

        go.transform.position = lineDistance/2 + start;

        Vector3 scales = new Vector3();
        scales.y = go.transform.localScale.y;
        scales.z = go.transform.localScale.z;

        scales.x = Linear.Pythagoras3(lineDistance);

        go.transform.localScale = scales;

    }

   

 


}
