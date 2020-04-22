using UnityEngine;

namespace Src
{
    public static class Utils
    {
        public static Vector2 GetRandom2dPoint(Rect bounds)
        {
            var x = Random.Range(bounds.xMin, bounds.xMax);
            var y = Random.Range(bounds.yMin, bounds.yMax);
            return new Vector2(x,y);
        }
    }
}