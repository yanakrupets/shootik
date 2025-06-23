using TMPro;
using UnityEngine;

namespace UI
{
    public class EnemyCounterView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI enemyCounterText;
        
        public void UpdateCount(int count)
        {
            enemyCounterText.text = count.ToString();
        }
    }
}
