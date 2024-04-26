using Gameplay;
using Zenject;

namespace UI.Windows
{
    public class LoseWindow : Window
    {
        [Inject] private SceneController _sceneController;

        public void Restart()
        {
            _sceneController.ReloadCurrentScene();
        }
    }
}