using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCollector : MonoBehaviour
{
    private int coin = 0;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        // Try to find the scoreText if not manually assigned in Inspector
        if (scoreText == null)
        {
            GameObject scoreObj = GameObject.Find("ScoreText");
            if (scoreObj != null)
            {
                scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
            }

            if (scoreText == null)
            {
                Debug.LogError("ScoreText is not assigned and could not be found in the scene.");
            }
        }

        UpdateScoreText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coin++;
            Destroy(other.gameObject);
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + coin;
        }
    }
}
