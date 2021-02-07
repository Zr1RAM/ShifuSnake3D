using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text ScoreText;
    Animator CanvasAnimator;
    [SerializeField]
    UnityEvent GameOverEvent, StartGameEvent;
    public void InitializeUIManager()
    {
        CanvasAnimator = transform.GetComponent<Animator>();
    }
    public void UpdateScore(int Score)
    {
        ScoreText.text = " " + Score;
        CanvasAnimator.SetTrigger("Score");
    }
    public void InvokeGameOverEvent()
    {
        if(GameOverEvent != null)
        {
            GameOverEvent.Invoke();
        }
    }
    public void InvokeStartGameEvent()
    {
        if (StartGameEvent != null)
        {
            StartGameEvent.Invoke();
        }
    }

}
