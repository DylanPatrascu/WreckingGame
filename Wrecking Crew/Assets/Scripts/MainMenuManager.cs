using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Image mainMenuBackground;
    [SerializeField] private Image fadePanel;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite button1Sprite; 
    [SerializeField] private Sprite button2Sprite; 
    [SerializeField] private Sprite button3Sprite; 
    [SerializeField] private Sprite button4Sprite;

    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private float fadeDuration = 1f;

    private void Start()
    {
        if (mainMenuBackground && idleSprite)
        {
            mainMenuBackground.sprite = idleSprite;
        }

        if (fadePanel)
        {
            Color temp = fadePanel.color;
            temp.a = 0f;
            fadePanel.color = temp;
        }
    }

    public void StartGame()
    {
        if (mainMenuBackground && button1Sprite)
            mainMenuBackground.sprite = button1Sprite;

        StartCoroutine(Transition("SampleScene"));
    }


    public void Customize()
    {
        if (mainMenuBackground && button2Sprite)
            mainMenuBackground.sprite = button2Sprite;

        StartCoroutine(Transition("CustomizeScene"));
    }

    public void Options()
    {
        if (mainMenuBackground && button3Sprite)
            mainMenuBackground.sprite = button3Sprite;

        StartCoroutine(Transition("OptionsScene"));
    }

    public void Quit()
    {
        if (mainMenuBackground && button4Sprite)
            mainMenuBackground.sprite = button4Sprite;

        StartCoroutine(Transition(""));
    }

    private IEnumerator Transition(string sceneName)
    {
        yield return new WaitForSeconds(animationDuration);

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            if (fadePanel)
            {
                Color c = fadePanel.color;
                c.a = Mathf.Lerp(0, 1, timer / fadeDuration);
                fadePanel.color = c;
            }
            yield return null;
        }

        if (fadePanel)
        {
            Color c = fadePanel.color;
            c.a = 1f;
            fadePanel.color = c;
        }

        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Application.Quit();
        }
    }

}
