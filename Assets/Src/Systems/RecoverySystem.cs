using Src.DataComponents;
using Unity.Entities;
using Unity.Jobs;

namespace Src.Systems
{
    public class RecoverySystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var time = UnityEngine.Time.time;
            return Entities.ForEach((ref CreatureData data) =>
            {
                if (data.IsInfected)
                {
                    var deltaTime = time - data.InfectionTimestamp;
                    if (data.InfectionDuration < deltaTime)
                    {
                        data.HasImmunity = true;
                        data.IsInfected = false;
                        data.InfectedHasChanged = true;
                    }
                }
            }).Schedule(inputDeps);
        }
    }
}