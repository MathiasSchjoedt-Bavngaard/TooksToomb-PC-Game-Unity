using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TutorialSideController : TutorialControllerBase
{
    public GameObject sidePlayer;
    public GameObject toggleSceneManager;
    public GameObject pauseMenu;

    protected override void Start()
    {
        var characterControl = sidePlayer.GetComponent<CharacterController2D>();
        var playerMovement = sidePlayer.GetComponent<PlayerMovement>();

        var pauseMenuScript = pauseMenu.GetComponent<PauseMenu>();
        var toggleSceneManagerScript = toggleSceneManager.GetComponent<ToggleScene>();

        var movement = new RequiredControl(new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D}," to move!", characterControl, playerMovement);
        var shift = new RequiredControl(new KeyCode[] { KeyCode.LeftShift, KeyCode.RightShift }, " to sprint!");
        var pauseMenuControl = new RequiredControl(new KeyCode[] {KeyCode.Escape }, " to toggle the pause menu!", pauseMenuScript);
        var toggleView = new RequiredControl(new KeyCode[] { KeyCode.Z}, " to toggle the character perspective between side-view and topdown!", toggleSceneManagerScript);

        _requiredControls = new List<RequiredControl>() { movement, shift, pauseMenuControl, toggleView};
        base.Start();
    }

}