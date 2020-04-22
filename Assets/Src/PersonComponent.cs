using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Src
{
    public class PersonComponent : MonoBehaviour
    {
        [SerializeField] public CircleCollider2D Collider2D;
        [SerializeField] public Image Image;
        public Person Person { get; set; }

        public readonly HashSet<PersonComponent> CollisionsWithOtherPersons = new HashSet<PersonComponent>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            var person = other.GetComponent<PersonComponent>();
            if (person != null && !CollisionsWithOtherPersons.Contains(person))
            {
                CollisionsWithOtherPersons.Add(person);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var person = other.GetComponent<PersonComponent>();
            if (person != null && CollisionsWithOtherPersons.Contains(person))
            {
                CollisionsWithOtherPersons.Remove(person);
            }
        }
    }
}