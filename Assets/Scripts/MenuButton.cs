using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public SoundManagement soundManagement;

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
        if (UnityEditor.EditorApplication.isPlaying) {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        soundManagement.PlayUIClick();

    }

    public void PlayGame()
    {
        Debug.Log("Play Game");
        soundManagement.PlayUIClick();
    }
}
