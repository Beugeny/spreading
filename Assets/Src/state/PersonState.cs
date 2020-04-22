using UnityEngine;

namespace Src.state
{
    public class PersonState
    {
        public Vector3 TargetPoint;
        public bool MovingToTargetPoint = false;
        public bool IsInfected = false;
        public bool HasImmunity = false;
        public float TimeWhenInfected = 0;
    }
}