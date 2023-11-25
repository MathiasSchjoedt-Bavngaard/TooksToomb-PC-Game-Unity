using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueOptions : MonoBehaviour
{
    public GameObject acceptButton;
    public GameObject declineButton;
    public virtual void DialogueEnd()
    {
        Invoke(nameof(SetButtonsActive), 0.25f);
    }
    private void SetButtonsActive()
    {
        acceptButton.SetActive(true);
        declineButton.SetActive(true);
    }
    private void SetButtonsInActive()
    {
        acceptButton.SetActive(false);
        declineButton.SetActive(false);
    }
    public virtual void OnAccept()
    {
        SetButtonsInActive();
    }
    public virtual void OnDecline()
    {
        SetButtonsInActive();
    }
}
