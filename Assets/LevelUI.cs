using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text gameOverText;
    public TMP_Text lifeLostText;
    public TMP_Text pressKeyToStartText;
    public TMP_Text youWinText;

    public Image heart1;
    public Image heart2;
    public Image heart3;

    public void UpdateHeartsUI(int life)
    {
        heart1.color = life >= 1 ? Color.white : Color.gray2;
        heart2.color = life >= 2 ? Color.white : Color.gray2;
        heart3.color = life >= 3 ? Color.white : Color.gray2;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void ShowLifeLost()
    {
        lifeLostText.gameObject.SetActive(true);
    }

    public void ShowGameOver()
    {
        gameOverText.gameObject.SetActive(true);
    }

    public void HideLifeLost()
    {
        lifeLostText.gameObject.SetActive(false);
    }

    public void HidePressKey()
    {
        pressKeyToStartText.gameObject.SetActive(false);
    }

    public void ShowWin()
    {
        youWinText.gameObject.SetActive(true);
    }
}
