using System.Collections;
using TMPro;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public NPCInteraction npcInteraction;
    
    public GameObject dialoguePanel;
    public GameObject shopPanel;
    public GameObject dialogueOptionsPanel;
    public GameObject textBoxPanel;
    public TMP_Text dialogueText;

    public AudioSource audioSource;
    public AudioClip[] typingSounds;

    private bool isTyping = false;
    private string fullText;
    private Coroutine typingCoroutine;


    void Update()
    {
        if(!textBoxPanel.activeSelf)
        {
            return;
        }
        if(textBoxPanel.activeSelf && Input.GetKeyUp(KeyCode.Space))
        {
            onSkip();
        }
    }
    public void openDialogue()
    {
        dialoguePanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        cameraController.uiOpen = true; 
        Player.uiOpen = true;
    }

    public void closeAll()
    {
        dialoguePanel.SetActive(false);
        shopPanel.SetActive(false);
        dialogueOptionsPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraController.uiOpen = false;
        Player.uiOpen = false;

        npcInteraction.reactivatePrompt();
    }

    public void onTalkButton()
    {
        dialoguePanel.SetActive(false);
        dialogueOptionsPanel.SetActive(true);
    }

    public void onShopButton()
    {
        dialoguePanel.SetActive(false);
        shopPanel.SetActive(true);
    }

    public void onBack()
    {
        dialoguePanel.SetActive(true);
        dialogueOptionsPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    public void onLeaveButton()
    {
        closeAll();
    }

    public void onDialogue1()
    {
        showDialogue("I am nothing but a capsule in this empty world. My name is NPC and one day my creator will make me look beautiful!");
    }
    public void onDialogue2()
    {
        showDialogue("Well, right now theres not much to do around here...      but if you want to try out our fishing minigame go stand on that platform behind you. I would try it but I dont have hands, or a face, or anything.");
    }

    public void onDialogue3()
    {
        showDialogue("Oh...      that cube? Its a way to die. I've heard rumors that whoever touches it suffers a gruesome death.");
    }
    public void onDialogue4()
    {
        showDialogue("The brown thing over? Thats the bear right now. If you dont want to die by walking into a cube, you can go get mauled by the bear instead!");
    }

    public void showDialogue(string text)
    {
        dialogueOptionsPanel.SetActive(false);
        textBoxPanel.SetActive(true);

        fullText = text;

        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText(text));
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach(char c in text)
        {
            dialogueText.text += c;

            if(char.IsLetterOrDigit(c))
            {
                playTypingSound();
            }

            yield return new WaitForSeconds(0.05f);

            if(!isTyping)
            {
                dialogueText.text = fullText;
                yield break;
            }
        }

        isTyping = false;
    }
    public void onSkip()
    {
        if(isTyping)
        {
            isTyping = false;
            dialogueText.text = fullText;
        }
        else
        {
            textBoxPanel.SetActive(false);
            dialogueOptionsPanel.SetActive(true);
        }
    }

    public void playTypingSound()
    {
        if (typingSounds.Length == 0) return;

        int index = Random.Range(0,typingSounds.Length);
        audioSource.PlayOneShot(typingSounds[index], 1f);
    }
}