using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject[] Border;
    public float timeCounter;
    public bool meeleLeft;
    public bool meeleRight;
    public bool rangedLeft;
    public bool rangedRight;
    int fixedMCounter = 0;
    int fixedRCounter = 0;
    void Start()
    {
        InvokeRepeating("SpawnEnemyM", 1f, 2f);
        InvokeRepeating("SpawnFixedM", 40f, 25f);
        InvokeRepeating("SpawnEnemyR", 60f, 4f);
        InvokeRepeating("SpawnFixedR", 130f, 25f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnFixedM()
    {
        if (fixedMCounter == 0)
        {
            meeleLeft = true;
            SpawnMeele(5);
        }
        else if (fixedMCounter == 1)
        {
            meeleRight = true;
            SpawnMeele(5);
        }
        else if (fixedMCounter == 2)
        {
            meeleLeft = true;
            meeleRight = true;
            SpawnMeele(5);
        }
        fixedMCounter++;
    }

    void SpawnFixedR()
    {
        if (fixedRCounter == 0)
        {
            rangedLeft = true;
            SpawnRanged(5);
        }
        else if (fixedRCounter == 1)
        {
            rangedRight = true;
            SpawnRanged(5);
        }
        else if (fixedRCounter == 2)
        {
            rangedLeft = true;
            rangedRight = true;
            SpawnRanged(5);
        }
        fixedRCounter++;
    }

    void SpawnEnemyM()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(-0.1f, -0.1f));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1.1f, 1.1f));

        GameObject enemyM = Instantiate(Enemy1);
        
        switch (Random.Range(1, 5))
        {
            case 1:
                enemyM.transform.position = new Vector2(max.x, Random.Range(min.y, max.y));
                while (enemyM.transform.position.y < Border[1].transform.position.y || enemyM.transform.position.y > Border[0].transform.position.y)
                {
                    enemyM.transform.position = new Vector2(max.x, Random.Range(min.y, max.y));
                }
                break;
            case 2:
                enemyM.transform.position = new Vector2(min.x, Random.Range(min.y, max.y));
                while (enemyM.transform.position.y < Border[1].transform.position.y || enemyM.transform.position.y > Border[0].transform.position.y)
                {
                    enemyM.transform.position = new Vector2(min.x, Random.Range(min.y, max.y));
                }
                break;
            case 3:
                enemyM.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
                break;
            case 4:
                enemyM.transform.position = new Vector2(Random.Range(min.x, max.x), min.y);
                break;
        }
    }
    void SpawnEnemyR()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(-0.1f, -0.1f));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1.1f, 1.1f));

        GameObject enemyR = Instantiate(Enemy2);

        switch (Random.Range(1, 5))
        {
            case 1:
                enemyR.transform.position = new Vector2(max.x, Random.Range(min.y, max.y));
                while (enemyR.transform.position.y < Border[1].transform.position.y || enemyR.transform.position.y > Border[0].transform.position.y)
                {
                    enemyR.transform.position = new Vector2(max.x, Random.Range(min.y, max.y));
                }
                break;
            case 2:
                enemyR.transform.position = new Vector2(min.x, Random.Range(min.y, max.y));
                while (enemyR.transform.position.y < Border[1].transform.position.y || enemyR.transform.position.y > Border[0].transform.position.y)
                {
                    enemyR.transform.position = new Vector2(min.x, Random.Range(min.y, max.y));
                }
                break;
            case 3:
                enemyR.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
                break;
            case 4:
                enemyR.transform.position = new Vector2(Random.Range(min.x, max.x), min.y);
                break;
        }
    }

    void SpawnMeele(int quantity)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(-0.1f, -0.1f));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1.1f, 1.1f));

        for (int i = 0; i < quantity; i++)
        {
            GameObject enemyM = Instantiate(Enemy1);
            if (meeleLeft)
            {
                enemyM.transform.position = new Vector2(min.x, Random.Range(min.y, max.y));
            }
            if (meeleRight)
            {
                enemyM.transform.position = new Vector2(max.x, Random.Range(min.y, max.y));
            }
        }
        meeleLeft = false;
        meeleRight = false;
    }
    void SpawnRanged(int quantity)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(-0.1f, -0.1f));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1.1f, 1.1f));


        for (int i = 0; i < quantity; i++)
        {
            GameObject enemyR = Instantiate(Enemy2);
            if (rangedLeft)
            {
                enemyR.transform.position = new Vector2(min.x, Random.Range(min.y, max.y));
            }
            if (rangedRight)
            {
                enemyR.transform.position = new Vector2(max.x, Random.Range(min.y, max.y));
            }
        }
        rangedLeft = false;
        rangedRight = false;
    }
}
