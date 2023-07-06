using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingSystem : MonoBehaviour
{
    public float distance;
    public GameObject target;
    public int rank;

    void CalculateDistance()
    {
        //distance = Vector3.Distance(transform.position, target.transform.position);
        distance = Math.Abs(transform.position.z-target.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDistance();
    }
}
