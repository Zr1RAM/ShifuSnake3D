using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    // Start is called before the first frame update
    public void MapGeneration(int width,int height)
    {
        this.transform.localScale = new Vector3(width,this.transform.localScale.y,height);
    }
    void FillTraversedTile()
    {

    }
}
