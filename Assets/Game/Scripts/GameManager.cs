using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts
{
    public class GameManager : MonoBehaviour
    {   
        public static GameManager Instance;

        [SerializeField] private string[] _stages;

        private int _currentStage;
    
        public string CurrentStageName => _stages[_currentStage];

        private void Awake()
        {
            if (Instance == null)
                Instance = GetComponent<GameManager>();

            DontDestroyOnLoad(gameObject);
        }

        private static void ResetStage()
        {
            var activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.name);
        }

        public void NextStage()
        {
            _currentStage++;
            ResetStage();
        }
    }
}
