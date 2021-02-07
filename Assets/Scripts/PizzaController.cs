using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaController : MonoBehaviour
{
    [SerializeField]
    float PizzaTimer;
    [SerializeField]
    float PizzaCutOffTimer; //Timer value for when pizza is almost gone 
    [SerializeField]
    float Timer;
    BoxCollider PizzaCollider;
    Animator PizzaAnimator;
    public void SpawnPizza()
    {
        transform.position = GameManager.gameManagerInstance.GetAvailableTilePositionInMap();
        transform.localScale = Vector3.one;
        PizzaCollider.enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
        PizzaAnimator.SetTrigger("PizzaSpawned");
    }
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
            SpawnPizza();
            PizzaAnimator.SetTrigger("TimerOver");
            Timer = PizzaTimer;
        }
        else
        {
            Timer -= Time.deltaTime;
            PizzaTimeRunningOutSoon();
        }
    }
    public void OnPizzaTimerOver()
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
        if (Timer <= PizzaCutOffTimer)
        {
            transform.localScale = Vector3.one * (Timer / PizzaCutOffTimer);
        }
    }
}
