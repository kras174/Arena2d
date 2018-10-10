using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject gameOverPanel;

    private float searchCountdowm = 1f;

    private void Awake()
    {
        ScoreUI.score = 0;
    }

    private void Update()
    {
        if (!PlayerIsAlive())
        {
            GameOver();
        }
        else
        {
            return;
        }
    }

    bool PlayerIsAlive()
    {
        searchCountdowm -= Time.deltaTime;
        if (searchCountdowm <= 0)
        {
            searchCountdowm = 1f;
            if (GameObject.FindGameObjectWithTag("Player") == null)
            {
                return false;
            }
        }

        return true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
        if (pauseButton) pauseButton.SetActive(true);
        if (pausePanel) pausePanel.SetActive(false);
        if (gameOverPanel) gameOverPanel.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Debug.Log("EXIT GAME");
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        pausePanel.SetActive(true);
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
    }
    public void GameOver()
    {
        pauseButton.SetActive(false);
        gameOverPanel.SetActive(true);
    }
}
