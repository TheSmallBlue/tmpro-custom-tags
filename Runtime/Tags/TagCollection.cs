using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Oneiromancer.TMP.Effects;
using Oneiromancer.TMP.Events;
using UnityEngine;

namespace Oneiromancer.TMP.Tags
{
    [CreateAssetMenu(menuName = "Text Tags/TagCollection")]
    [System.Serializable]
    public class TagCollection : ScriptableObject
    {
        public BaseTextEffect[] effects;
        public BaseTextEvent[] events;

        public List<string> AllTags => effects.Select( x => x.Tag).Concat(events.Select( x => x.Tag)).ToList();
    }
}
