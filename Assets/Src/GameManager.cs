using Src.DataComponents;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = System.Random;

namespace Src
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] public GameObject CreaturePrefab;
        [SerializeField] public Transform Plane;
        [SerializeField] public int CreatureCount;

        private Entity _creatureEntityPrefab;
        private BlobAssetStore _assetStore;
        private EntityManager _manager;
        private Rect _bounds;
        private Random _r = new Random();

        private void Awake()
        {
            _assetStore = new BlobAssetStore();
            _manager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var settings =
                GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, new BlobAssetStore());
            _creatureEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(CreaturePrefab, settings);

            var t = Plane.transform;
            var pos = t.position;
            var scale = t.localScale;
            _bounds = new Rect(pos.x - scale.x / 2, pos.y - scale.y / 2, scale.x, scale.y);

            SpawnCreatures();
        }

        private void SpawnCreatures()
        {
            for (int i = 0; i < CreatureCount; i++)
            {
                SpawnCreature(i %10== 0);
            }
        }

        private void SpawnCreature(bool isInfected)
        {
            Entity newCreature = _manager.Instantiate(_creatureEntityPrefab);
            _manager.AddComponent(newCreature, typeof(MovementData));
            _manager.AddComponent(newCreature, typeof(CreatureData));
            _manager.AddComponent(newCreature, typeof(RandomData));

            _manager.SetComponentData(newCreature, new RandomData
            {
                Random = new Unity.Mathematics.Random((uint) _r.Next())
            });
            _manager.SetComponentData(newCreature, new Translation
            {
                Value = Utils.GetRandomFloat2OnPlane(new Unity.Mathematics.Random((uint) _r.Next()), _bounds, 1)
            });
            _manager.SetComponentData(newCreature, new MovementData {Speed = 2500, TargetPoint = float2.zero});
            _manager.SetComponentData(newCreature, new CreatureData
            {
                InfectedHasChanged = isInfected,
                IsInfected = isInfected, InfectionTimestamp = isInfected ? Time.time : 0, InfectionDuration = 20
            });
        }

        private void OnDestroy()
        {
            _assetStore.Dispose();
        }
    }
}