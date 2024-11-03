using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QTEManager : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB; 
    public Transform safeZone; 
    public float moveSpeed = 100f; 
    public GameObject qtePanel; // QTE UI Panel to show/hide
    public Transform pointer; 

    private Vector3 targetPosition;
    private EnemyHealth enemyHealth; 
    private bool qteActive = false;

    void Start()
    {
        if (pointer == null)
        {
            Debug.LogError("Pointer not assigned. Please assign a pointer GameObject in the Inspector.");
            return;
        }

        targetPosition = pointB.position; 
        pointer.position = pointA.position;
        qtePanel.SetActive(false); // Hide QTE panel initially
    }

    public void StartQTE(EnemyHealth targetEnemy)
    {
        enemyHealth = targetEnemy; // Set enemy for QTE
        qteActive = true;
        qtePanel.SetActive(true); // Show QTE panel
        pointer.position = pointA.position;
        targetPosition = pointB.position; 
    }

    void Update()
    {
        if (!qteActive || pointer == null) return;

        pointer.position = Vector3.MoveTowards(pointer.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(pointer.position, pointA.position) < 0.1f)
        {
            targetPosition = pointB.position;
        }
        else if (Vector3.Distance(pointer.position, pointB.position) < 0.1f)
        {
            targetPosition = pointA.position;
        }

        // Spacebar press to check success
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckSuccess();
        }
    }

    void CheckSuccess()
    {
        // Ensure the pointer and safe zone are both RectTransforms for accurate UI checking
        RectTransform pointerRect = pointer.GetComponent<RectTransform>();
        RectTransform safeZoneRect = safeZone.GetComponent<RectTransform>();

        if (pointerRect != null && safeZoneRect != null)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(safeZoneRect, pointerRect.position, null))
            {
                Debug.Log("Success!");
                ExecuteQTE();
            }
            else
            {
                Debug.Log("Fail!");
                GameOver();
            }
        }
        else
        {
            if (Vector3.Distance(pointer.position, safeZone.position) < 0.5f) 
            {
                Debug.Log("Success!");
                ExecuteQTE();
            }
            else
            {
                Debug.Log("Fail!");
                GameOver();
            }
        }

        qteActive = false;
        qtePanel.SetActive(false);
    }

    void ExecuteQTE()
    {
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(100);
            Debug.Log("Enemy damaged!");
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over! Failed QTE.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
