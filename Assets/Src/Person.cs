using Src.info;
using Src.state;
using Src.utils;
using UnityEngine;

namespace Src
{
    public class Person:IService
    {
        private readonly PersonState _state;
        private readonly Transform _transform;
        private readonly Rect _bounds;
        private readonly PersonComponent _component;
        public PersonProperties Props { get; }
        private InfectionProps _infectionProps;

        public Person(PersonComponent component, PersonProperties props, Rect bounds)
        {
            Props = props;
            _component = component;
            _component.Person = this;
            _bounds = bounds;
            _transform = component.transform;
            _state=new PersonState();
            updateView();
        }

        private void updateView()
        {
            if (_state.IsInfected)
            {
                _component.Image.color = Color.red;
            }
            else if (_state.HasImmunity)
            {
                _component.Image.color =Color.green;
            }
            else
            {
                _component.Image.color = Color.yellow;
            }
        }

        public void Update()
        {
            //find new target to move
            if (!_state.MovingToTargetPoint)
            {
                _state.MovingToTargetPoint = true;
                _state.TargetPoint = Utils.GetRandom2dPoint(_bounds);
            }

            //move
            if (_state.MovingToTargetPoint)
            {
                var delta = (_state.TargetPoint - _transform.localPosition);
                if (delta.magnitude > Props.Speed * Time.deltaTime)
                {
                    _transform.localPosition += Props.Speed * Time.deltaTime * delta.normalized;
                }
                else
                {
                    _state.MovingToTargetPoint = false;
                }
            }


            //infect other people
            if (_state.IsInfected)
            {
                foreach (var collisionPerson in _component.CollisionsWithOtherPersons)
                {
                    if (!collisionPerson.Person.IsInfected)
                    {
                        collisionPerson.Person.Infect(_infectionProps);
                    }
                }

                if (Time.time - _state.TimeWhenInfected > _infectionProps.Duration)
                {
                    SetImmunity();
                    Recover();
                }
            }
        }

        public bool IsInfected => _state.IsInfected;
        public bool IsRecovered => !IsInfected && _state.HasImmunity;

        public void Infect(InfectionProps infectionProps)
        {
            if (_state.IsInfected || _state.HasImmunity) return;
            _infectionProps = infectionProps;
            _state.TimeWhenInfected = Time.time;
            _state.IsInfected = true;
            updateView();
        }

        public void Recover()
        {
            if (_state.IsInfected)
            {
                _state.IsInfected = false;
                updateView();
            }
        }


        public void SetImmunity()
        {
            if (_state.HasImmunity) return;
            _state.HasImmunity = true;
            Recover();
            updateView();
        }

        public void Dispose()
        {
            
        }
    }
}