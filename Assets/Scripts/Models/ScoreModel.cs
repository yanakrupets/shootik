using System;
using DI;
using UI;

namespace Models
{
    public class ScoreModel
    {
        private readonly BestScoreView _view;
        private int _bestScore;

        public event Action<string, int> OnSave;
        
        [Inject]
        public ScoreModel(BestScoreView view)
        {
            _view = view;
        }

        // every time, because if player leave game while playing, but have better score, it should be saved
        public void UpdateScore(int score)
        {
            if (score > _bestScore || score == 0)
            {
                _bestScore = score;
                _view.UpdateBestScore(_bestScore);
                
                OnSave?.Invoke(SaveConstants.BestScore, _bestScore);
            }
        }
    }
}
