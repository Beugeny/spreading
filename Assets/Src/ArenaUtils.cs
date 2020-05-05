using Src.info;
using UnityEngine;

namespace Src
{
    public static class ArenaUtils
    {
        public static Person CreatePerson(PersonProperties props, RectTransform parent)
        {
            var parentRect = parent.rect;
            var personObject = Object.Instantiate(props.PersonPrefab, parent);
            // personObject.transform.localPosition = Utils.GetRandom2dPoint(parentRect);
            var p = new Person(personObject.GetComponent<PersonComponent>(),props,parentRect);
            return p;
        }
    }
}