using Src.info;
using Src.ui;
using Src.utils;
using UnityEngine;

namespace Src
{
    public class Game : IService
    {
        private ArenaProperties _props;
        private Arena _arena;
        private MainStatUi _arenaUi;
        private bool _isStarted = false;
        
        private readonly ArenaUiComps _arenaUiComps;
        private readonly GameObject _arenaGo;
        private readonly GameObject _personPrefab;

        public Game(GameObject arenaGo, GameObject personPrefab, ArenaUiComps arenaUiComps)
        {
            _personPrefab = personPrefab;
            _arenaGo = arenaGo;
            _arenaUiComps = arenaUiComps;
            _arenaUiComps.gameObject.SetActive(false);
            _arenaGo.gameObject.SetActive(false);
        }

        public void RestartArena()
        {
            StopArena();
            StartNewArena();
        }

        public void StartNewArena()
        {
            if (_isStarted) return;

            _arenaUiComps.gameObject.SetActive(true);
            _arenaGo.gameObject.SetActive(true);
            _isStarted = true;
            _props = new ArenaProperties(10000, _arenaGo, _personPrefab, 100f, 0.5f,50000,50000);
            _arena = new Arena(_props);
            _arena.InfectRandom(new InfectionProps(60));
            _arenaUi = new MainStatUi(_arenaUiComps, _arena.State, _arena);
        }

        private void StopArena()
        {
            if (_isStarted)
            {
                _isStarted = false;
                _arenaUi?.Dispose();
                _arena?.Dispose();
                _arenaUiComps.gameObject.SetActive(false);
                _arenaGo.gameObject.SetActive(false);
            }
        }


        public void Update()
        {
            if (_isStarted)
            {
                _arena.Update();
                _arenaUi.Update();
            }
        }

        public void Dispose()
        {
            StopArena();
        }
    }
}