using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SnakeHeadController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.CompareTag("Wall"))
        {
            SnakeRecoilMovement(other.contacts[0].normal);
        }
        if(other.transform.CompareTag("Tile"))
        {
            SnakeWalkedOnTile(other.gameObject.name);
        }
        if(other.transform.CompareTag("Pizza"))
        {
            SnakeEatsPizza(other.transform);
        }
    }
    void SnakeRecoilMovement(Vector3 wallNormal)
    {
        Vector3 ReboundDirection = Vector3.Reflect(transform.forward, wallNormal);
        transform.rotation = Quaternion.LookRotation(ReboundDirection);
    }
    void SnakeWalkedOnTile(string val)
    {
        GameManager.gameManagerInstance.mapController.DisableTileAt(val);
    }
    void SnakeEatsPizza(Transform EatenPizza)
    {
        EatenPizza.GetComponent<PizzaController>().PizzaEaten();
    }
}
