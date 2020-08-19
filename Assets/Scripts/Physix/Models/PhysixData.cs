
using Assets.Scripts.Physix.Data;

namespace Assets.Scripts.Physix.Models
{
    public class PhysixData
    {
        public Force force;
        public Speed speed;
        public PhysixData()
        {
            speed = new Speed();
            force = new Force(); 
        }



    }
}
