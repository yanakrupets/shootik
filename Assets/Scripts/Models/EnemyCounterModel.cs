using DI;
using UI;

namespace Models
{
    public class EnemyCounterModel
    {
        private readonly EnemyCounterView _counterView;
        private readonly ScoreResultView _resultScoreView;
    
        private int _enemyCount;

        public int CurrentCount => _enemyCount;

        [Inject]
        public EnemyCounterModel(EnemyCounterView counterView, ScoreResultView resultScoreView)
        {
            _counterView = counterView;
            _resultScoreView = resultScoreView;
        }

        public void ResetCounter()
        {
            _enemyCount = 0;
            _counterView.UpdateCount(_enemyCount);
            _resultScoreView.SetResultScore(_enemyCount);
        }

        public void RaiseCounter()
        {
            _enemyCount++;
            _counterView.UpdateCount(_enemyCount);
        }

        public void SetResultScore()
        {
            _resultScoreView.SetResultScore(_enemyCount);
        }
    }
}
