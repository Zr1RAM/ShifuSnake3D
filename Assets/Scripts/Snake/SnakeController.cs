// SnakeController and SnakeHeadController could have been ideally a single script that handles the snake behaviour
// But due to some issues with that approach of having the body parts as child object of the head, it has been broken
// into two Components.

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SnakeController : MonoBehaviour
{

    public List<Transform> BodyParts;// List of Body trails that will follow the snake.

    public float BodyPartsMinDistance = 0.25f; // Minimum distance between each body part / bone.

    public int SnakeSize; // Number of snake segments.
    
    public float Speed = 1; // Speed of snake Note : there is already a parameter in game manager which I forgot to remove.
    public float RotationSpeed = 50; // 

    [SerializeField]
    GameObject BodyPrefab;

    float CurrentDistBetweenBodyParts;
    Transform CurrentBodyPart;
    Transform PreviousBodyPart;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.gameManagerInstance.PlayerObject = BodyParts[0].gameObject; // Set reference to player object if ever needed to be accessed by other components. In the end its reference was not needed.
        //SpawnBodyParts(); //Should be called in Game start event
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameManagerInstance.isPlaying)
        {
            Move();
        }
    }
    // Used to move the head and the body parts of the snake.
    public void Move()
    {
        BodyParts[0].Translate(BodyParts[0].forward * Speed * Time.deltaTime, Space.World);
#if UNITY_EDITOR
        if (Input.GetAxis("Horizontal") != 0)
        {
            BodyParts[0].Rotate(Vector3.up * RotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal")); // For testing in editor without using the steering wheel to turn.
        }
#endif
        if (GameManager.gameManagerInstance.GetSteeringWheelAxis() != 0)
        {
            BodyParts[0].Rotate(Vector3.up * RotationSpeed * Time.deltaTime * GameManager.gameManagerInstance.GetSteeringWheelAxis());
        }
        for (int i = 1; i < BodyParts.Count; i++)
        {
            CurrentBodyPart = BodyParts[i];
            PreviousBodyPart = BodyParts[i - 1];
            CurrentDistBetweenBodyParts = Vector3.Distance(PreviousBodyPart.position, CurrentBodyPart.position);
            Vector3 NewPositionForCurrentBodyPart = PreviousBodyPart.position;
            float Step = Time.deltaTime * CurrentDistBetweenBodyParts / BodyPartsMinDistance * Speed;
            if (Step > BodyPartsMinDistance)
            {
                Step = BodyPartsMinDistance;
            }
                //if(Vector3.Distance(CurrentBodyPart.position,PreviousBodyPart.position) > BodyPartsMinDistance)
                //{
                //    CurrentBodyPart.position = Vector3.Slerp(CurrentBodyPart.position, NewPositionForCurrentBodyPart, Step);
                //    CurrentBodyPart.rotation = Quaternion.Slerp(CurrentBodyPart.rotation, PreviousBodyPart.rotation, Step);
                //}
             CurrentBodyPart.position = Vector3.Slerp(CurrentBodyPart.position, NewPositionForCurrentBodyPart, Step);
             CurrentBodyPart.rotation = Quaternion.Slerp(CurrentBodyPart.rotation, PreviousBodyPart.rotation, Step);
        }
    }
    // Used to spawn and respawwn Snake.
    public void SpawnBodyParts() 
    {
        float SnakeHeadXpos, SnakeHeadZpos;
        SnakeHeadXpos = Random.Range(0,GameManager.gameManagerInstance.MapWidth) + 0.5f;
        SnakeHeadZpos = Random.Range(0, GameManager.gameManagerInstance.MapHeight) + 0.5f;
        BodyParts[0].position = new Vector3(SnakeHeadXpos, BodyParts[0].position.y, SnakeHeadZpos);
        if(BodyParts.Count != SnakeSize)
        {
            for (int i = 0; i < SnakeSize - 1; i++)
            {
                Transform SpawnedBodyPart = (Instantiate(BodyPrefab) as GameObject).transform;
                //SpawnedBodyPart.GetComponent<MeshRenderer>().material = BodyParts[0].GetComponent<MeshRenderer>().material;
                SpawnedBodyPart.SetParent(transform, false);
                SpawnedBodyPart.position = BodyParts[BodyParts.Count - 1].position;
                SpawnedBodyPart.rotation = BodyParts[BodyParts.Count - 1].rotation;
                BodyParts.Add(SpawnedBodyPart);
            }
        }
        else
        {
            for(int i = 1; i < SnakeSize; i++)
            {
                BodyParts[i].position = BodyParts[i - 1].position;
                BodyParts[i].rotation = BodyParts[i - 1].rotation;
            }
        }
    }
    // Used to reset the prefab transform as the transform values seems to get modified. Not sure if it is an engine bug or if this is how prefabs work now.
    void OnApplicationQuit()
    {
        BodyPrefab.transform.position = Vector3.zero;
        BodyPrefab.transform.rotation = Quaternion.identity;
    }

}

