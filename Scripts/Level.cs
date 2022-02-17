using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float deathTimeDelay = 1.5f;

    int currentSceneIndex;
    int sceneIndex;

    GameStatus gameStatus;

    private void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
    }

    public void GetNextScene()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex + 1);
    }

    public void GetStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(LoadGameCoroutine());
    }

    public IEnumerator LoadGameCoroutine()
    {
        yield return new WaitForSeconds(deathTimeDelay);
        SceneManager.LoadScene("Game Over");
    }

    public void LoadRetryScene()
    {
        StartCoroutine(LoadRetryCouroutine());
    }

    public void RetryLevel()
    {
        currentSceneIndex = gameStatus.GetCurrentLevel();
        gameStatus.RestartLevelScore();
        if (currentSceneIndex > 1)
            gameStatus.SetBool();
        SceneManager.LoadScene(currentSceneIndex);
    }

    public IEnumerator LoadRetryCouroutine()
    {
        yield return new WaitForSeconds(deathTimeDelay);
        SceneManager.LoadScene("Retry Scene");
    }

    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void ResetScore()
    {
        FindObjectOfType<GameStatus>().ResetGame();
    }
}
