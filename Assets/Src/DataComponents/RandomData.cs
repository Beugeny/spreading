using Unity.Entities;

namespace Src.DataComponents
{
    [GenerateAuthoringComponent]
    public struct RandomData:IComponentData
    {
        public Unity.Mathematics.Random Random;
    }
}