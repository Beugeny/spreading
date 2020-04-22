using System;
using System.Collections.Generic;
using System.Linq;
using Src.info;
using Src.state;
using Src.utils;
using UnityEngine;
using Random = System.Random;

namespace Src
{
    public class Arena:IService
    {
        private readonly List<Person> _persons = new List<Person>();
        private readonly PopulationState _state;
        private readonly RectTransform _arenaRectTransform;
        private MoveController _moveController;

        public Arena(ArenaProperties props)
        {
            _state=new PopulationState();
            _state.StartPopulation = props.Population;

            _arenaRectTransform = props.ArenaObject.transform as RectTransform;
            if (_arenaRectTransform == null)
            {
                throw new Exception("_arenaRectTransform is null");
            }
            _moveController = new MoveController(_arenaRectTransform);
            
            
            _arenaRectTransform.sizeDelta=new Vector2(props.WorldWidth,props.WorldHeight);
            
            var rand = new Random();
            for (int i = 0; i < props.Population; i++)
            {
                var personProps = new PersonProperties();
                if (rand.NextDouble() > props.PercentAtIsolation)
                {
                    personProps.Speed = (float) (props.PersonSpeed+rand.NextDouble()*props.PersonSpeed);
                }
                else
                {
                    personProps.Speed = 0;
                }

                personProps.PersonPrefab = props.PersonObject;
                var person = ArenaUtils.CreatePerson(personProps, _arenaRectTransform);
                _persons.Add(person);
            }
        }

        public IReadOnlyPopulationState State=>_state;

        public void InfectRandom(InfectionProps props)
        {
            var r = new Random();
            var movingPersons=_persons.Where(x => x.Props.Speed > 0 && !x.IsRecovered).ToList();
            var index = Mathf.FloorToInt((float) r.NextDouble() * movingPersons.Count);
            if (index >= 0 && index < movingPersons.Count - 1)
            {
                movingPersons[index].Infect(props);
            }
        }


        public void Update()
        {
            for (int i = 0; i < _persons.Count; i++)
            {
                _persons[i].Update();
            }

            _state.Population = _persons.Count;
            _state.TotalCases = _persons.Count(x => x.IsInfected);
            _state.TotalCasesPeopleWhoIsolated = _persons.Count(x => (x.IsInfected || x.IsRecovered) && Math.Abs(x.Props.Speed) <= float.Epsilon);
            
            _state.TotalRecovered = _persons.Count(x => x.IsRecovered);

            if (_state.TotalCases > 0)
            {
                _state.TotalDuration += Time.deltaTime;
            }

            _moveController.Update();
        }

        public void Dispose()
        {
            foreach (var person in _persons)
            {
                person.Dispose();
            }
            _persons.Clear();
            foreach (Transform child in _arenaRectTransform)
            {
                UnityEngine.Object.Destroy(child.gameObject);
            }
            
            _moveController.Dispose();
        }
    }
}