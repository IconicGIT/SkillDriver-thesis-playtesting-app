using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{

    GameManager gameManager;

    public int levelIndex;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gm = GameObject.Find("Game Manager");
        gameManager = gm.GetComponent<GameManager>();

    }

    public void ChangeToScene()
    {
        gameManager.SetCurrentLevel(levelIndex);
        gameManager.currentScene = "Map";
        gameManager.ChangeScene();
    }

}
