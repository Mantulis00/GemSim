using System;
using System.Collections;
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

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MakeEndPoints(Transform cameraPos, GameObject obModel, bool first)
    {
        GameObject go;
        go = Instantiate(obModel) as GameObject;
        go.transform.SetParent(this.transform);


        go.transform.position = SetupSpawnDistance(cameraPos);

        Vector3 goRotation = cameraPos.transform.rotation.eulerAngles;
        goRotation.x = 0;
        go.transform.rotation = Quaternion.Euler(goRotation);



    }

    public void ConnectEndPoints()
    {

    }

    private Vector3 SetupSpawnDistance(Transform camPos)
    {
        Vector3 spawnLocation = new Vector3();
        if (Math.Abs(camPos.rotation.y) > 90)
        {
            spawnLocation.x = camPos.position.x - spawnDistance * (float)Math.Sin(camPos.rotation.eulerAngles.y / 180 * Math.PI);
            spawnLocation.z = camPos.position.z - spawnDistance * (float)Math.Cos(camPos.rotation.eulerAngles.y / 180 * Math.PI);

        }
        else
        {
            spawnLocation.x = camPos.position.x + spawnDistance * (float)Math.Sin(camPos.rotation.eulerAngles.y / 180 * Math.PI);
            spawnLocation.z = camPos.position.z + spawnDistance * (float)Math.Cos(camPos.rotation.eulerAngles.y / 180 * Math.PI);
        }
        spawnLocation.y = camPos.position.y;

        return spawnLocation;
    }

    private Vector3 UnityAnglesToNormal()
    {
        Vector3 rotation = new Vector3();
        return rotation;
    }


}
