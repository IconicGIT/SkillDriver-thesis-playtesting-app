using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class QuizRushManager : QuestionManager
{
    [SerializeField] QuestionTheme currentTheme = QuestionTheme.NONE;
    Queue<QuestionTheme> lastThemes = new Queue<QuestionTheme>();
    [SerializeField] int avoidRepeatThemeCount;

    uint score;
    uint succeededCount;

    [SerializeField] GameObject restartPanel;
    [SerializeField] ThemeButton StartThemeButton1;
    [SerializeField] ThemeButton StartThemeButton2;
    [SerializeField] ThemeButton StartThemeButton3;

    [SerializeField] ThemeButton NextThemeButton1;
    [SerializeField] ThemeButton NextThemeButton2;
    [SerializeField] ThemeButton NextThemeButton3;

    public List<QuestionTheme> bannedThemes = new List<QuestionTheme>();

    public List<Question> initiation;
    public List<Question> documentation;
    public List<Question> the_driver;
    public List<Question> obligations;
    public List<Question> vehicle_elements;
    public List<Question> traffic_norms;
    public List<Question> maneuvers;
    public List<Question> transportation;
    public List<Question> preventive_driving;
    public List<Question> vehicle_mantainance;

    public List<Question> shownQuestions;
    // Start is called before the first frame update
    override public void Start() 
    {
        GameObject[] allQuestions = GameObject.FindGameObjectsWithTag("Question");
        foreach (var question in allQuestions) 
        {
            Question q = question.GetComponent<QuestionCollider>().question;
            print("start: " + q.statement);
            switch (q.theme)
            {
                case QuestionTheme.INITIATION:
                    initiation.Add(q);
                    break;
                    case QuestionTheme.DOCUMENTATION:
                        documentation.Add(q);
                    break;
                    case QuestionTheme.THE_DRIVER:
                        the_driver.Add(q);
                    break;
                    case QuestionTheme.OBLIGATIONS:
                        obligations.Add(q);
                    break;
                    case QuestionTheme.VEHICLE_ELEMENTS:
                        vehicle_elements.Add(q);
                    break;
                    case QuestionTheme.TRAFFIC_NORMS:
                        traffic_norms.Add(q);
                    break;
                    case QuestionTheme.MANEUVERS:
                        maneuvers.Add(q);
                    break;
                    case QuestionTheme.TRANSPORTATION:
                        transportation.Add(q);
                    break;
                    case QuestionTheme.PREVENTIVE_DRIVING:
                        preventive_driving.Add(q);
                    break;

                    case QuestionTheme.VEHICLE_MAINTENANCE:
                        vehicle_mantainance.Add(q);
                    break;
                default:
            break;}
        }

        
    }

    private void Awake()
    {
        SetThemeButtons(ref StartThemeButton1, ref StartThemeButton2, ref StartThemeButton3);
    }


    void SetThemeButtons(ref ThemeButton themeButton1, ref ThemeButton themeButton2, ref ThemeButton themeButton3)
    {

        QuestionTheme theme;
        do
        {
            theme = (QuestionTheme)Random.Range(0, 10);
            themeButton1.theme = theme;
        } while (lastThemes.Contains(theme) ||
                bannedThemes.Contains(theme) ||
                themeButton1.theme == QuestionTheme.NONE);


        do
        {
            theme = (QuestionTheme)Random.Range(0, 10);
            themeButton2.theme = theme;
        } while (lastThemes.Contains(theme)
                || bannedThemes.Contains(theme)
                || themeButton2.theme == QuestionTheme.NONE
                || themeButton2.theme == themeButton1.theme);

        do
        {
            theme = (QuestionTheme)Random.Range(0, 10);
            themeButton3.theme = theme;
        } while (lastThemes.Contains(theme)
                || bannedThemes.Contains(theme)
                || themeButton3.theme == QuestionTheme.NONE
                || themeButton3.theme == themeButton1.theme
                || themeButton3.theme == themeButton2.theme);

        if (lastThemes.Count > avoidRepeatThemeCount)
            lastThemes.Dequeue();

        themeButton1.UpdateButtonText();
        themeButton2.UpdateButtonText();
        themeButton3.UpdateButtonText();
    }
    public void SelectTheme(QuestionTheme theme)
    {

        currentTheme = theme;
        lastThemes.Enqueue(theme);
        //foreach (var item in lastThemes)
        //{
        //    print(item);
        //}
    }

    public override void OnRestart()
    {
        mapManager.tripPaused = true;
        SetThemeButtons(ref NextThemeButton1, ref NextThemeButton2, ref NextThemeButton3);
        restartPanel.SetActive(true);
    }

    public override void EndQuestion()
    {
        currentQuestion.completed = true;
        questions[GetQuestionIndex(currentQuestion)].GetComponent<QuestionCollider>().question = currentQuestion;
        eventQuestion.SetActive(false);
        if (failCount < 3)
        {
            mapManager.tripPaused = false;
            resultPanel.SetActive(false);
            score += 100;
            succeededCount++;
        }
        else
        {
            resultPanel.SetActive(false);
            mapManager.EndTrip();
        }

       
        scoreText.text = score.ToString();
    }

    public override void GetResults(out int succeeds, out int fails, out int score)
    {

        succeeds = (int)succeededCount;
        fails = failCount;
        score = (int)this.score;
    }

    public override void SetQuestions()
    {

        List<Question> questions = new List<Question>();

        switch (currentTheme)
        {
            case QuestionTheme.INITIATION:
                AddRandomQuestion(initiation, questions, shownQuestions);
                break;
            case QuestionTheme.DOCUMENTATION:
                AddRandomQuestion(documentation, questions, shownQuestions);
                break;
            case QuestionTheme.THE_DRIVER:
                AddRandomQuestion(the_driver, questions, shownQuestions);
                break;
            case QuestionTheme.OBLIGATIONS:
                AddRandomQuestion(obligations, questions, shownQuestions);
                break;
            case QuestionTheme.VEHICLE_ELEMENTS:
                AddRandomQuestion(vehicle_elements, questions, shownQuestions);
                break;
            case QuestionTheme.TRAFFIC_NORMS:
                AddRandomQuestion(traffic_norms, questions, shownQuestions);
                break;
            case QuestionTheme.MANEUVERS:
                AddRandomQuestion(maneuvers, questions, shownQuestions);
                break;
            case QuestionTheme.TRANSPORTATION:
                AddRandomQuestion(transportation, questions, shownQuestions);
                break;
            case QuestionTheme.PREVENTIVE_DRIVING:
                AddRandomQuestion(preventive_driving, questions, shownQuestions);
                break;
            case QuestionTheme.VEHICLE_MAINTENANCE:
                AddRandomQuestion(vehicle_mantainance, questions, shownQuestions);
                break;
            default:
                break;
        }

        for (int i = 0; i < this.questions.Length; i++)
        {
            this.questions[i].GetComponent<QuestionCollider>().question = shownQuestions[i];
        }

        mapManager.tripPaused = false; //end of restart
    }

    private void AddRandomQuestion(List<Question> questionPool, List<Question> questions, List<Question> shownQuestions)
    {
        questions.Add(questionPool[Random.Range(0, questionPool.Count)]);

        Question q;

        shownQuestions.Clear();
        while (shownQuestions.Count < 10)
        {
            do
            {
                q = questionPool[Random.Range(0, questionPool.Count)];
            }
            while (questions.Contains(q) || shownQuestions.Contains(q)); // Check both lists

            shownQuestions.Add(q);
        }
    }

}


