using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class CarWobbler : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] bool pauseOnQuestion;
    [SerializeField] bool instantPos;
    [SerializeField] float vForce;
    [SerializeField] float vDist;
    [SerializeField] float hForce;
    [SerializeField] float hDist;
    [SerializeField] float smoothSpeed;
    [SerializeField] MapManager mapManager;
    [SerializeField] Transform avatar;
    //float lastDisplacement;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float timeStep = Time.time * 0.1f; // Adjust the step value to smooth out the noise
        float v = Mathf.PerlinNoise(timeStep, timeStep + 1000000);
        float h = Mathf.PerlinNoise(timeStep, timeStep - 1000000);

        float height = Mathf.Sin(Mathf.Deg2Rad * Time.time * vForce) * vDist * v; // vertical displacement
        float distance = Mathf.Sin(Mathf.Deg2Rad * Time.time * hForce) * hDist * h; // horizontal displacement

        if (mapManager.onTrip)
        {
            if (pauseOnQuestion && !mapManager.tripPaused)
            {
                Vector3 targetPosition = avatar.position + new Vector3(distance, height, 0);
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed); // Adjust smoothSpeed as needed

            }
            else if (!pauseOnQuestion)
            {
                Vector3 targetPosition = avatar.position + new Vector3(distance, height, 0);
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed); // Adjust smoothSpeed as needed

            }
            if (instantPos)
            {
                transform.position = avatar.position + new Vector3(distance, height, 0);
            }


        }


    }
}
