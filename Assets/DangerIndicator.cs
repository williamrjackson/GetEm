using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerIndicator : MonoBehaviour
{
    public UnityEngine.UI.Image redOverlay;
    public UnityEngine.UI.Text gameOverText;
    public UnityEngine.UI.Button retryButton;


    public void SetDangerValue(float level)
    {
        redOverlay.transform.Alpha(level, .25f);
    }
    public void ShowGameOver()
    {
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
    }
    public void HideGameOver()
    {
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
    }
}
