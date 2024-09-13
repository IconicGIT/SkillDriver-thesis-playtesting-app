using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestionManager;

public class PlayerCollider : MonoBehaviour
{

    [SerializeField] QuestionManager questionManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        QuestionCollider quCol = collision.gameObject.GetComponent<QuestionCollider>();
        if (quCol != null)
        {
            questionManager.ShowQuestion(ref quCol);
            Question question = quCol.GetComponent<QuestionCollider>().question;
            print(question.questionId);

        }
    }
}
