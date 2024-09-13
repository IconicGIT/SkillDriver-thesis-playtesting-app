using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThemeButton : MonoBehaviour
{
    public QuestionManager.QuestionTheme theme;
    public QuizRushManager quizRushManager;
    [SerializeField] MapManager mapManager;

    private void Start()
    {
        UpdateButtonText();
    }
    public void UpdateButtonText()
    {
        string themeText = "---";
        switch (theme)
        {
            case QuestionManager.QuestionTheme.NONE:
                themeText = "None";
                break;
            case QuestionManager.QuestionTheme.INITIATION:
                themeText = "Iniciaci�n";
                break;
            case QuestionManager.QuestionTheme.DOCUMENTATION:
                themeText = "Documentaci�n";
                break;
            case QuestionManager.QuestionTheme.THE_DRIVER:
                themeText = "El Conductor";
                break;
            case QuestionManager.QuestionTheme.OBLIGATIONS:
                themeText = "Obligaciones";
                break;
            case QuestionManager.QuestionTheme.VEHICLE_ELEMENTS:
                themeText = "Elementos del veh�culo";
                break;
            case QuestionManager.QuestionTheme.TRAFFIC_NORMS:
                themeText = "Normas de tr�fico";
                break;
            case QuestionManager.QuestionTheme.MANEUVERS:
                themeText = "Maniobras";
                break;
            case QuestionManager.QuestionTheme.TRANSPORTATION:
                themeText = "Transportaci�n";
                break;
            case QuestionManager.QuestionTheme.PREVENTIVE_DRIVING:
                themeText = "Conducci�n Preventiva";
                break;
            case QuestionManager.QuestionTheme.VEHICLE_MAINTENANCE:
                themeText = "Mantenimiento del veh�culo";
                break;
            default:
                break;
        }

        var text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = themeText;
    }
    public void SelectTheme()
    {
        quizRushManager.SelectTheme(theme);

        if (!mapManager.onTrip)
        {
            mapManager.StartTrip();
        }
        else
        {
            quizRushManager.SetQuestions();
        }

        transform.parent.gameObject.SetActive(false);
    }
}
