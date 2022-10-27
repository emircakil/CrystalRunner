using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject roadPrefab;
    public float offset = -0.7071068f;
    public Vector3 lastPosition;
    private double velocityTime = 1;
    private float createSpeed = 0.2f;
    private GameManager gameManager;

    private int roadCount = 0;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void StartBuilding()
    {
        InvokeRepeating("CreateNewRoadPart", 0.1f, createSpeed);
    }
    /*
    private void Update()
    {
        velocityTime -= Time.deltaTime;

        if (velocityTime <= 0 && createSpeed > 0.1f) {

            velocityTime = 10;
            createSpeed -= 0.1f;
        
        }

        if (!gameManager.gameStarted) {

            createSpeed = 0.5f;
            velocityTime = 10;
        }

    }
    */
    public void CreateNewRoadPart() {

        Vector3 spawnPosition = Vector3.zero;

        float chance = Random.Range(0, 100);

        if (chance < 50)
        {

            spawnPosition = new Vector3(lastPosition.x + offset, lastPosition.y, lastPosition.z - offset);


        }
        else { 
        
            spawnPosition = new Vector3(lastPosition.x - offset, lastPosition.y, lastPosition.z - offset);
        }

        GameObject g = Instantiate(roadPrefab, spawnPosition , Quaternion.Euler(0,45,0));

        lastPosition = g.transform.position;

        roadCount++;

        if (roadCount % 5 == 0) {

            g.transform.GetChild(0).gameObject.SetActive(true);
        }
    
    }

}
