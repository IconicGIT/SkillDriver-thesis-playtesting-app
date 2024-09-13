using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MaxScoreTitle : MonoBehaviour
{
    GameManager gameManager;
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gm = GameObject.Find("Game Manager");
        gameManager = gm.GetComponent<GameManager>();

        text = GetComponent<TextMeshProUGUI>();

        text.text = gameManager.GetTotalScore().ToString();
    }
}
