using Src.ui;
using UnityEngine;
using UnityEngine.UI;

namespace Src
{
    public class Launcher : MonoBehaviour
    {
        [SerializeField] public GameObject ArenaGo;
        [SerializeField] public GameObject PersonPrefab;
        [SerializeField] public ArenaUiComps ArenaUiComps;
        [SerializeField] public Button BtnStartNewArena;

        private Game _game;

        public void Start()
        {
            BtnStartNewArena.onClick.AddListener(onStartArena);
            _game = new Game(ArenaGo, PersonPrefab, ArenaUiComps);
        }

        private void onStartArena()
        {
            _game.RestartArena();
        }


        private void Update()
        {
            _game.Update();
        }
    }
}