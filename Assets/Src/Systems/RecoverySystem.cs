using Src.DataComponents;
using Unity.Entities;
using Unity.Jobs;

namespace Src.Systems
{
    public class RecoverySystem : JobComponentSystem
    {
        private EndSimulationEntityCommandBufferSystem _commandBuffer;

        protected override void OnCreate()
        {
            base.OnCreate();
            _commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var time = UnityEngine.Time.time;
            var entityCommandBuffer = _commandBuffer.CreateCommandBuffer();
            var concurrentCommandBuffer = entityCommandBuffer.ToConcurrent();
            return Entities.ForEach((Entity e, ref CreatureData data) =>
            {
                if (data.IsInfected)
                {
                    var deltaTime = time - data.InfectionTimestamp;
                    if (data.InfectionDuration < deltaTime)
                    {
                        data.HasImmunity = true;
                        data.IsInfected = false;
                        concurrentCommandBuffer.AddComponent(0, e, typeof(OnInfectionChangedEvent));
                    }
                }
            }).Schedule(inputDeps);
        }
    }
}