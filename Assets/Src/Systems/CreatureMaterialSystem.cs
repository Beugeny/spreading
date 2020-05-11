using Src.DataComponents;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

namespace Src.Systems
{
    [UpdateAfter(typeof(InfectionSystem))]
    public class CreatureMaterialSystem : ComponentSystem
    {
        private Material _recoveredMaterial;
        private Material _infectedMaterial;
        private Material _withoutImmunityMaterial;


        protected override void OnCreate()
        {
            base.OnCreate();
            _infectedMaterial = new Material(Shader.Find("Specular"));
            _infectedMaterial.color = Color.red;
            _infectedMaterial.enableInstancing = true;

            _recoveredMaterial = new Material(Shader.Find("Specular"));
            _recoveredMaterial.color = Color.green;
            _recoveredMaterial.enableInstancing = true;

            _withoutImmunityMaterial = new Material(Shader.Find("Specular"));
            _withoutImmunityMaterial.color = Color.yellow;
            _withoutImmunityMaterial.enableInstancing = true;
        }


        protected override void OnUpdate()
        {
            Entities.ForEach(
                (Entity entity, ref CreatureData data, ref OnInfectionChangedEvent evt) =>
                {
                    var render = EntityManager.GetSharedComponentData<RenderMesh>(entity);
                    if (data.IsInfected)
                    {
                        EntityManager.SetSharedComponentData(entity,
                            new RenderMesh() {mesh = render.mesh, material = _infectedMaterial});
                    }
                    else if (data.HasImmunity)
                    {
                        EntityManager.SetSharedComponentData(entity,
                            new RenderMesh() {mesh = render.mesh, material = _recoveredMaterial});
                    }
                    else
                    {
                        EntityManager.SetSharedComponentData(entity,
                            new RenderMesh() {mesh = render.mesh, material = _withoutImmunityMaterial});
                    }
                });
        }
    }
}