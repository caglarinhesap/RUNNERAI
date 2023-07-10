using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Opponent : MonoBehaviour
{
    public NavMeshAgent OpponentAgent;
    public GameObject Target;
    Vector3 OpponentStartPos;
    private const float speedBoostValue = 3.0f; 
    private const float speedBoostDuration = 1.5f;
    public GameObject speedBoosterIcon;

    // Start is called before the first frame update
    void Start()
    {
        OpponentAgent = GetComponent<NavMeshAgent>();
        OpponentStartPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        speedBoosterIcon.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        OpponentAgent.SetDestination(Target.transform.position);
        if (GameManager.instance.isGameOver)
        {
            OpponentAgent.isStopped = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            transform.position = OpponentStartPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpeedBoost"))
        {
            BoostCollected();
        }

        if (other.CompareTag("FinishLine"))
        {
            OpponentFinished();
        }
    }

    private void BoostCollected()
    {
        OpponentAgent.speed += speedBoostValue;
        speedBoosterIcon.SetActive(true);
        StartCoroutine(EndSpeedBoostCoroutine());
    }

    private void OpponentFinished()
    {
        OpponentAgent.isStopped = true;

        for (int i = 0; i < GameManager.instance.sortArray.Count; i++)
        {
            GameManager.instance.finishedRanks[i] = true;
        }
    }
    private IEnumerator EndSpeedBoostCoroutine()
    {
        yield return new WaitForSeconds(speedBoostDuration);
        OpponentAgent.speed -= speedBoostValue;
        speedBoosterIcon.SetActive(false);
    }
}
