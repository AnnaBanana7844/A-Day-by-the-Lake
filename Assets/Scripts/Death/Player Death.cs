using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    public GameObject deathPanel;
    public GameObject backGround;

    public AudioSource audioSource;
    public AudioClip[] deathSounds;

    public void showDeathScreen()
    {
        deathSequence();
    }

    public void onMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

        public void deathSequence()
    {
        playRandomDeathSound();
        GameManager.instance.youLose();

     

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
