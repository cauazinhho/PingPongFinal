using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public BallMoviment bM;

    public GameObject points;
    public GameObject pauseMenu;
    public GameObject mainMenu;

    public TextMeshProUGUI countText;
    public GameObject countTextObj;
    RectTransform textRectTransform;

    public bool isPlaying;
    bool isPaused;

    float textPosY = 0;
    int time = 5;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        textRectTransform = countTextObj.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (isPlaying && !isPaused)
            {
                pauseMenu.SetActive(true);
                isPaused = true;
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
            }
            else if (isPlaying && isPaused)
            {
                Resume();
            }
        }

        
        textRectTransform.localPosition = new Vector3(0, textPosY, 0);
    }

    public void Resume()
    {
        pauseMenu?.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        StartCoroutine(WaitToStart());
    }

    IEnumerator WaitToStart()
    {
        bool alreadyDone = false;
        for (int i = time; i >= 0; i--)
        {
            if (i > 0)
                countText.text = i.ToString();
            else
                countText.text = "Já";

            if (i <= 2 && !alreadyDone)
            {
                StartCoroutine(CountAnimator());
                alreadyDone = true;
            }

            yield return new WaitForSeconds(1f);
        }

        countText.text = "";
        points.SetActive(true);
        bM.LaunchBall();
        textPosY = 0f;
        isPlaying = true;
        //ballScript.StartGame();
    }

    IEnumerator CountAnimator()
    {
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            textPosY = Mathf.Lerp(0, 245, elapsed / duration);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}