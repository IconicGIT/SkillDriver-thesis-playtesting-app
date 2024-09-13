using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour
{
    GameObject cam;
    GameObject pl;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.currentLevel.MapPrefab != null)
            Instantiate(GameManager.Instance.currentLevel.MapPrefab);

        cam = GameObject.FindGameObjectWithTag("MainCamera");
        pl = GameObject.FindGameObjectWithTag("Player");

        //cam.transform.parent = pl.transform;
        cam.transform.localPosition = new Vector3(0, 0, -10);

    }

    private void Update()
    {
        if (pl != null && cam != null)
        {
            cam.transform.position = new Vector3(pl.transform.position.x, pl.transform.position.y, -10);


        }
    }

}
