using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaController : MonoBehaviour
{
    [SerializeField]
    float PizzaTimer;

    void SpawnPizza()
    {
        transform.position = GameManager.gameManagerInstance.mapController.AvailableTilePosition();
    }
}
