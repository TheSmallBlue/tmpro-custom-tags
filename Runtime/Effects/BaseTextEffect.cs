using TMPro;
using UnityEngine;

namespace Oneiromancer.TMP.Effects
{
    /// Base class for text effects that processes it character by character
    [System.Serializable]
    public abstract class BaseTextEffect : ScriptableObject
    {
        public string Tag;

        public void ProcessEffect(TMP_Text text, int startIdx, int endIdx, string parameter)
        {
            for (int i = startIdx; i <= endIdx; i++)
            {
                var charInfo = text.textInfo.characterInfo[i];
                if (charInfo.character == ' ') continue;
                ApplyToCharacter(text, charInfo, parameter);
            }
        }
        
        protected abstract void ApplyToCharacter(TMP_Text text, TMP_CharacterInfo charInfo, string parameter);
    }
}