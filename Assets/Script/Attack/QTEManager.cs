using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEManager : MonoBehaviour
{
    public GameObject qtePanel;
    public Image needle;
    public float qteDuration = 2f;
    public float needleSpeed = 200f;
    public float hitAreaSize = 30f;

    private bool qteActive = false;
    private float targetAngle;
    private float currentAngle;
    private bool success = false;
    private EnemyHealth enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        qtePanel.SetActive(false);
    }

    public void StartQTE(EnemyHealth enemyHealthToDamage)
    {
        enemyHealth = enemyHealthToDamage;
        success = false;
        currentAngle = 0f;
        targetAngle = Random.Range(0f, 360f);
        qtePanel.SetActive(true);
        StartCoroutine(QTECoroutine());
    }

    private IEnumerator QTECoroutine()
    {
        float timer = 0f;

        while (timer < qteDuration)
        {
            if (!success)
            {
                currentAngle += needleSpeed * Time.deltaTime;
                needle.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);

                float angleDifference = Mathf.Abs((currentAngle % 360) - targetAngle);
                if (angleDifference < hitAreaSize)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        success = true;
                        qtePanel.SetActive(false) ;
                        ExecuteQTE();
                    }
                }
            }
            timer += Time.deltaTime;
            yield return null;
        }

        if (!success)
        {
            GameOver();
        }
    }

    private void ExecuteQTE()
    {
        if (enemyHealth !=null)
        {
            enemyHealth.TakeDamage(100);
            Debug.Log("QTE don");
        }
    }

    private void GameOver() 
    {
        Debug.Log("Cupu");
    }
}
