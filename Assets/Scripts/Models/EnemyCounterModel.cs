using DI;
using UI;

namespace Models
{
    public class EnemyCounterModel
    {
        private readonly EnemyCounterView _view;
    
        private int _enemyCount;

        [Inject]
        public EnemyCounterModel(EnemyCounterView view)
        {
            _view = view;
        }

        public void ResetCounter()
        {
            _enemyCount = 0;
            _view.ResetView(_enemyCount);
        }

        public void RaiseCounter()
        {
            _enemyCount++;
            _view.UpdateCount(_enemyCount);
        }
    }
}
