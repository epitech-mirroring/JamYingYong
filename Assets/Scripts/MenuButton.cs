using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public SoundManagement soundManagement;

    public void QuitGame()
    {
        Application.Quit();
        if (UnityEditor.EditorApplication.isPlaying) { //Delete this condition to allow build
            UnityEditor.EditorApplication.isPlaying = false;
        }
        soundManagement.PlayUIClick();

    }

    public void PlayGame()
    {
        soundManagement.PlayUIClick();
        SceneManager.LoadScene("Assets/Scenes/LevelSelection.unity");
    }
}
