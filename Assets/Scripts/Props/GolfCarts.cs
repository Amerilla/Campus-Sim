using UnityEngine;

namespace Props
{
    public class GolfCarts : MonoBehaviour
    {
        private int _currentLevel;
        
        private void Start() {
            _currentLevel = 0;
            
            for (int i = 0; i < transform.childCount; i++) {
                Transform child = transform.GetChild(i);
                child.gameObject.SetActive(false);
            }
        }

        private void Update() {
            
        }

        public void Upgrade() {
            ++_currentLevel;
            if (_currentLevel <= 3) {
                string groupName = "Group" + _currentLevel;
                Transform group = transform.Find(groupName);

                if (group != null) {
                    group.gameObject.SetActive(true);
                } else {
                    Debug.LogWarning("Group not found: " + groupName);
                }
            }
        }
    }
}