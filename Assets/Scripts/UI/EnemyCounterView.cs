using TMPro;
using UnityEngine;

namespace UI
{
    public class EnemyCounterView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI enemyCounter;

        public void ResetView(int startCount)
        {
            enemyCounter.text = startCount.ToString();
        }
        
        public void UpdateCount(int count)
        {
            enemyCounter.text = count.ToString();
        }
    }
}
