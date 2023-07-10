using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private InGameRanking igRanking;
    private GameObject[] runners;
    public List<RankingSystem> sortArray = new List<RankingSystem>();
    public List<bool> finishedRanks;
    public bool isGameOver = false;

    public string username = "Player";

    private void Awake()
    {
        instance = this;
        runners = GameObject.FindGameObjectsWithTag("Runner");
        igRanking = FindObjectOfType<InGameRanking>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < runners.Length; i++)
        {
            if (runners[i].name == "Player")
            { 
                runners[i].name = PlayerPrefs.GetString("username");
            }

            sortArray.Add(runners[i].GetComponent<RankingSystem>());
            finishedRanks.Add(false);
        }
    }

    void CalculateRank()
    {
        sortArray = sortArray.OrderBy(x => x.distance).ToList();
        sortArray[0].rank = 1;
        sortArray[1].rank = 2;
        sortArray[2].rank = 3;
        sortArray[3].rank = 4;
        sortArray[4].rank = 5;
        sortArray[5].rank = 6;
        sortArray[6].rank = 7;

        if (!finishedRanks[0])
        {
            igRanking.a = sortArray[6].name;
        }
        if (!finishedRanks[1])
        {
            igRanking.b = sortArray[5].name;
        }
        if (!finishedRanks[2])
        {
            igRanking.c = sortArray[4].name;
        }
        if (!finishedRanks[3])
        {
            igRanking.d = sortArray[3].name;
        }
        if (!finishedRanks[4])
        {
            igRanking.e = sortArray[2].name;
        }
        if (!finishedRanks[5])
        {
            igRanking.f = sortArray[1].name;
        }
        if (!finishedRanks[6])
        {
            igRanking.g = sortArray[0].name;
        }

        /*
        //TODO: Stop updating finished players rank
        igRanking.a = sortArray[6].name;
        igRanking.b = sortArray[5].name;
        igRanking.c = sortArray[4].name;
        igRanking.d = sortArray[3].name;
        igRanking.e = sortArray[2].name;
        igRanking.f = sortArray[1].name;
        igRanking.g = sortArray[0].name;
        */

    }

    // Update is called once per frame
    void Update()
    {
        CalculateRank();
    }
}
