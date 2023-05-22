using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class SwitchingPlayers : MonoBehaviour
{
    public PlayerController player;
    public List<GameObject> cubesList;

    void Update()
    {
        foreach (var cube in cubesList)
        {
            cube.SetActive(player.isWhite);
        }
    }
}
