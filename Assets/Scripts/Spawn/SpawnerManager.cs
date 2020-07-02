using Assets.Scripts.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

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
        Vector3 tangentProjectionOffsets = ProjectToScreen(camPos.gameObject.GetComponent<Camera>(), mousePos);



        if (Math.Abs(camPos.rotation.y + ProjectedAngle(camPos.gameObject.GetComponent<Camera>(), mousePos.x, true)) >= 90) // temp. need 2b cleaned
        {
            spawnLocation.x = camPos.position.x - tangentProjectionOffsets.x;
            spawnLocation.z = camPos.position.z - (tangentProjectionOffsets.z);
        }
        else
        {
            spawnLocation.x = camPos.position.x + (tangentProjectionOffsets.x);
            spawnLocation.z = camPos.position.z + (tangentProjectionOffsets.z);
        }



        spawnLocation.y = camPos.position.y + tangentProjectionOffsets.y;

        return spawnLocation;
    }


    private Vector3 ProjectToScreen(Camera cam, Vector2 mousePos)
    {
        Vector3 distances = new Vector3();

       // first spawn  directly at the middle of the screen with vertical adjustments 
       // img_P1

        float verticalAngle = ProjectedAngle(cam, mousePos.y, false);
        float distanceByVertical = spawnDistance / (float)(Math.Cos(verticalAngle));


        distances.y = distanceByVertical *
            (float)Math.Sin(-(cam.transform.rotation.eulerAngles.x * Math.PI / 180) + verticalAngle); // get hight (only line that regulates y coordinate)

        distances.x = distanceByVertical *
            (float)Math.Cos(-(cam.transform.rotation.eulerAngles.x * Math.PI / 180) + verticalAngle) * // adjust tu vertical rotation
            (float)Math.Sin(cam.transform.rotation.eulerAngles.y * Math.PI / 180  ); // adjust to horzontal rotation

        distances.z = distanceByVertical *
            (float)Math.Cos(-(cam.transform.rotation.eulerAngles.x * Math.PI / 180) + verticalAngle) * // adjust tu vertical rotation
            (float)Math.Cos(cam.transform.rotation.eulerAngles.y * Math.PI / 180  ); // adjust tu horizontal rotation

        // then adjust horizontally
        // img_P2

        float horizontalAngle = ProjectedAngle(cam, mousePos.x, true);
        float distanceByHorizontal = spawnDistance * (float)(Math.Tan(horizontalAngle));

        distances.x += distanceByHorizontal * (float)Math.Cos(cam.transform.rotation.eulerAngles.y * Math.PI / 180 );
        distances.z -= distanceByHorizontal * (float)Math.Sin(cam.transform.rotation.eulerAngles.y * Math.PI / 180 );

        return distances;
    }


        private float ProjectedAngle(Camera cam, float pos, bool horizontal) 
    {
        float axisFOV;

        if (horizontal)
        {
            axisFOV = (float)Math.Tan(cam.fieldOfView / 360 * Math.PI) * cam.aspect; // tan of horizontal fov
        }
        else
        {
            axisFOV = (float)Math.Tan(cam.fieldOfView / 360 * Math.PI) / cam.aspect; // tan of horizontal fov
        }

        float angle;

        if (horizontal)
             angle = (float)Math.Atan((2 * pos / cam.scaledPixelWidth - 1) * axisFOV); // (IMG) FOV_1 attached
        else
        {
            angle = 2 * (float)Math.Atan((2 * pos / cam.scaledPixelHeight - 1) * axisFOV); // (IMG) FOV_1 attached
        }

        Debug.Log(angle * 180/Math.PI);
   

        return angle;
    }

 


}
