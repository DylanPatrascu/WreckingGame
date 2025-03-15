using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class EndMenuManager : MonoBehaviour
{
    [SerializeField] private Image endMenuBackground;
    [SerializeField] private Image fadeInImage;
    [SerializeField] private Image fadePanel;
    [SerializeField] private Sprite idleSprite;

    [SerializeField] private float fadeInDuration = 5;
    [SerializeField] private float fadeDuration = 1f;

    [Header("Select Animations")]
    [SerializeField] private float blinkSpeed = 1;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color blinkColor;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip selectClip;
    [SerializeField] private AudioClip activateClip;

    private Coroutine showSelected;
    private AudioManager audioManager;

    private void Start()
    {
        if (fadePanel)
        {
            Color temp = fadePanel.color;
            temp.a = 0f;
            fadePanel.color = temp;
        }

        audioManager = FindAnyObjectByType<AudioManager>();
        StartCoroutine(FadeIn());

    }

    public void restartGame()
    {
        audioManager.PlaySound(activateClip);
        StartCoroutine(Transition("GameScene"));
    }

    public void Quit()
    {
        audioManager.PlaySound(activateClip);
        StartCoroutine(Transition("Quit"));
    }

    public void Mainmenu()
    {
        audioManager.PlaySound(activateClip);
        StartCoroutine(Transition("MainMenu"));
    }
    private IEnumerator Transition(string sceneName)
    {
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

    private IEnumerator FadeIn()
    {
        float time = 0;
        float t = 0;

        Color old = fadeInImage.color;
        Color newC = fadeInImage.color;
        newC.a = 0;

        while (t < fadeInDuration)
        {
            t = time / fadeInDuration;
            fadeInImage.color = Color.Lerp(old, newC, t);
            time += Time.deltaTime;
            yield return null;
        }
        fadeInImage.color = newC;

    }

    public void ButtonSelected(Button button)
    {
        if (showSelected != null) StopCoroutine(showSelected);
        showSelected = StartCoroutine(ShowSelected(button));
        audioManager.PlaySound(selectClip);
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
