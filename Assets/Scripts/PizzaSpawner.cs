using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    float PizzaTimer;
    List<int> PizzaSpawnLocationIndex;
    void InitialisePizzaSpawner(float pizzaTimer,int FreeTilesNum) {
        PizzaSpawnLocationIndex = new List<int>();
        PizzaTimer = pizzaTimer;
        for(int i = 0 ; i < FreeTilesNum ; i++)
        {
            PizzaSpawnLocationIndex.Add(i);
        }
    }
    void SpawnPizza()
    {

    }
    void PizzaEaten()
    {

    }
    void Timer()
    {
        if(PizzaTimer <= 0)
        {
            SpawnPizza();
        }
        else
        {
            PizzaTimer -= Time.deltaTime;
        }
    }
    int RandomSpawnLocationIndex()
    {
        return PizzaSpawnLocationIndex[Random.Range(0,PizzaSpawnLocationIndex.Count)];
    }
}
