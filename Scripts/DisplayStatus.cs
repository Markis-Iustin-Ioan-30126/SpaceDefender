using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DisplayStatus : MonoBehaviour
{

    TextMeshProUGUI text;
    GameStatus gameStatus;
    
    // Start is called before the first frame update
    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        text = GetComponent<TextMeshProUGUI>();
        text.text = gameStatus.GetScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = gameStatus.GetScore().ToString();
    }
}
