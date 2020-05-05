using Unity.Entities;

namespace Src.DataComponents
{
    [GenerateAuthoringComponent]
    public struct CreatureData : IComponentData
    {
        public bool IsInfected;
        public bool HasImmunity;
        public float InfectionTimestamp;
        public int InfectionDuration;
        public bool InfectedHasChanged;
    }
}