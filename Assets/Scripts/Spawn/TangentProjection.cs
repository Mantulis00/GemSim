using System;
using UnityEngine;

/// <summary>
///  Purpose: takes objact to be cloned, mouse position on the screen, distance to tangent plane
///  when SetupSpawnDistance() is called chosen object is spawned in the location when mouse was when it was called
/// </summary>



namespace Assets.Scripts.Spawn
{
    public static class TangentProjection
    {
        public static Vector3 SetupSpawnDistance(Transform camPos, Vector2 mousePos, float tangentDistance)
        {
            Vector3 spawnLocation = new Vector3();
            Vector3 tangentProjectionOffsets = ProjectToScreen(camPos.gameObject.GetComponent<Camera>(), mousePos, tangentDistance);


            if (Math.Abs(camPos.rotation.y + ProjectedAngle(camPos.gameObject.GetComponent<Camera>(), mousePos.x, true)) >= 90) // temp. need 2b cleaned
            {
                spawnLocation.x = camPos.position.x - tangentProjectionOffsets.x;
                spawnLocation.z = camPos.position.z - tangentProjectionOffsets.z;
            }
            else
            {
                spawnLocation.x = camPos.position.x + tangentProjectionOffsets.x;
                spawnLocation.z = camPos.position.z + tangentProjectionOffsets.z;
            }

            spawnLocation.y = camPos.position.y + tangentProjectionOffsets.y;

            return spawnLocation;
        }


        private static  Vector3 ProjectToScreen(Camera cam, Vector2 mousePos, float tangentDistance)
        {
            Vector3 distances = new Vector3();

            // first spawn  directly at the middle of the screen with vertical adjustments 
            // img_P1

            float verticalAngle = ProjectedAngle(cam, mousePos.y, false);
            float distanceByVertical = tangentDistance / (float)(Math.Cos(verticalAngle));


            distances.y = distanceByVertical *
                (float)Math.Sin(-(cam.transform.rotation.eulerAngles.x * Math.PI / 180) + verticalAngle); // get height (only line that regulates y coordinate)

            distances.x = distanceByVertical *
                (float)Math.Cos(-(cam.transform.rotation.eulerAngles.x * Math.PI / 180) + verticalAngle) * // adjust to vertical rotation
                (float)Math.Sin(cam.transform.rotation.eulerAngles.y * Math.PI / 180); // adjust to horzontal rotation

            distances.z = distanceByVertical *
                (float)Math.Cos(-(cam.transform.rotation.eulerAngles.x * Math.PI / 180) + verticalAngle) * // adjust to vertical rotation
                (float)Math.Cos(cam.transform.rotation.eulerAngles.y * Math.PI / 180); // adjust tu horizontal rotation

            // then adjust horizontally
            // img_P2

            float horizontalAngle = ProjectedAngle(cam, mousePos.x, true);
            float distanceByHorizontal = tangentDistance * (float)(Math.Tan(horizontalAngle));

            distances.x += distanceByHorizontal * (float)Math.Cos(cam.transform.rotation.eulerAngles.y * Math.PI / 180);
            distances.z -= distanceByHorizontal * (float)Math.Sin(cam.transform.rotation.eulerAngles.y * Math.PI / 180);

            return distances;
        }


        private static float ProjectedAngle(Camera cam, float pos, bool horizontal)
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



            return angle;
        }

    }
}
