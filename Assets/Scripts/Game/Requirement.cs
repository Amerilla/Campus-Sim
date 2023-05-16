using TMPro;
using UnityEngine.SocialPlatforms.Impl;

namespace Game
{
    public class Requirement
    {
        private readonly Score _score;
        private readonly int _value;
        
        public bool HasRequirement() {
            return _score.GetValue() >= _value;
        }

    }
}