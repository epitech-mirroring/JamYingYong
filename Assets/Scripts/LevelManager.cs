using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelManager : MonoBehaviour
{
    public GameObject[] buttons;
    public int maxLevel = 1;
    private int _oldMaxLevel = 1;

    void Start()
    {
        DisableLockedLevels();  
    }

    void Update()
    {
        if (maxLevel != _oldMaxLevel)
        {
            _oldMaxLevel = maxLevel;
            DisableLockedLevels();
        }
    }

    void DisableLockedLevels()
    {
        for (int i = 0; i < buttons.Length; i++) {
            if (i + 1 > maxLevel)
                buttons[i].GetComponent<Button>().interactable = false;
            else
                buttons[i].GetComponent<Button>().interactable = true;
        }
    }

    public void LoadLevel(int level)
    {
        if (File.Exists("Assets/Scenes/levels/level" + level + ".unity"))
            SceneManager.LoadScene("Assets/Scenes/levels/level" + level + ".unity");
    }
}
