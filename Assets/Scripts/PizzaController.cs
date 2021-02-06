using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaController : MonoBehaviour
{
    [SerializeField]
    float PizzaTimer;
    float Timer;
    BoxCollider PizzaCollider;
    void SpawnPizza()
    {
        transform.position = GameManager.gameManagerInstance.mapController.AvailableTilePosition();
        PizzaCollider.enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }
    void Start()
    {
        Timer = PizzaTimer;
        PizzaCollider = GetComponent<BoxCollider>();
        SpawnPizza();    
    }
    void Update()
    {
        TimerClock();    
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
            Timer = PizzaTimer;
        }
        else
        {
            Timer -= Time.deltaTime;
        }
    }
}
