using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] bool debugMode;
    public GameObject answerButton;
    public MapManager mapManager;
    public GameObject eventQuestion;
    public TextMeshProUGUI statement;
    public GameObject answersPanel;
    public GameObject resultPanel;
    public GameObject failPanel;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI scoreText;
    public Image contextImage;
    [TextArea(2, 10)] public string failEndText;

    [SerializeField] GameObject fail1;
    [SerializeField] GameObject fail2;
    [SerializeField] GameObject fail3;

    public enum QuestionTheme
    {
        NONE, INITIATION, DOCUMENTATION, THE_DRIVER, OBLIGATIONS, VEHICLE_ELEMENTS, TRAFFIC_NORMS, MANEUVERS, TRANSPORTATION, PREVENTIVE_DRIVING, VEHICLE_MAINTENANCE
    }

    [Serializable]
    public struct Question
    {
        public QuestionTheme theme;
        public int questionId;
        [Range(0, 1.0f)] public float placeInRoad;
        public Sprite contextImage;
        [TextArea(2, 10)] public string statement;
        [TextArea(2, 10)] public string failExplanation;
        [TextArea(2, 10)] public string successExplanation;
        [Range(0,2)]public int correctAnswer;
        public float failSeverity;
        [TextArea(2, 10)] public string[] answers;
        public bool completed;
        public bool failed;
    }

    public GameObject[] questions;
    public Question currentQuestion;
    protected int failCount = 0;


    virtual public void Start()
    {
        questions = GameObject.FindGameObjectsWithTag("Question");
        if (!debugMode)
            foreach (var item in questions)
        {
            item.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    public void ShowQuestion(ref QuestionCollider quCol)
    {

        foreach (GameObject qgo in questions)
        {
            Question question = qgo.GetComponent<QuestionCollider>().question;

            if (!question.completed && question.questionId == quCol.question.questionId)
            {
               BuildQuestionPanel(question);
            }
        }
    }

    void BuildQuestionPanel(Question question)
    {
        currentQuestion = question;
        contextImage.sprite = question.contextImage;
        contextImage.preserveAspect = true;
        mapManager.tripPaused = true;

        eventQuestion.SetActive(true);
        statement.text = question.statement;
        CreateAnswers(question);
    }

    void CreateAnswers(Question question)
    {
        for (int i = 0; i < answersPanel.transform.childCount; i++)
        {
            Destroy(answersPanel.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < question.answers.Length; i++)
        {
            GameObject ans = Instantiate(answerButton, answersPanel.transform);
            ans.GetComponent<AnswerButton>().questionManager = this;
            ans.GetComponent<AnswerButton>().answerID = i;
            ans.GetComponent<AnswerButton>().text = question.answers[i];

            if (debugMode && i == question.correctAnswer)
            {
                ans.GetComponent<Button>().image.color = new Color(0, 1, 0);
            }

        }

        RandomizeAnswers(answersPanel.transform);
    }

    public void RandomizeAnswers(Transform parent)
    {
        // Get the list of children
        int childCount = parent.childCount;
        Transform[] children = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            children[i] = parent.GetChild(i);
        }

        // Shuffle the array of children
        for (int i = 0; i < childCount; i++)
        {
            Transform temp = children[i];
            int randomIndex = UnityEngine.Random.Range(0, childCount);
            children[i] = children[randomIndex];
            children[randomIndex] = temp;
        }

        // Reassign children in the new order
        for (int i = 0; i < childCount; i++)
        {
            children[i].SetSiblingIndex(i);
        }
    }


public int GetQuestionIndex(Question question)
    {
        for (int i = 0; i < questions.Length; i++)
        {
            if (question.questionId == questions[i].GetComponent<QuestionCollider>().question.questionId)
                return i;
        }
        return -1;
    }


    public void EvaluateAnswer(int answerID)
    {
        eventQuestion.SetActive(false);

        if (answerID == currentQuestion.correctAnswer)
        {
            resultText.text = currentQuestion.successExplanation;
            currentQuestion.failed = false;
        }
        else
        {
            currentQuestion.failed = true;
            failCount++;

            if (failCount == 1) fail1.SetActive(true);
            if (failCount == 2) fail2.SetActive(true);
            if (failCount == 3) fail3.SetActive(true);

            if (failCount < 3)
                resultText.text = currentQuestion.failExplanation;
            else
                resultText.text = failEndText;

        }
        resultPanel.SetActive(true);



    }

    public virtual void EndQuestion()
    {
        currentQuestion.completed = true;
        questions[GetQuestionIndex(currentQuestion)].GetComponent<QuestionCollider>().question = currentQuestion;
        eventQuestion.SetActive(false);
        if (failCount < 3)
        {
            mapManager.tripPaused = false;
            resultPanel.SetActive(false);
        }
        else
        {
            failPanel.SetActive(true);
            
        }

        int sc;
        GetResults(out int s, out int f, out sc);
        scoreText.text = sc.ToString();
    }

    public void ReturnToLevelSelector()
    {
        GameManager.Instance.currentScene = "LevelSelector";
        GameManager.Instance.ChangeScene();
    }

    public virtual void GetResults(out int succeeds, out int fails, out int score)
    {
        int s = 0;
        int f = 0;
        int sc = 0;
        foreach (GameObject qgo in questions)
        {
            Question q = qgo.GetComponent<QuestionCollider>().question;
            if (q.completed)
                if (q.failed)
                {
                    f++;
                    sc -= Mathf.FloorToInt(100 * (q.failSeverity + 1));
                }
                else
                {
                    s++;
                    sc += 100;
                }
            if (sc < 0) sc = 0;
        }

        

        succeeds = s;
        fails = f;
        score = sc;
    }

    public void RestartQuestions()
    {
        foreach (GameObject qgo in questions)
        {
            qgo.GetComponent<QuestionCollider>().question.failed = false;
            qgo.GetComponent<QuestionCollider>().question.completed = false;
        }

        
    }

    public virtual void OnRestart()
    {

    }

    public virtual void SetQuestions()
    {

    }
}
