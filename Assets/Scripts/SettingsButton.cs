using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject playButton;
    public GameObject quitButton;
    public GameObject optionButton;
    public SoundManagement soundManagement;

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        playButton.SetActive(false);
        quitButton.SetActive(false);
        optionButton.SetActive(false);
        soundManagement.PlayUIClick();
    }

    public void QuitSettings()
    {
        settingsPanel.SetActive(false);
        playButton.SetActive(true);
        quitButton.SetActive(true);
        optionButton.SetActive(true);
        soundManagement.PlayUIClick();
    }
}
