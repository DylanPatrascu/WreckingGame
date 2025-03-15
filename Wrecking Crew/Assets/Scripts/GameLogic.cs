using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{

    [SerializeField] private PlayerControls player;
    [SerializeField] private new CinemachineVirtualCamera camera;

    [Header("GameTimer")]
    [SerializeField] private Transform gameTimer;
    [SerializeField] private TMP_Text gameTimerText;
    [SerializeField] private float startTime = 10.0f;
    [SerializeField] private float slideInTime = 1f;
    [SerializeField] Color NearDeathColor = Color.red;
    [SerializeField] float NearDeathBlinkSpeed = 0.5f;

    [Header("Countdown Time")]
    [SerializeField] TMP_Text countdownTimerText;
    [SerializeField] private float countDownTime = 3;

    [Header("Game Over")]
    [SerializeField] Image gameOverImage;
    [SerializeField] GameObject gameOverButton1;
    [SerializeField] GameObject gameOverButton2;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] float targetGameOverImageAlpha = 0.25f;
    [SerializeField] float gameOverScreenTime = 3;

    [Header("WIN the game, bitch")]
    [SerializeField] Image fadeOutImage;
    [SerializeField] string winScene;
    [SerializeField] float fadeOutTime;
    [SerializeField] AudioHighPassFilter hp;

    [Header("Time Added Label")]
    [SerializeField] GameObject timeAddedLabel;
    [SerializeField] Transform timeAddedLabelGroup;
    [SerializeField] float timeAddedLabelTime = 5f;
    [SerializeField] float timeAddedLabelFadeTime = 0.5f;

    [SerializeField] private AudioManager audioManager;

    public static float timeRemaining;

    public static bool gameRunning;

    private void Start()
    {
        StopAllCoroutines();

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

        timeRemaining = startTime;
        gameRunning = false;

        StartCoroutine(StartGame());
    }

    private void Update()
    {
        if (gameRunning)
        {
            if (timeRemaining > 0)
            {
                gameTimerText.text = (Mathf.Floor(timeRemaining) / 60).ToString("00") + ":" + (Mathf.Floor(timeRemaining % 60)).ToString("00");
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                StartCoroutine(GameOver());
            }
            if (JunkMeter.progress == JunkMeter.maxProgress)
            {
                StartCoroutine(WinGame());
            }
                
        }
        
    }

    public void AddTime(float time)
    {
        StartCoroutine(TimeAddedLabel(time));
        timeRemaining += time;
    }

    private IEnumerator StartGame()
    {
        float timer = countDownTime;
        float startingOrthoSize = camera.m_Lens.OrthographicSize;
        Time.timeScale = 1;
        while (timer > 0)
        {
            if (countdownTimerText.text != Mathf.Ceil(timer).ToString())
            {
                countdownTimerText.text = Mathf.Ceil(timer).ToString();
                camera.m_Lens.OrthographicSize += 2.5f;
            }
            timer -= Time.deltaTime;
            yield return null;
        }

        camera.m_Lens.OrthographicSize = startingOrthoSize;
        countdownTimerText.text = "GO!";
        gameRunning = true;
        StartCoroutine(GameTimerSlide());
        StartCoroutine(TimerBlink());

        yield return new WaitForSeconds(1);

        timer = 0.5f;
        float t;
        Color startColor = countdownTimerText.color;
        Color endColor = startColor;
        endColor.a = 0;

        while (timer > 0)
        {
            t = timer / 0.5f;
            countdownTimerText.color = Color.Lerp(endColor, startColor, t);
            timer -= Time.deltaTime;
            yield return null;
        }
        countdownTimerText.color = endColor;
        countdownTimerText.gameObject.SetActive(false);
    }

    private IEnumerator GameTimerSlide()
    {
        float timer = 0f;
        float t;
        float curvePos;
        
        Vector3 startPos = gameTimer.position;
        Vector3 endPos = startPos;
        endPos.x += 2 * Mathf.Abs(endPos.x);
        AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, slideInTime, 1);

        while (timer < slideInTime)
        {
            t = timer / slideInTime;
            curvePos = curve.Evaluate(t);
            gameTimer.position = Vector3.Lerp(startPos, endPos, curvePos);
            timer += Time.deltaTime;
            yield return null;
        }
        gameTimer.position = endPos;
    }

    private IEnumerator TimerBlink()
    {
        Color NormalColor = gameTimerText.color;
        float time = 0;
        float t = 0;
        bool fadeDir = false;

        while (gameRunning)
        {
            if (timeRemaining < 4 || gameTimerText.color != NormalColor)
            {
                t = (time % (NearDeathBlinkSpeed / 2)) / (NearDeathBlinkSpeed / 2);

                switch (fadeDir)
                {
                    case true:
                        gameTimerText.color = Color.Lerp(NormalColor, NearDeathColor, t);
                        break;
                    case false:
                        gameTimerText.color = Color.Lerp(NearDeathColor, NormalColor, t);
                        break;
                }
                
                time += Time.deltaTime;
                if (time >= NearDeathBlinkSpeed / 2)
                {
                    switch (fadeDir)
                    {
                        case true:
                            gameTimerText.color = NearDeathColor;
                            break;
                        case false:
                            gameTimerText.color = NormalColor;
                            break;
                    }
                    fadeDir = !fadeDir;
                    time = 0;
                }

            }
            yield return null;
        }

    }

    private IEnumerator GameOver()
    {
        float time = 0;
        float t;
        Color startColor = gameOverImage.color;
        Color endColor = startColor;
        endColor.a = targetGameOverImageAlpha;

        while (time < gameOverScreenTime)
        {
            t = time / gameOverScreenTime;
            Time.timeScale = Mathf.Lerp(1, 0, t);
            audioManager.SetPitch(Mathf.Lerp(1, 0, t));
            gameOverImage.color = Color.Lerp(startColor, endColor, t);

            time += Time.fixedDeltaTime;
            yield return null;
        }

        Time.timeScale = 0;
        audioManager.StopMusic();
        gameOverImage.color = endColor;
        gameOverText.gameObject.SetActive(true);
        gameOverButton1.SetActive(true);
        gameOverButton2.SetActive(true);

    }

    private IEnumerator WinGame()
    {
        float time = 0;
        float t;
        Color startColor = fadeOutImage.color;
        Color endColor = startColor;
        endColor.a = 1;

        while (time < gameOverScreenTime)
        {
            t = time / gameOverScreenTime;
            hp.cutoffFrequency = Mathf.Lerp(10, 22000, t);
            fadeOutImage.color = Color.Lerp(startColor, endColor, t);

            time += Time.deltaTime;
            yield return null;
        }

        audioManager.StopMusic();
        fadeOutImage.color = endColor;
        SceneManager.LoadScene(winScene);
    }

    private IEnumerator TimeAddedLabel(float time)
    {
        GameObject timeAddedLabelGO = Instantiate(timeAddedLabel, timeAddedLabelGroup);
        

        float timer = 0;
        float t;
        TMP_Text timeAddedLabelText = timeAddedLabelGO.GetComponent<TMP_Text>();
        timeAddedLabelText.text = "+" + time.ToString();

        yield return new WaitForSeconds(timeAddedLabelTime);

        Color startColor = timeAddedLabelText.color;
        Color endColor = startColor;
        endColor.a = 0;

        while (timer < timeAddedLabelFadeTime)
        {
            t = timer / timeAddedLabelFadeTime;
            timeAddedLabelText.color = Color.Lerp(startColor, endColor, t);
            timer += Time.deltaTime;
            yield return null;
        }
        timeAddedLabelText.color = endColor;
        yield return new WaitForSeconds(0.01f);
        Destroy(timeAddedLabelGO);
    }

}
