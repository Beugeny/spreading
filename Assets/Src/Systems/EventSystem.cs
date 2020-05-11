using System;
using Src.DataComponents;
using Unity.Entities;
using Unity.Jobs;

namespace Src.Systems
{
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    public class EventSystem:ComponentSystem
    {
        private EndSimulationEntityCommandBufferSystem _commandBuffer;
        public event EventHandler OnSmbInfected;
        
        protected override void OnCreate()
        {
            base.OnCreate();
            _commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        
        protected override void OnUpdate()
        {
            var commandBuffer=_commandBuffer.CreateCommandBuffer();
            Entities.ForEach((Entity entity,ref EventComponent evt) =>
            {
                 // OnSmbInfected?.Invoke(this, EventArgs.Empty);
                 commandBuffer.DestroyEntity(entity);
            });
        }
    }
}