
using Assets.Scripts.Physix.Data;
using UnityEngine;

namespace Assets.Scripts.Physix.Models
{
    public class PhysixData
    {
        public Force force;
        public Vector3 speed;
        public float mass;
        public PhysixData()
        {
            speed = new Vector3();
            force = new Force();
            mass = 1000f; // temp
        }



    }
}
