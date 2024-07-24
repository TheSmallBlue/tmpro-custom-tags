using System.Collections;
using System.Collections.Generic;
using Oneiromancer.TMP.Events;
using Oneiromancer.TMP.Typewriter;
using UnityEngine;

namespace Oneiromancer.TMP.Events
{
    [CreateAssetMenu(menuName = "CustomTMPTags/Events/Wait")]
    [System.Serializable]
    public class WaitTextEvent : BaseTextEvent
    {
        protected override IEnumerator TriggerEvent(TypewriterAnimation typewriter, string parameter)
        {
            float waitTime = float.Parse(parameter);

            yield return new WaitForSeconds(waitTime);
        }
    }
}
