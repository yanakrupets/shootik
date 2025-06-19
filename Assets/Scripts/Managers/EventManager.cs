namespace Managers
{
    public class EventManager
    {
        public delegate void ItemShotEvent(ShootableItem item);
        public event ItemShotEvent OnItemShot;

        public delegate void StartEvent();
        public event StartEvent OnStart;
        
        public delegate void FinishEvent();
        public event FinishEvent OnFinish;

        public void PublishItemShot(ShootableItem item)
        {
            OnItemShot?.Invoke(item);
        }

        public void PublishStartGame()
        {
            OnStart?.Invoke();
        }
        
        public void PublishFinishGame()
        {
            OnFinish?.Invoke();
        }
    }
}
