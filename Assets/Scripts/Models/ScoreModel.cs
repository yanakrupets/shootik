using DI;
using Interfaces;
using UI;

namespace Models
{
    public class ScoreModel : IInitializable
    {
        private readonly BestScoreView _view;
        private int _bestScore;
        
        [Inject]
        public ScoreModel(BestScoreView view)
        {
            _view = view;
        }
        
        public void Initialize()
        {
            // take best score from player settings
            _bestScore = 0;
            _view.UpdateBestScore(_bestScore);
        }

        // every time, because if player leave game while playing, but have better score, it should be saved
        public void UpdateScore(int score)
        {
            if (score > _bestScore)
            {
                _bestScore = score;
                // save to player settings
                _view.UpdateBestScore(_bestScore);
            }
        }
    }
}
