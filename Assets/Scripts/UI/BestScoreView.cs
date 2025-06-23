using TMPro;
using UnityEngine;

namespace UI
{
    public class BestScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI bestScoreText;
        
        public void UpdateBestScore(int count)
        {
            bestScoreText.text = "Best score: " + count;
        }
    }
}