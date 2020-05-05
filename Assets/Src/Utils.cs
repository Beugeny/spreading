using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Src
{
    public static class Utils
    {
        public static float2 GetRandomFloat2(Random r, Rect bounds)
        {
            return new float2(r.NextFloat(bounds.xMin, bounds.xMax), r.NextFloat(bounds.yMin, bounds.yMax));
        }

        public static float3 GetRandomFloat2OnPlane(Random r,Rect bounds, float y)
        {
            return new float3(r.NextFloat(bounds.xMin, bounds.xMax),y, r.NextFloat(bounds.yMin, bounds.yMax));
        }
    }
}