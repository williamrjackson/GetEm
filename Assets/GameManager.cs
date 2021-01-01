using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UnityEngine.UI.Text scoreReadout;
    public Explode explosionPrototype;
    public DangerIndicator dangerIndicator;

    public UnityAction GameOver;
    public UnityAction GameStart;

    private List<Movement> badGuysList = new List<Movement>();

    public static GameManager Instance;
    private static bool _gameOver;
    private bool gameOver
    {
        set
        {
            _gameOver = value;
            if (value)
            {
                if (GameOver != null) GameOver();
                foreach (Movement item in badGuysList)
                {
                    Destroy(item.gameObject);
                }
                badGuysList.Clear();
            }
            else
            {
                if (GameStart != null) GameStart();
            }
        }
    }
    public static bool isGameOver { get { return _gameOver; } }

    private static int _score = 0;
    public static int score
    {
        get
        {
            return _score;
        }
        private set
        {
            _score = value;
            Instance.scoreReadout.text = "Score: " + _score;
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        StartCoroutine(CheckDangerRoutine());
    }

    public void AddToList(Movement movement)
    {
        badGuysList.Add(movement);
    }
    public void RemoveFromList(Movement movement)
    {
        badGuysList.Remove(movement);
    }

    private IEnumerator CheckDangerRoutine()
    {
        while (true)
        {
            float biggest = 0f;
            foreach (Movement badGuy in badGuysList)
            {
                biggest = Mathf.Max(badGuy.transform.localScale.x, biggest);
            }
            if (biggest > .5f)
            {
                dangerIndicator.SetDangerValue(1f);
                dangerIndicator.ShowGameOver();
                gameOver = true;
                StopCoroutine(CheckDangerRoutine());
                break;
            }
            dangerIndicator.SetDangerValue(biggest);
            yield return new WaitForSeconds(.5f);
        }
    }

    public void Restart()
    {
        gameOver = false;
        dangerIndicator.HideGameOver();
        dangerIndicator.SetDangerValue(0f);
        ResetScore();
        StartCoroutine(CheckDangerRoutine());
    }

    private void ResetScore()
    {
        score = 0;
    }
    public static void AddToScore()
    {
        score++;
    }

}
