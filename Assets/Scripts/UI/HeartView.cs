using UnityEngine;

namespace UI
{
    public class HeartView : MonoBehaviour
    {
        [SerializeField] private GameObject heartPrefab;
        
        private GameObject[] _hearts;

        public void ResetHearts(int count)
        {
            if (_hearts != null)
            {
                foreach (var heart in _hearts)
                {
                    Destroy(heart);
                }
            }
            
            _hearts = new GameObject[count];
            for (var i = 0; i < count; i++)
            {
                _hearts[i] = Instantiate(heartPrefab, transform);
            }
        }

        public void ChangeHeart(int index, bool isActive)
        {
            _hearts[index].SetActive(isActive);
        }
    }
}
