using UnityEngine;
using System;

namespace Assets.Scripts.Spawn.Matricies
{
    static class Matricies
    {
       

        public static Plane GetPlane(Vector3 rotation, Vector3 pointOnPlane)
        {
            Vector3 rotationRadians = new Vector3();
            rotationRadians.x = rotation.x / 180 * (float)Math.PI;
            rotationRadians.y = rotation.y / 180 * (float)Math.PI;
            rotationRadians.z = rotation.z / 180 * (float)Math.PI;

            Vector3 verticalVector = new Vector3(); // 1st vec
            verticalVector.x = (float)(Math.Sin(rotationRadians.x) * Math.Sin(rotationRadians.y));
            verticalVector.y = (float)(Math.Cos(rotationRadians.x));
            verticalVector.z = (float)(Math.Sin(rotationRadians.x) * Math.Cos(rotationRadians.y));

            Vector3 horizontalVector = new Vector3(); // 2nd vec
            horizontalVector.z = (float)Math.Sin(rotationRadians.y);
            horizontalVector.x = -(float)Math.Cos(rotationRadians.y);

            Plane projectedPlane = new Plane(Vector3.Cross(verticalVector, horizontalVector), pointOnPlane);
            return projectedPlane;
        }


        public static void MoveToRayedPlanePossition(Vector3 rotation, Vector3 pointOnPlane, GameObject go)
        {
            Plane plane = GetPlane(rotation, pointOnPlane); // rotation = go.transform.rotation.euler

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float enter = 0f;

            if (plane.Raycast(ray, out enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);

                go.transform.position = hitPoint;
            }

        }

        public static void MoveToRayedPlanePossition(GameObject go, GameObject goAround, float magnitude)
        {
            Plane plane = GetPlane(go.transform.rotation.eulerAngles, go.transform.position); // rotation = go.transform.rotation.euler

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float enter = 0f;

            if (plane.Raycast(ray, out enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);



                hitPoint = hitPoint - goAround.transform.position ;

                go.transform.position = goAround.transform.position + hitPoint/hitPoint.magnitude*magnitude;
            }

        }




    }
}
