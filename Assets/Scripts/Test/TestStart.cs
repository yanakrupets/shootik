using DI;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class TestStart : MonoBehaviour
    {
        [SerializeField] private Button testButtonStart;
        [SerializeField] private Button testButtonFinish;
        
        [Inject] private EventManager _eventManager;
        
        private void Start()
        {
            testButtonStart.onClick.AddListener(_eventManager.PublishStartGame);
            testButtonFinish.onClick.AddListener(_eventManager.PublishFinishGame);
        }
    }
}
