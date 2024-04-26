using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class SceneController
    {
        public void ReloadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}