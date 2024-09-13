using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionCollider : MonoBehaviour
{
    public QuestionManager.Question question;

    private void Awake()
    {
        question.questionId = GetInstanceID();
    }

}
