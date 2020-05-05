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

        protected override void OnCreate()
        {
            base.OnCreate();
            _buildPhysicsWorld = World.GetExistingSystem<BuildPhysicsWorld>();
            _stepPhysicsWorld = World.GetExistingSystem<StepPhysicsWorld>();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var currentTime = UnityEngine.Time.time;
            var triggerJob = new TriggerJob(GetComponentDataFromEntity<CreatureData>(),currentTime);
            return triggerJob.Schedule(_stepPhysicsWorld.Simulation, ref _buildPhysicsWorld.PhysicsWorld, inputDeps);
        }

        private struct TriggerJob : ITriggerEventsJob
        {
            private ComponentDataFromEntity<CreatureData> _creatures;
            private readonly float _currentTime;

            public TriggerJob(ComponentDataFromEntity<CreatureData> creatures, float currentTime)
            {
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
                            creatureA.InfectedHasChanged = true;
                            creatureA.InfectionTimestamp = _currentTime;
                            _creatures[a] = creatureA;
                        }

                        if (!creatureB.IsInfected && !creatureB.HasImmunity)
                        {
                            creatureB.IsInfected = true;
                            creatureB.InfectedHasChanged = true;
                            creatureB.InfectionTimestamp = _currentTime;
                            _creatures[b] = creatureB;
                        }
                    }
                }
            }
        }
    }
}