using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // method loads the scene passed in as a string (referring to its title)
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }
}
