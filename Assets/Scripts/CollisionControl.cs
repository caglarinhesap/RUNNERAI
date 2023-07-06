using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CollisionControl : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject FinishPanel;
    Vector3 PlayerStartPos; 
    private const float speedBoostValue = 3.0f;
    private const float speedBoostDuration = 2.0f;
    public GameObject speedBoosterIcon;
    private InGameRanking igRanking;

    private void Start()
    {
        PlayerStartPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        speedBoosterIcon.SetActive(false);
        igRanking = FindObjectOfType<InGameRanking>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {
            PlayerFinished();
        }

        if (other.CompareTag("SpeedBoost"))
        {
            BoostCollected();
        }
    }

    private void PlayerFinished()
    {
        playerController.runningSpeed = 0;
        transform.Rotate(transform.rotation.x, 180, transform.rotation.z, Space.Self);
        FinishPanel.SetActive(true);
        GameManager.instance.isGameOver = true;
    }

    private void BoostCollected()
    {
        playerController.runningSpeed += speedBoostValue;
        speedBoosterIcon.SetActive(true);
        StartCoroutine(EndSpeedBoostCoroutine());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            transform.position = PlayerStartPos;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator EndSpeedBoostCoroutine()
    {
        yield return new WaitForSeconds(speedBoostDuration);
        playerController.runningSpeed -= speedBoostValue;
        speedBoosterIcon.SetActive(false);
    }
}
