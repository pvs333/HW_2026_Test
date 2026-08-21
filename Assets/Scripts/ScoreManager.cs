using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public Animator anim;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bigScore;
    public TextMeshProUGUI gameOverText, subtext;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GameOver()
    {
        StartCoroutine(gameEndDelay(1.5f));
    }

    IEnumerator gameEndDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        scoreText.text = "";
        bigScore.text = "score: " + score.ToString();
        gameOverText.text = "Game Over!";
        subtext.text = "Press R to Restart";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }

    public void UpdateScore()
    {
        scoreText.text = score.ToString();
        anim.SetTrigger("score");
    }
}
