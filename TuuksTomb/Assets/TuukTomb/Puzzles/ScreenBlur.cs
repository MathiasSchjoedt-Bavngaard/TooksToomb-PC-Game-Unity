using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBlur : MonoBehaviour
{
    public DialogueManager manager;
    
    public void OnFadeOutComplete()
    {
        if (manager.gameObject.activeSelf)
        {
            manager.StartText();
        }
    }
}
