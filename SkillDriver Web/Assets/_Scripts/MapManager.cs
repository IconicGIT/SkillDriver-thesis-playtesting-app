using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using TMPro;

public class MapManager : MonoBehaviour
{

    [SerializeField] [Range(0,1f)] float currentProgress = 0;

    GameManager gameManager;
    public bool restartOnFinished;
    public bool tripPaused = false;
    QuestionManager questionManager;
    [SerializeField] SplineContainer road;
    [SerializeField] GameObject Avatar;

    public bool onTrip = false;
    [SerializeField] float progress = 0;
    [SerializeField] float startTime;
    [SerializeField] float finishTime;
    [SerializeField] float timeToComplete;

    public bool useSpeed = false;
    [SerializeField] float speed;
    float roadLength;
    // Start is called before the first frame update
    void Start()
    {
        progress = 0;
        GameObject gm = GameObject.Find("Game Manager");
        gameManager = gm.GetComponent<GameManager>();
        questionManager = GetComponent<QuestionManager>();
        questionManager.mapManager = this;
        road = GetComponentInChildren<SplineContainer>();
        roadLength = road.CalculateLength();


    }
    private void Update()
    {
        if (onTrip)
        {

            if (!tripPaused)
            {
                if (useSpeed)
                    progress += speed * Time.deltaTime / roadLength;
                else
                    progress = (Time.time - startTime) / timeToComplete;




                //foreach (GameObject qgo in questionManager.questions)
                //{
                //    QuestionManager.Question q = qgo.GetComponent<QuestionCollider>().question;
                //    if (progress >= q.placeInRoad && !q.completed)
                //    {
                //        questionManager.ShowQuestion(q);
                //    }
                //}
            }


            if (progress < 0) progress = 0;
            if (progress > 1)
            {
                if (restartOnFinished)
                {
                    progress = 0;
                    questionManager.OnRestart(); // call for restart
                }
                else
                {
                    tripPaused = true;
                    EndTrip();
                }
               
            }
        }
        else
            progress = currentProgress;

        Avatar.transform.position = road.EvaluatePosition(progress);
        Avatar.transform.rotation = Quaternion.LookRotation(Vector3.forward, road.EvaluateTangent(progress));
    }

    public void StartTrip()
    {
        onTrip = true;
        startTime = Time.time;
        finishTime = startTime + timeToComplete;
        questionManager.SetQuestions();
    }

    [SerializeField] GameObject endPanel;
    [SerializeField] TextMeshProUGUI failsText;
    [SerializeField] TextMeshProUGUI succeedsText;
    [SerializeField] TextMeshProUGUI totalScoreText;
    public void EndTrip()
    {
        int succeeds, fails, score;

        questionManager.GetResults(out succeeds, out fails, out score);

        endPanel.SetActive(true);
        failsText.text = fails.ToString();
        succeedsText.text = succeeds.ToString();
        totalScoreText.text = score.ToString();

        gameManager.levels[gameManager.currentLevel.levelIndex].score = score;

    }

    public void ReturnToLevelSelector()
    {

        print("Link levels to maps + pass scores");
        gameManager.currentScene = "LevelSelector";
        gameManager.ChangeScene();

        //TODO:
        /*

        DONE: make map
        make questions

         
         */
    }

}
