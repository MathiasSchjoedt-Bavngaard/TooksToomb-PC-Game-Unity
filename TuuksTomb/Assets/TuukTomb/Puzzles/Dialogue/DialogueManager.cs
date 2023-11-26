using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textWait;

    private int _index;
    public bool isDialogInProgress;
    
    public Animator animator;
    private GameObject _player;

    [CanBeNull] public DialogueOptions dialogueOptions;

    public void StartText()
    {
        StartCoroutine(TypeLines());
    }
    private void FadeIn()
    {
        animator.SetTrigger("FadeInBlack");
    }

    private void FadeOut()
    {
        animator.SetTrigger("FadeOutBlack");
    }

    private void Start()
    {
        textComponent.text = string.Empty;
    }

    private void Update()
    {
        if (isDialogInProgress && Input.GetKeyDown(KeyCode.Space))
        {
            if (textComponent.text == lines[_index])
            {
                NextLine();
            }
        }
    }

    public void StartDialogue(GameObject collidedObj)
    {
        _player = collidedObj;
        _index = 0;
        textComponent.text = string.Empty;
        FadeOut();
    }

    private IEnumerator TypeLines()
    {
        yield return new WaitForSeconds(textWait);
        foreach (var c in lines[_index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textWait);
        }
        if (_index+1>=lines.Length)
        {
            dialogueOptions?.DialogueEnd();
        }
    }

    private void NextLine()
    {
        if (_index < lines.Length - 1)
        {
            _index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLines());
        }
        else if(dialogueOptions!=null)
        {
            dialogueOptions.DialogueEnd();
        }
        else
        {
            EndDialogue();
        }
    }

    public void OnAccept()
    {
        EndDialogue();
        dialogueOptions.Accept();
    }

    public  void OnDecline()
    {
        EndDialogue();
        dialogueOptions.Decline();
    }

    public void EndDialogue()
    {
        if (_player != null)
        {
            _player.GetComponent<PlayerMovement>().enabled = true;
            _player.transform.GetChild(2).gameObject.GetComponent<Animator>().enabled = true;
        }
        FadeIn();
        isDialogInProgress = false;
        gameObject.SetActive(false);
    }
    
}