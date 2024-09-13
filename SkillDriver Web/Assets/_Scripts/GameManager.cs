using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject levelSelector;


    [Serializable]
    public struct Level
    {
        public string name;
        public int levelIndex;
        public int score;
        public GameObject MapPrefab;
        
    }

    public string currentScene;
    public Level[] levels;

    public Level currentLevel;


    // Start is called before the first frame update
    void Start()
    {

        DontDestroyOnLoad(gameObject);


        startPanel.SetActive(true);
        levelSelector.SetActive(false);
    }

    public int GetTotalScore()
    {
        int ret = 0;
        foreach (GameManager.Level l in levels)
        {
            ret += l.score;
        }

        return ret;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void SetCurrentLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex > levels.Length) 
            Debug.Log("Level Index is out of bounds");
        else
            currentLevel = levels[levelIndex];
    }
}
