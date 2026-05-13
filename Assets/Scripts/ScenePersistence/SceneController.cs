using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public string SceneToLoad;
    public bool isPLaying;

    public void changeSceneNow()
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    public void EndGame()
    {
        Debug.Log("Exiting Game!!!!!");

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;

        #else
            Application.Quit();

        #endif
    }


}
