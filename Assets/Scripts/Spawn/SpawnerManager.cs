using Assets.Scripts.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.Animations;

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

        float spawnProjected_x = spawnDistance * 
            (float)Math.Sin(camPos.rotation.eulerAngles.y * Math.PI / 180 +
            ProjectedAngle(camPos.gameObject.GetComponent<Camera>(), mousePos.x, true) ); 

        float spawnProjected_z = spawnDistance *
                 (float)Math.Cos(camPos.rotation.eulerAngles.y * Math.PI / 180 +
                 ProjectedAngle(camPos.gameObject.GetComponent<Camera>(), mousePos.x, true) );

        float spawnProjected_y = spawnDistance *
                (float)Math.Sin(camPos.rotation.eulerAngles.x * Math.PI / 180 +
                ProjectedAngle(camPos.gameObject.GetComponent<Camera>(), mousePos.y, false) );

        //Debug.Log(ProjectedAngle(camPos.gameObject.GetComponent<Camera>(), mousePos.x, true));

        if (Math.Abs(camPos.rotation.y + ProjectedAngle(camPos.gameObject.GetComponent<Camera>(), mousePos.x, true)) > 90)
        {
            spawnLocation.x = camPos.position.x - spawnProjected_x;
            spawnLocation.z = camPos.position.z - spawnProjected_z;
        }
        else
        {
            spawnLocation.x = camPos.position.x + spawnProjected_x;
            spawnLocation.z = camPos.position.z + spawnProjected_z;
        }
        spawnLocation.y = camPos.position.y + spawnProjected_y;

        return spawnLocation;
    }

    private float ProjectedAngle(Camera cam, float pos, bool horizontal)
    {
        float axisFOV;

        axisFOV = (float)Math.Tan(cam.fieldOfView / 360 * Math.PI) * cam.aspect; // tan of horizontal fov

        if (!horizontal)
        {
            axisFOV = (float)(Math.Atan(axisFOV) * 180 / Math.PI);
            axisFOV /= cam.aspect;
            axisFOV = (float)(Math.Tan(axisFOV/180*Math.PI));
           
        }
      //  if (!horizontal)
           



        float angle;

        if (horizontal)
             angle = (float)Math.Atan((2 * pos / cam.scaledPixelWidth - 1) * axisFOV); // (IMG) FOV_1 attached
        else
            angle = (float)Math.Atan((2 * pos / cam.scaledPixelHeight - 1) * axisFOV); // (IMG) FOV_1 attached

         Debug.Log(angle);

        return angle;
    }


}
