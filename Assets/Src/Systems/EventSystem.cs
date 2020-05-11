using System;
using Src.DataComponents;
using Unity.Entities;

namespace Src.Systems
{
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    public class EventSystem : ComponentSystem
    {
        private EndSimulationEntityCommandBufferSystem _commandBuffer;
        // public event EventHandler<Entity> OnSmbInfected;

        protected override void OnCreate()
        {
            base.OnCreate();
            _commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }


        protected override void OnUpdate()
        {
            var commandBuffer = _commandBuffer.CreateCommandBuffer();
            Entities.ForEach((Entity entity, ref OnInfectionChangedEvent evt) =>
            {
                // OnSmbInfected?.Invoke(this, entity);
                commandBuffer.RemoveComponent<OnInfectionChangedEvent>(entity);
            });
        }
    }
}