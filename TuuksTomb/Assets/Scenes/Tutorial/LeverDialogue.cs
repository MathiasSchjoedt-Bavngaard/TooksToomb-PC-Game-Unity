using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LeverDialogue : DialogueOptions
{
    public GameObject player;
    public Tilemap tilemap;
    public SceneReference startLevel;
    public GameObject lever;
    public Vector3 playerPos;

    // Time it takes in seconds to shrink from starting scale to target scale.
    private float ShrinkDuration = 1.5f;

    // The target scale
    private Vector3 TargetScale = Vector3.one * .25f;

    // The starting scale
    private Vector3 startScale;

    // T is our interpolant for our linear interpolation.
    private float t = 0;


    protected override void OnAccept()
    {
        Debug.Log("Accept");
        var characterControl = player.GetComponent<CharacterController2D>();
        var playerMovement = player.GetComponent<PlayerMovement>();

        characterControl.enabled = false;
        playerMovement.enabled = false;

        Invoke(nameof(RemoveTile), 0.5f);
    }
    private void RemoveTile()
    {
        var leverScale = lever.transform.localScale;
        lever.transform.localScale = new Vector3(-leverScale.x, leverScale.y, leverScale.z);

        var tileTop = new Vector3Int(4, 0, 0);
        var tileMiddle = new Vector3Int(4, -1, 0);
        var tileBottom = new Vector3Int(4, -2, 0);
        tilemap.SetTile(tileTop, null);
        tilemap.SetTile(tileMiddle, null);
        tilemap.SetTile(tileBottom, null);
        Invoke(nameof(FallAnimation), 0.5f);
    }
    private void FallAnimation()
    {
        t = 0;
        startScale = player.transform.localScale;
        Shrink();
    }
    protected override void OnDecline()
    {
        Debug.Log("Decline");
    }

    private void Shrink()
    {
        // Divide deltaTime by the duration to stretch out the time it takes for t to go from 0 to 1.
        t += Time.deltaTime / ShrinkDuration;

        // Lerp wants the third parameter to go from 0 to 1 over time. 't' will do that for us.
        Vector3 newScale = Vector3.Lerp(startScale, TargetScale, t);
        player.transform.localScale = newScale;

        if (t > 1)
        {
            PlayerPrefs.SetFloat("x", playerPos.x);
            PlayerPrefs.SetFloat("z", playerPos.z);
            PlayerPrefs.SetFloat("y", playerPos.y);

            SceneManager.LoadScene(startLevel.Name);
            PlayerPrefs.SetString("continueLevel", startLevel.Name);
            return;
        }
        Invoke(nameof(Shrink), 1/60);
    }

}
