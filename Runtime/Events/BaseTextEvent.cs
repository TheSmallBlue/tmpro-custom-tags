using System.Collections;
using System.Collections.Generic;
using Oneiromancer.TMP.Typewriter;
using UnityEngine;

namespace Oneiromancer.TMP.Events
{
    [System.Serializable]
    public abstract class BaseTextEvent : ScriptableObject
    {
        public string Tag;


        public System.Func<int, IEnumerator> ProcessEvent(TypewriterAnimation typewriter, int triggerIndex, string parameter)
        {
            return (i) => i == triggerIndex ? TriggerEvent(typewriter, parameter) : null;
        }

        protected abstract IEnumerator TriggerEvent(TypewriterAnimation typewriter, string parameter);
    }
}
