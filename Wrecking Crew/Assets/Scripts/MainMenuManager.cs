using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Image mainMenuBackground;
    [SerializeField] private Image fadePanel;
    [SerializeField] Animator animator;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite button1Sprite; 
    [SerializeField] private Sprite button2Sprite; 
    [SerializeField] private Sprite button3Sprite; 
    [SerializeField] private Sprite button4Sprite;

    [SerializeField] private float animationDuration = 1f;
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

        gameObject.GetComponentInChildren<Button>().Select();
        audioManager = FindAnyObjectByType<AudioManager>();

    }

    public void StartGame()
    {
        audioManager.PlaySound(activateClip);
        if (mainMenuBackground && button1Sprite)
            mainMenuBackground.sprite = button1Sprite;
        animator.Play("Start");
        StartCoroutine(Transition("GameScene"));
    }


    public void Customize()
    {
        audioManager.PlaySound(activateClip);
        if (mainMenuBackground && button2Sprite)
            mainMenuBackground.sprite = button2Sprite;
        animator.Play("Customize");
        StartCoroutine(Transition("CustomizeScene"));
    }

    public void Options()
    {
        audioManager.PlaySound(activateClip);
        if (mainMenuBackground && button3Sprite)
            mainMenuBackground.sprite = button3Sprite;
        animator.Play("Options");
        StartCoroutine(Transition("OptionsScene"));
    }

    public void Quit()
    {
        audioManager.PlaySound(activateClip);
        if (mainMenuBackground && button4Sprite)
            mainMenuBackground.sprite = button4Sprite;
        animator.Play("Quit");
        StartCoroutine(Transition("Quit"));
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
