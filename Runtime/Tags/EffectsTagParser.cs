using System.Linq;
using Oneiromancer.TMP.Effects;
using TMPro;
using UnityEngine;

namespace Oneiromancer.TMP.Tags
{
    /// Component that can process custom tags in TMP_Text, given SO settings for each tag
    [ExecuteAlways]
    public class EffectsTagParser : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private TagCollection _tags;
        private BaseTextEffect[] _tagEffects => _tags.effects;

        private CustomTagPreprocessor _currentPreprocessor;
        private bool _inPreviewMode;

        private void Awake()
        {
            SetParser();
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying && !_inPreviewMode) return;
#endif
            UpdateTextMesh();
        }

        private void OnValidate()
        {
            _text ??= GetComponent<TMP_Text>();
        }

        public void SetTargetText(TMP_Text text)
        {
            if (text == null) throw new System.ArgumentNullException(nameof(text), "Text shouldn't be null");
            _text = text;
        }

        private void UpdateTextMesh()
        {
            _text.ForceMeshUpdate();
            foreach (var tagInfo in _currentPreprocessor.TagInfos)
            {
                foreach (var processor in _tagEffects)
                {
                    if (!tagInfo.IsTagEqual(processor.Tag)) continue;
                    processor.ProcessEffect(_text, tagInfo.StartIndex, tagInfo.LastIndex, tagInfo.Parameter);
                }
            }
            _text.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
        }

        private void SetParser()
        {
            var possibleTags = _tags.AllTags;

            if (_text.textPreprocessor is CustomTagPreprocessor)
            {
                _currentPreprocessor = _text.textPreprocessor as CustomTagPreprocessor;
            }
            else
            {
                _currentPreprocessor = new CustomTagPreprocessor(possibleTags);
                _text.textPreprocessor = _currentPreprocessor;
            }
            
            _text.ForceMeshUpdate();
        }

#if UNITY_EDITOR
        [ContextMenu("Start Preview")]
        public void Preview()
        {
            if (Application.isPlaying) return;
            SetParser();
            _inPreviewMode = true;
        }

        [ContextMenu("Stop Preview")]
        public void ResetParser()
        {
            if (Application.isPlaying) return;
            _currentPreprocessor = null;
            _text.textPreprocessor = null;
            _text.ForceMeshUpdate();
            _inPreviewMode = false;
        }
#endif
    }
}