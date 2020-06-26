using Assets.Scripts.Controls;
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


    public void MakeEndPoints(Transform cameraPos, GameObject obModel, Vector2 mouseLocation, bool first)
    {
        GameObject go;
        go = Instantiate(obModel) as GameObject;
        go.transform.SetParent(this.transform);


        go.transform.position = SetupSpawnDistance(cameraPos, mouseLocation); // location + mqpw

        Vector3 goRotation = cameraPos.transform.rotation.eulerAngles;
        goRotation.x = 0;
        go.transform.rotation = Quaternion.Euler(goRotation);



    }

    public void ConnectEndPoints()
    {

    }

    private Vector3 SetupSpawnDistance(Transform camPos, Vector2 mousePos)
    {
        Vector3 spawnLocation = new Vector3();

        Debug.Log(ProjectedAngle(camPos.gameObject.GetComponent<Camera>(), mousePos.x));
        Debug.Log(camPos.rotation.eulerAngles.y);

        if (Math.Abs(camPos.rotation.y + ProjectedAngle(camPos.gameObject.GetComponent<Camera>(), mousePos.x)) > 90)
        {
            spawnLocation.x = camPos.position.x - spawnDistance *
                 (float)Math.Sin((camPos.rotation.eulerAngles.y + ProjectedAngle(camPos.gameObject.GetComponent<Camera>(), mousePos.x)) * (Math.PI / 180f));
            spawnLocation.z = camPos.position.z - spawnDistance *
                 (float)Math.Cos((camPos.rotation.eulerAngles.y + ProjectedAngle(camPos.gameObject.GetComponent<Camera>(), mousePos.x)) * (Math.PI / 180f));

        }
        else
        {
            spawnLocation.x = camPos.position.x + spawnDistance * 
                (float)Math.Sin((camPos.rotation.eulerAngles.y + ProjectedAngle(camPos.gameObject.GetComponent<Camera>(), mousePos.x)) *(Math.PI/180f));
            spawnLocation.z = camPos.position.z + spawnDistance *
                (float)Math.Cos((camPos.rotation.eulerAngles.y + ProjectedAngle(camPos.gameObject.GetComponent<Camera>(), mousePos.x)) * (Math.PI / 180f));
        }
        spawnLocation.y = camPos.position.y;

        return spawnLocation;
    }

    private float ProjectedAngle(Camera cam, float pos)
    {
        float angle;
       
            angle = 2 * cam.fieldOfView/4*3 * ( pos / cam.pixelWidth - 0.5f );
           // Debug.Log(angle);
       
       
        return angle;
    }


}
