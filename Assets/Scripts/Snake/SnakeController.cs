using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{

    public List<Transform> BodyParts = new List<Transform>();

    public float BodyPartsMinDistance = 0.25f;

    public int SnakeSize;
    
    public float speed = 1;
    public float rotationSpeed = 50;

    public GameObject BodyPrefab;

    private float CurrentDistBetweenBodyParts;
    private Transform CurrentBodyPart;
    private Transform PreviousBodyPart;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < SnakeSize - 1; i++)
        {

            AddBodyPart();

        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetKey(KeyCode.Q))
            AddBodyPart();
    }

    public void Move()
    {

        float curspeed = speed;

        if (Input.GetKey(KeyCode.W))
            curspeed *= 2;

        BodyParts[0].Translate(BodyParts[0].forward * curspeed * Time.smoothDeltaTime, Space.World);

        if (Input.GetAxis("Horizontal") != 0)
            BodyParts[0].Rotate(Vector3.up * rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));

        for (int i = 1; i < BodyParts.Count; i++)
        {

            CurrentBodyPart = BodyParts[i];
            PreviousBodyPart = BodyParts[i - 1];

            CurrentDistBetweenBodyParts = Vector3.Distance(PreviousBodyPart.position,CurrentBodyPart.position);

            Vector3 newpos = PreviousBodyPart.position;

            newpos.y = 0;

            float T = Time.deltaTime * CurrentDistBetweenBodyParts / BodyPartsMinDistance * curspeed;

            if (T > 0.5f)
                T = 0.5f;
            CurrentBodyPart.position = Vector3.Slerp(CurrentBodyPart.position, newpos, T);
            CurrentBodyPart.rotation = Quaternion.Slerp(CurrentBodyPart.rotation, PreviousBodyPart.rotation, T);



        }
    }


    public void AddBodyPart()
    {

        Transform newpart = (Instantiate (BodyPrefab, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count - 1].rotation) as GameObject).transform;

        newpart.SetParent(transform);

        BodyParts.Add(newpart);
    }
    void OnApplicationQuit()
    {
        BodyPrefab.transform.position = Vector3.zero;
        BodyPrefab.transform.rotation = Quaternion.identity;//Quaternion.Euler(Vector3.zero);
    }

}

