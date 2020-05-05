using Src.DataComponents;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Src.Systems
{
    // public class DeleteEntitySystem:JobComponentSystem
    // {
    //     protected override JobHandle OnUpdate(JobHandle inputDeps)
    //     {
    //         EntityCommandBuffer commandBuffer=new EntityCommandBuffer(Allocator.TempJob);
    //         Entities
    //             .WithAll<DeleteTag>()
    //             .ForEach((Entity entity) =>
    //             {
    //                 commandBuffer.DestroyEntity(entity);
    //             }).Run();
    //         
    //         commandBuffer.Playback(EntityManager);
    //         commandBuffer.Dispose();
    //
    //         return default;
    //     }
    // }
}