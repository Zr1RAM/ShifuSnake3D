// SnakeHeadController and SnakeController could have been ideally a single script that handles the snake behaviour
// But due to facing some issues with that approach of having the body parts as child object of the head, it has been broken
// into two Components.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SnakeHeadController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.CompareTag("Wall")) //When collision with wall.
        {
            SnakeWallRebound(other.contacts[0].normal);
        }
        if(other.transform.CompareTag("Tile")) //When collision with tile.
        {
            SnakeWalkedOnTile(other.gameObject.name);
        }
        if(other.transform.CompareTag("Pizza")) //When collision with pizza.
        {
            SnakeEatsPizza(other.transform);
        }
    }
    // Set snake head rotation to rebound angle.
    // For now the 90 degree angle rebounds are direct deflections which should have been worked on some more.
    // Other angle rebounds are not a problem.
    void SnakeWallRebound(Vector3 wallNormal)
    {
        Vector3 ReboundDirection = Vector3.Reflect(transform.forward, wallNormal);
        transform.rotation = Quaternion.LookRotation(ReboundDirection);
        GameManager.gameManagerInstance.PlayWallBounceSoundFromSoundManager();
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
