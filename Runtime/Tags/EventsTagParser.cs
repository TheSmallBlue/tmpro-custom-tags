using System.Collections;
using System.Collections.Generic;
using Oneiromancer.TMP.Events;
using TMPro;
using UnityEngine;
using System.Linq;
using Oneiromancer.TMP.Typewriter;

namespace Oneiromancer.TMP.Tags
{
    [RequireComponent(typeof(TypewriterAnimation))]
    public class EventsTagParser : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private TypewriterAnimation _typewriter;
        [SerializeField] private TagCollection _tags;

        private BaseTextEvent[] _tagEvents => _tags.events;

        private CustomTagPreprocessor _currentPreprocessor;

        private System.Func<int, IEnumerator> _tickEvent;

        private void Awake() 
        {
            _typewriter.TextChangedEvent += OnTextChanged;
            _typewriter.TickEvent += OnTick;

            SetParser();
        }

        private void OnTextChanged(string newText)
        {
            _tickEvent = null;

            foreach (var tagInfo in _currentPreprocessor.TagInfos)
            {
                foreach (var processor in _tagEvents)
                {
                    if(!tagInfo.IsTagEqual(processor.Tag)) continue;
                    processor.ProcessEvent(_typewriter, tagInfo.StartIndex, tagInfo.Parameter, ref _tickEvent);
                }
            }
        }

        private IEnumerator OnTick(int index)
        {
            yield return _tickEvent?.Invoke(index);
        }

        private void OnValidate() 
        {
            _text ??= GetComponent<TMP_Text>();
            _typewriter ??= GetComponent<TypewriterAnimation>();
        }

        private void SetParser()
        {
            var possibleTags = _tags.AllTags;
            
            if(_text.textPreprocessor is CustomTagPreprocessor)
            {
                _currentPreprocessor = _text.textPreprocessor as CustomTagPreprocessor;
            } else 
            {
                _currentPreprocessor = new CustomTagPreprocessor(possibleTags);
                _text.textPreprocessor = _currentPreprocessor;
            }
            
            _text.ForceMeshUpdate();
        }
    }
}
