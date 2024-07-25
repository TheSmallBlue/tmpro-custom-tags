using System.Collections;
using System.Collections.Generic;
using Oneiromancer.TMP.Events;
using Oneiromancer.TMP.Typewriter;
using UnityEngine;

namespace Oneiromancer.TMP
{
    [CreateAssetMenu(menuName = "CustomTMPTags/Events/WaitForInput")]
    [System.Serializable]
    public class InputWaitTextEvent : BaseTextEvent
    {
        protected override IEnumerator TriggerEvent(TypewriterAnimation typewriter, string parameter)
        {
            while (!Input.GetMouseButtonDown(0))
            {
                yield return null;
            }

            yield return null;
        }
    }
}
