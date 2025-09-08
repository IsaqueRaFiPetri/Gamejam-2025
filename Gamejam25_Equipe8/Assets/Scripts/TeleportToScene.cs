using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToScene : MonoBehaviour
{
    public void Tp(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    private void Quitgame()
    {
        Application.Quit();
    }
}