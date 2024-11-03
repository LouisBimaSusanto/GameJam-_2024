using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEManager : MonoBehaviour
{
    public GameObject qtePanel;
    public RectTransform needle;
    public RectTransform hitArea;
    public float qteDuration = 2f;
    public float needleSpeed = 200f;

    private bool qteActive = false;
    private bool success = false;
    private float originalNeedleX;
    private EnemyHealth enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        qtePanel.SetActive(false);
        originalNeedleX = needle.anchoredPosition.x;
    }

    public void StartQTE(EnemyHealth enemyHealthToDamage)
    {
        enemyHealth = enemyHealthToDamage;
        success = false;
        qteActive = true;
        needle.anchoredPosition = new Vector2(originalNeedleX, needle.anchoredPosition.y); // Reset posisi jarum
        qtePanel.SetActive(true);
        StartCoroutine(QTECoroutine());
    }

    private IEnumerator QTECoroutine()
    {
        float timer = 0f;

        while (timer < qteDuration)
        {
            if (!success && qteActive)
            {
                needle.anchoredPosition += Vector2.right * needleSpeed * Time.deltaTime;

                if (needle.anchoredPosition.x > qtePanel.GetComponent<RectTransform>().rect.width)
                {
                    needle.anchoredPosition = new Vector2(originalNeedleX, needle.anchoredPosition.y);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (IsNeedleInHitArea())
                    {
                        success = true;
                        qtePanel.SetActive(false); // Nonaktifkan QTE UI
                        ExecuteQTE(); // Eksekusi QTE
                    }
                    else
                    {
                        GameOver(); // Jika gagal
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

    private bool IsNeedleInHitArea()
    {
        float needleX = needle.anchoredPosition.x;
        float hitAreaMinX = hitArea.anchoredPosition.x - (hitArea.rect.width / 2);
        float hitAreaMaxX = hitArea.anchoredPosition.x + (hitArea.rect.width / 2);

        return needleX >= hitAreaMinX && needleX <= hitAreaMaxX;
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
