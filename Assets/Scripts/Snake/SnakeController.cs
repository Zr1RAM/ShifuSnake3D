using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{

    public List<Transform> BodyParts;// = new List<Transform>();

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
        SpawnBodyParts();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public void Move()
    {
        BodyParts[0].Translate(BodyParts[0].forward * speed * Time.smoothDeltaTime, Space.World);
#if UNITY_EDITOR
        if (Input.GetAxis("Horizontal") != 0)
        {
            BodyParts[0].Rotate(Vector3.up * rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
        }
#endif
        if (GameManager.gameManagerInstance.GetSteeringWheelAxis() != 0)
        {
            BodyParts[0].Rotate(Vector3.up * rotationSpeed * Time.deltaTime * GameManager.gameManagerInstance.GetSteeringWheelAxis());
        }
        for (int i = 1; i < BodyParts.Count; i++)
        {
            CurrentBodyPart = BodyParts[i];
            PreviousBodyPart = BodyParts[i - 1];
            CurrentDistBetweenBodyParts = Vector3.Distance(PreviousBodyPart.position,CurrentBodyPart.position);
            Vector3 NewPositionForCurrentBodyPart = PreviousBodyPart.position;
            float Step = Time.deltaTime * CurrentDistBetweenBodyParts / BodyPartsMinDistance * speed;
            if (Step > BodyPartsMinDistance)
            {
                Step = BodyPartsMinDistance;
            }  
            CurrentBodyPart.position = Vector3.Slerp(CurrentBodyPart.position, NewPositionForCurrentBodyPart, Step);
            CurrentBodyPart.rotation = Quaternion.Slerp(CurrentBodyPart.rotation, PreviousBodyPart.rotation, Step);
        }
    }

    void SpawnBodyParts()
    {
        float SnakeHeadXpos, SnakeHeadZpos;
        SnakeHeadXpos = Random.Range(0,GameManager.gameManagerInstance.MapWidth) + 0.5f;
        SnakeHeadZpos = Random.Range(0, GameManager.gameManagerInstance.MapHeight) + 0.5f;
        BodyParts[0].position = new Vector3(SnakeHeadXpos, BodyParts[0].position.y, SnakeHeadZpos);
       
        for (int i = 0; i < SnakeSize - 1; i++)
        {
            Transform SpawnedBodypart = (Instantiate(BodyPrefab) as GameObject).transform;
            SpawnedBodypart.SetParent(transform,false);
            SpawnedBodypart.position = BodyParts[BodyParts.Count - 1].position;
            SpawnedBodypart.rotation = BodyParts[BodyParts.Count - 1].rotation;
            BodyParts.Add(SpawnedBodypart);
        }
    }
    void OnApplicationQuit()
    {
        BodyPrefab.transform.position = Vector3.zero;
        BodyPrefab.transform.rotation = Quaternion.identity;
    }

}

