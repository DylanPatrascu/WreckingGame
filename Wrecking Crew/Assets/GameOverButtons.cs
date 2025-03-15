using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverButtons : MonoBehaviour
{
    [SerializeField] private string menuScene;

    [SerializeField] private float blinkSpeed = 1;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color blinkColor;

    private Coroutine showSelected;

    public void RestartGame()
    {
        Time.timeScale = 1;
        JunkMeter.progress = 0;
        JunkMeter.maxProgress = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(menuScene);
        
    }

    public void ButtonSelected(Button button)
    {
        if (showSelected != null) StopCoroutine(showSelected);
        showSelected = StartCoroutine(ShowSelected(button));
    }

    public void ButtonDeselected(Button button)
    {
        button.gameObject.GetComponentInChildren<TMP_Text>().color = normalColor;
    }


    private IEnumerator ShowSelected(Button button)
    {
        TMP_Text buttonText = button.gameObject.GetComponentInChildren<TMP_Text>();
        float time = 0;
        float t;
        bool dir = true;

        while (true)
        {
            t = time / blinkSpeed;
            if (dir) buttonText.color = Color.Lerp(normalColor, blinkColor, t);
            else buttonText.color = Color.Lerp(blinkColor, normalColor, t);
            time += Time.fixedDeltaTime;

            if (time > blinkSpeed)
            {
                dir = !dir;
                time %= blinkSpeed;
            }
                
            yield return null;
        }
    }

}
