using UnityEngine;

namespace Asteroids.Utils
{
    public static class Utils 
    {
        public static Quaternion GetRandomRotation2D() => Quaternion.Euler(0, 0, Random.Range(0, 360));
    }
}