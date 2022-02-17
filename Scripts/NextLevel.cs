using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI levelNumberText;

    Level level;
    Player player;
    GameStatus score;

    void Start()
    {
        level = FindObjectOfType<Level>();
        player = FindObjectOfType<Player>();
        score = FindObjectOfType<GameStatus>();
        levelNumberText.text = level.GetCurrentSceneName();
    }

    public void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex < 2)
            level.GetNextScene();
        else
            LoadGameOverScene();
    }

    public void LoadGameOverScene()
    {
        Destroy(player.gameObject);
        level.LoadGameOverScene();
    }

    public void LoadStartScene()
    {
        
        Destroy(player.gameObject);
        Destroy(score.gameObject);
        level.GetStartScene();
    }
}
