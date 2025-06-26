using DI;
using UI;

namespace Models
{
    public class HeartModel
    {
        private const int MaxCount = 3;
    
        private readonly HeartView _view;
    
        private int _currentIndex;

        [Inject]
        public HeartModel(HeartView view)
        {
            _view = view;
        }
    
        public void ResetHearts()
        {
            _currentIndex = MaxCount - 1;
            _view.ResetHearts(MaxCount);
        }
    
        public void AddHeart()
        {
            if (_currentIndex >= MaxCount - 1)
                return;
            
            _currentIndex++;
            _view.ChangeHeart(_currentIndex, true);
        }
    
        public bool TryRemoveHeart()
        {
            _view.ChangeHeart(_currentIndex, false);
            _currentIndex--;
        
            return _currentIndex != -1;
        }
    }
}
