using System;
using Src.info;
using Src.state;
using Src.utils;
using UnityEngine;

namespace Src.ui
{
    public class MainStatUi:IService
    {
        private readonly ArenaUiComps _comps;
        private readonly IReadOnlyPopulationState _state;
        private readonly Arena _arena;

        public MainStatUi(ArenaUiComps comps, IReadOnlyPopulationState state,Arena arena)
        {
            _arena = arena;
            _state = state;
            _comps = comps;
            
            _comps.BtnInfectSomebody.onClick.AddListener(onClick);
        }

        private void onClick()
        {
            _arena.InfectRandom(new InfectionProps(30));
        }

        public void Update()
        {
            _comps.TxtPopulation.text = $"Population: {_state.Population}/{_state.StartPopulation}";
            _comps.TxtCases.text =
                $"Active cases {_state.TotalCases} ({percentToString((float) _state.TotalCases / _state.StartPopulation)}%)";
            _comps.InfectedTotal.text =
                $"Total cases {_state.TotalCases + _state.TotalRecovered}" +
                $" ({percentToString((float) (_state.TotalCases + _state.TotalRecovered) / _state.StartPopulation)}%)";
                
            _comps.TxtRecovered.text =
                $"Recovered: {_state.TotalRecovered} ({percentToString((float) _state.TotalRecovered / _state.StartPopulation)}%)";
            _comps.TxtTotalDuration.text =
                $"Duration: {Mathf.RoundToInt(_state.TotalDuration)} sec.";
        }


        private string percentToString(float percent)
        {
            percent = percent * 100;
            return Math.Round(percent, 1).ToString();
        }

        public void Dispose()
        {
            _comps.BtnInfectSomebody.onClick.RemoveListener(onClick);
        }
    }
}