using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatMap : MonoBehaviour
{
    [SerializeField] MapManager mapManager;
    [SerializeField] GameObject map;
    [SerializeField] float speed;
    [SerializeField] float repeatDistance;

    Vector3 startPos;
    [SerializeField] float distance;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (mapManager.onTrip)
        transform.position += new Vector3(0, Time.deltaTime * speed, 0);
        distance = Vector3.Distance(startPos, transform.position);
        if (distance > repeatDistance)
        {
            transform.position = startPos;
        }
    }
}
