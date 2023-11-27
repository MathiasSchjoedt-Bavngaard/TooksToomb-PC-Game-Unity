using UnityEngine;

public abstract class DialogueOptions : MonoBehaviour
{
    public GameObject acceptButton;
    public GameObject declineButton;
    public virtual void DialogueEnd()
    {
        Invoke(nameof(SetButtonsActive), 0.25f);
    }
    public void Accept()
    {
        SetButtonsInActive();
        OnAccept();
    }
    public void Decline()
    {
        SetButtonsInActive();
        OnDecline();
    }
    protected abstract void OnAccept();
    protected abstract void OnDecline();
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
}
