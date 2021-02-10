// Class that handles pizza spawning and other pizza related scenarios.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaController : MonoBehaviour
{
    [SerializeField]
    float PizzaTimer; // Timer before Pizza respawns in different location.
    [SerializeField]
    float PizzaCutOffTimer; //Timer value for when pizza is almost gone. (This is currently not working)
    [SerializeField]
    float Timer; //Current value of timer.
    BoxCollider PizzaCollider;
    Animator PizzaAnimator;
    // Function to spawn pizza.
    public void SpawnPizza()
    {
        // Get random Position from tiles not touched by the snake.
        transform.position = GameManager.gameManagerInstance.GetAvailableTilePositionInMap(); 
        transform.localScale = Vector3.one;
        PizzaCollider.enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
        PizzaAnimator.SetTrigger("PizzaSpawned");
    }
    // Initializing PizzaController 
    void Start()
    {
        PizzaAnimator = GetComponent<Animator>();
        PizzaCollider = GetComponent<BoxCollider>();
        //SpawnPizza(); // Should be called in Game start event
        Timer = PizzaTimer;
    }
    void Update()
    {
        if(GameManager.gameManagerInstance.isPlaying)
        {
            TimerClock();
        }    
    }
    // Called when pizza is eaten by snake.
    public void PizzaEaten()
    {
        GameManager.gameManagerInstance.PlayerScored();
        transform.GetChild(0).gameObject.SetActive(false);
        PizzaCollider.enabled = false;
        SpawnPizza();
    }
    void TimerClock()
    {
        if(Timer <= 0)
        {
            RespawnPizzaOnTimerOver();
        }
        else
        {
            Timer -= Time.deltaTime;
            PizzaTimeRunningOutSoon();
        }
    }
    public void OnPizzaTimerOver() // Animation event.. work in progress
    {
        print("called on animation over?");
        SpawnPizza();
        Timer = PizzaTimer;
    }
    public void ResetTimerClock()
    {
        Timer = PizzaTimer;
    }
    void PizzaTimeRunningOutSoon()
    {
        if (Timer < PizzaCutOffTimer)
        {
            transform.localScale = Vector3.one * (Timer / PizzaCutOffTimer);
        }
    }
    void RespawnPizzaOnTimerOver()
    {
        SpawnPizza();
        PizzaAnimator.SetTrigger("TimerOver");
        Timer = PizzaTimer;
        GameManager.gameManagerInstance.PlayPizzaLostSoundFromSoundManager();
    }
}
