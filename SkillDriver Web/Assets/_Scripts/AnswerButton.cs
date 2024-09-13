using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    public QuestionManager questionManager;
    public int answerID;
    public string text;

    private void Start()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
    }
    public void EvaluateAnswer()
    {
        questionManager.EvaluateAnswer(answerID);
    }
}
