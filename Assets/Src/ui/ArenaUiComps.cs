using UnityEngine;
using UnityEngine.UI;

namespace Src.ui
{
    public class ArenaUiComps:MonoBehaviour
    {
        [SerializeField] public Text TxtPopulation;
        [SerializeField] public Text TxtCases;
        [SerializeField] public Text TxtRecovered;
        [SerializeField] public Text InfectedTotal;
        [SerializeField] public Text TxtTotalDuration;
        [SerializeField] public Button BtnInfectSomebody;
    }
}