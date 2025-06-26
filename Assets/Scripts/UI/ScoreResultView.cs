using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreResultView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        public void SetResultScore(int score)
        {
            scoreText.text = "Score : " + score;
        }
    }
}
