using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarDisplay : MonoBehaviour
{

    [SerializeField]  Slider slider;
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        slider.maxValue = player.GetPlayerMaxHealth();
        slider.value = player.GetPlayerCurrentHealth();
    }

}
