using Src.DataComponents;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;

namespace Src.Systems
{
    public class InfectionSystem : JobComponentSystem
    {
        private BuildPhysicsWorld _buildPhysicsWorld;
        private StepPhysicsWorld _stepPhysicsWorld;
        private EndSimulationEntityCommandBufferSystem _commandBuffer;


        protected override void OnCreate()
        {
            base.OnCreate();
            _buildPhysicsWorld = World.GetExistingSystem<BuildPhysicsWorld>();
            _stepPhysicsWorld = World.GetExistingSystem<StepPhysicsWorld>();
            _commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var currentTime = UnityEngine.Time.time;
            var entityCommandBuffer = _commandBuffer.CreateCommandBuffer();
            var concurrentCommandBuffer = entityCommandBuffer.ToConcurrent();
            var triggerJob = new TriggerJob(GetComponentDataFromEntity<CreatureData>(), currentTime,
                concurrentCommandBuffer);
            var jh = triggerJob.Schedule(_stepPhysicsWorld.Simulation, ref _buildPhysicsWorld.PhysicsWorld, inputDeps);
            jh.Complete();
            _commandBuffer.AddJobHandleForProducer(jh);
            return jh;
        }

        private struct TriggerJob : ITriggerEventsJob
        {
            private ComponentDataFromEntity<CreatureData> _creatures;
            private readonly float _currentTime;
            private EntityCommandBuffer.Concurrent _concurrentCommandBuffer;


            public TriggerJob(ComponentDataFromEntity<CreatureData> creatures, float currentTime,
                EntityCommandBuffer.Concurrent concurrentCommandBuffer)
            {
                _concurrentCommandBuffer = concurrentCommandBuffer;
                _currentTime = currentTime;
                _creatures = creatures;
            }

            public void Execute(TriggerEvent triggerEvent)
            {
                var a = triggerEvent.Entities.EntityA;
                var b = triggerEvent.Entities.EntityB;
                if (_creatures.HasComponent(a) && _creatures.HasComponent(b))
                {
                    if (_creatures[a].IsInfected || _creatures[b].IsInfected)
                    {
                        var creatureA = _creatures[a];
                        var creatureB = _creatures[b];
                        if (!creatureA.IsInfected && !creatureA.HasImmunity)
                        {
                            creatureA.IsInfected = true;
                            creatureA.InfectionTimestamp = _currentTime;
                            _creatures[a] = creatureA;
                            _concurrentCommandBuffer.AddComponent(0, a, typeof(OnInfectionChangedEvent));
                        }

                        if (!creatureB.IsInfected && !creatureB.HasImmunity)
                        {
                            creatureB.IsInfected = true;
                            creatureB.InfectionTimestamp = _currentTime;
                            _creatures[b] = creatureB;
                            _concurrentCommandBuffer.AddComponent(0, b, typeof(OnInfectionChangedEvent));
                            // _concurrentCommandBuffer.CreateEntity(b.Index,_eventArchetype);
                        }
                    }
                }
            }
        }
    }
}