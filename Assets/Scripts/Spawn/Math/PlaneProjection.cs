using UnityEngine;
using System;

namespace Assets.Scripts.Spawn.Matricies
{
    static class Matricies
    {
       
        private static void GetVectors(Vector3 rotationRadians, ref Vector3 verticalVector, ref Vector3 horizontalVector, PlaneTypes type)
        {
            if (type == PlaneTypes.Ordinary)
            {
                verticalVector.x = (float)(Math.Sin(rotationRadians.x) * Math.Sin(rotationRadians.y));
                verticalVector.y = (float)(Math.Cos(rotationRadians.x));
                verticalVector.z = (float)(Math.Sin(rotationRadians.x) * Math.Cos(rotationRadians.y));

                horizontalVector.z = (float)Math.Sin(rotationRadians.y);
                horizontalVector.x = -(float)Math.Cos(rotationRadians.y);
            }
            else if (type == PlaneTypes.Perpendicular)
            {
                verticalVector.x = (float)(Math.Sin(rotationRadians.x) * Math.Cos(rotationRadians.y));
                verticalVector.z = (float)(Math.Sin(rotationRadians.x) * Math.Sin(rotationRadians.y));
                verticalVector.y = (float)(Math.Cos(rotationRadians.x));

                horizontalVector.x = (float)Math.Sin(rotationRadians.y);
                horizontalVector.z = (float)Math.Cos(rotationRadians.y);
            }

        }

        private static Plane GetPlane(Vector3 rotation, Vector3 pointOnPlane, PlaneTypes type) // XY plane
        {
            Vector3 rotationRadians = rotation / 180 * (float)Math.PI;

            Vector3 verticalVector = new Vector3(); // 1st vec
            Vector3 horizontalVector = new Vector3(); // 2nd vec

            GetVectors(rotationRadians, ref verticalVector, ref horizontalVector, type);

            Plane projectedPlane = new Plane(Vector3.Cross(verticalVector, horizontalVector), pointOnPlane);
            return projectedPlane;
        }





        public static Vector3 MoveToRayedPlanePossition(GameObject go, GameObject goAround)
        {
            Plane plane = GetPlane(goAround.transform.rotation.eulerAngles, goAround.transform.position, PlaneTypes.Ordinary); // ?TBC generate plane when rotation changes, give plane as structure root feature

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //sketchy if lags trash optimisation ?TBC


            if (plane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);

                hitPoint = hitPoint - goAround.transform.position ;

                return hitPoint * ((go.transform.position - goAround.transform.position).magnitude / hitPoint.magnitude);
               // go.transform.position = goAround.transform.position + 
            }
            return new Vector3();

        }

        public static void DoubleAxisPlanePossition(GameObject go, GameObject goAround, float maxAngle)
        {
            Plane plane = GetPlane(goAround.transform.rotation.eulerAngles, goAround.transform.position, PlaneTypes.Ordinary); // ?TBC generate plane when rotation changes, give plane as structure root feature
            Plane normalPlane = GetPlane(goAround.transform.rotation.eulerAngles, goAround.transform.position, PlaneTypes.Perpendicular);

        }





    }
}
