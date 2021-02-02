using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")
        {
            SnakeRecoilMovement();
        }
    }
    void SnakeRecoilMovement()
    {

    }
}
