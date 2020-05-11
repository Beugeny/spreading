using Src.Systems;
using Unity.Entities;
using UnityEngine;

namespace Src
{
    public class EventTest : MonoBehaviour
    {
        private void Start()
        {
            var system = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<EventSystem>();
            system.OnSmbInfected += (sender, args) => { Debug.Log("Smb infected"); };
        }
    }
}