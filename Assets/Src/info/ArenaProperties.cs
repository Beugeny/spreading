using UnityEngine;

namespace Src.info
{
    public readonly struct ArenaProperties
    {
        public readonly int Population;
        public readonly GameObject ArenaObject;
        public readonly GameObject PersonObject;
        public readonly float PersonSpeed;
        public readonly float PercentAtIsolation;
        public readonly float WorldWidth;
        public readonly float WorldHeight;

        public ArenaProperties(int population,
            GameObject arenaObject,
            GameObject personObject, 
            float personSpeed,
            float percentAtIsolation,
            int worldWidth,
            int worldHeight)
        {
            WorldWidth = worldWidth;
            WorldHeight = worldHeight;
            Population = population;
            this.ArenaObject = arenaObject;
            PersonObject = personObject;
            PersonSpeed = personSpeed;
            PercentAtIsolation = percentAtIsolation;
        }
    }
}