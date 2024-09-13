using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BuildLevelButtons : MonoBehaviour
{
    
    GameManager gameManager;

    [SerializeField] GameObject buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {

        GameObject gm = GameObject.Find("Game Manager");
        gameManager = gm.GetComponent<GameManager>();

        int index = 0;
        foreach (GameManager.Level l in gameManager.levels)
        {

            GameObject lvlButton = Instantiate(buttonPrefab,gameObject.transform);

            lvlButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = l.name;
            lvlButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = l.score.ToString();

            LevelButton lb = lvlButton.GetComponent<LevelButton>();
            lb.levelIndex = index;
            gameManager.levels[index].levelIndex = index;
            //lvlButton.transform.localScale = new Vector2(1, 1);

            //lvlButton.transform.parent = gameObject.transform;
            index++;
        }



    }
}
