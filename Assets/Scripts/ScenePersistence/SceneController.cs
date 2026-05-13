using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public string SceneToLoad;

    public void changeSceneNow()
    {
        SceneManager.LoadScene(SceneToLoad);
    }

}
