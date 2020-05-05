using Src.DataComponents;
using Unity.Entities;

namespace Src.Systems
{
    public class RecoverySystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var time = UnityEngine.Time.time;
            Entities.ForEach((ref CreatureData data) =>
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
            });
        }
    }
}