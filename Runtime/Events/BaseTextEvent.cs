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

        TypewriterAnimation _target;
        int _trigger;
        string _parameter;

        public void ProcessEvent(TypewriterAnimation typewriter, int triggerIndex, string parameter, ref System.Func<int, IEnumerator> tickEvent)
        {
            _target = typewriter;
            _trigger = triggerIndex;
            _parameter = parameter;

            tickEvent += TriggerCheck;
        }

        private IEnumerator TriggerCheck(int index)
        {
            if (index == _trigger)
            {
                yield return TriggerEvent(_target, _parameter);
            }
            else 
            {
                yield return null;
            }
        }

        protected abstract IEnumerator TriggerEvent(TypewriterAnimation typewriter, string parameter);
    }
}
