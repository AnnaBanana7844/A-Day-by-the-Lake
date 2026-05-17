using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public GameObject deathPanel;
    public DeathFade fader;

    public AudioSource audioSource;
    public AudioClip[] deathSounds;

    public void showDeathScreen()
    {
        StartCoroutine(DeathSequence());
    }

    public void onRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerController.uiOpen = false;
    }

    public void onMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

        IEnumerator DeathSequence()
    {
        playRandomDeathSound();

        yield return StartCoroutine(fader.FadeToBlack(1.5f));

        deathPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        PlayerController.uiOpen = true;
    }

    public void playRandomDeathSound()
    {
        int index = Random.Range(0, deathSounds.Length);
        audioSource.PlayOneShot(deathSounds[index]);
    }

}
