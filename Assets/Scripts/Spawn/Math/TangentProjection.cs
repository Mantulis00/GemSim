using System;
using UnityEngine;

/// <summary>
///  Purpose: takes object to be cloned, mouse position on the screen, distance to tangent plane
///  when SetupSpawnDistance() is called chosen object is spawned in the location where mouse was when it was called
/// </summary>



namespace Assets.Scripts.Spawn.TangentProjection
{
    public static class TangentProjection
    {
        public static Vector3 SetupSpawnDistance(Transform camPos, Vector2 mousePos, float tangentDistance)
        {
            Vector3 spawnLocation = new Vector3();
            Vector3 tangentProjectionOffsets = ProjectToScreen(camPos.gameObject.GetComponent<Camera>(), mousePos, tangentDistance);


            if (Math.Abs(camPos.rotation.y + ProjectedAngle(camPos.gameObject.GetComponent<Camera>(), mousePos.x, true)) >= 90) // temp. need 2b cleaned (for adjusting to unity angles)
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
                angle = (float)Math.Atan((2 * pos / cam.scaledPixelWidth - 1) * axisFOV); // (IMG) FOV_1 attached formula to get +- (center around middle)
            else
            {
                angle = 2 * (float)Math.Atan((2 * pos / cam.scaledPixelHeight - 1) * axisFOV); // (IMG) FOV_1 attached
            }



            return angle; // angle projected from screen \ | /
                                                       // \|/
        }

        public static Vector3 ProjectedAngle(Camera cam, Vector2 pos)
        {
            float axisFOV_H, axisFOV_V;


                axisFOV_H = (float)Math.Tan(cam.fieldOfView / 360 * Math.PI) * cam.aspect; // tan of horizontal fov

                axisFOV_V = (float)Math.Tan(cam.fieldOfView / 360 * Math.PI) / cam.aspect; // tan of horizontal fov


            Vector3 angle = new Vector3();


                angle.y = (float)Math.Atan((2 * pos.x / cam.scaledPixelWidth - 1) * axisFOV_H); // (IMG) FOV_1 attached formula to get +- (center around middle)
                angle.x = 2 * (float)Math.Atan((2 * pos.y / cam.scaledPixelHeight - 1) * axisFOV_V); // (IMG) FOV_1 attached


            angle *= 180 / (float)Math.PI;



            return angle;
        }

        public static Vector3 Short(Vector3 fromC, Vector3 toC, Vector3 vecC)
        {
             Vector3 vec = new Vector3(
                 (float)Math.Cos(vecC.x / 180 * Math.PI)* (float)Math.Sin(vecC.y / 180 * Math.PI),
                 (float)Math.Sin(vecC.x / 180 * Math.PI),
                 (float)Math.Cos(vecC.x / 180 * Math.PI) * (float)Math.Cos(vecC.y / 180 * Math.PI));
            

            Vector3 coor = new Vector3();
            float lambda =
                vec.x * (toC.x - fromC.x) +
                vec.y * (toC.y - fromC.y) +
                vec.z * (toC.z - fromC.z);

            lambda /=
                vec.x * vec.x +
                vec.y * vec.y +
                vec.z * vec.z;

            coor.x = fromC.x + lambda * vec.x;
            coor.y = fromC.y + lambda * vec.y;
            coor.z = fromC.z + lambda * vec.z;

            Debug.Log(vec);



            return coor;
        }


    }
}
