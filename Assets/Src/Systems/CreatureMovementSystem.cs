using Src.DataComponents;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Rendering;
using UnityEngine;
using Math = System.Math;

namespace Src.Systems
{
    public class CreatureMovementSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            float deltaTime = Time.DeltaTime;
            Rect bounds = new Rect(-5000, -5000, 10000, 10000);

            return Entities
                .ForEach(
                    (ref PhysicsVelocity vel, ref WorldRenderBounds bnds, ref MovementData data,
                        ref RandomData randomData) =>
                    {
                        if (Math.Abs(data.TargetPoint.x) < float.Epsilon &&
                            Math.Abs(data.TargetPoint.y) < float.Epsilon)
                        {
                            data.TargetPoint = Utils.GetRandomFloat2(randomData.Random, bounds);
                        }

                        float2 delta = data.TargetPoint - bnds.Value.Center.xz;
                        float tmp = data.Speed * deltaTime;
                        if (math.lengthsq(delta) > data.Speed * deltaTime)
                        {
                            vel.Linear.xz = tmp * math.normalize(delta);
                        }
                        else
                        {
                            data.TargetPoint = float2.zero;
                        }
                    }).Schedule(inputDeps);
        }
    }
}