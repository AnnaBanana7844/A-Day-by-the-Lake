using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public string SceneToLoad;
    public bool isPlaying;

    public void changeSceneNow()
    {
        SceneManager.LoadScene(SceneToLoad);
    }


    public void EndGame()
    {
        Debug.Log("Game is Closing");

        #if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

        #else

            Application.Quit(); 

        #endif

        
        
    }
}
