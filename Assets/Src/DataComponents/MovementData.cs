using Unity.Entities;
using Unity.Mathematics;

namespace Src.DataComponents
{
    [GenerateAuthoringComponent]
    public struct MovementData:IComponentData
    {
        public float Speed;
        public float2 TargetPoint;
    }
}