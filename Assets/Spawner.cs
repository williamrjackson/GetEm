using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawnObjects;

    private Coroutine spawnRoutine;
    void Start()
    {
        transform.position = transform.position.With(x: 0f, y: 0f);
        transform.localScale = new Vector3(GetScreenToWorldWidth, GetScreenToWorldHeight, 0f);
        GameManager.Instance.GameStart += StartSpawning;
        GameManager.Instance.GameOver += StopSpawning;
        StartSpawning();
    }

    public void StartSpawning()
    {
        spawnRoutine = StartCoroutine(SpawnOnIntervals());
    }
    public void StopSpawning()
    {
        StopCoroutine(spawnRoutine);
    }
    void AddSpawn()
    {
        GameObject newObject = Instantiate(spawnObjects.GetRandom());
        float posWidthRange = Mathf.Abs(transform.localScale.x * .4f);
        float posHeightRange = Mathf.Abs(transform.localScale.y * .4f);
        newObject.transform.position = Vector3.one.With(x: Random.Range(posWidthRange * -1f, posWidthRange), y: Random.Range(posHeightRange * -1f, posHeightRange));
        newObject.transform.localScale = Vector3.zero;
        newObject.SetActive(true);
    }

    private IEnumerator SpawnOnIntervals()
    {
        while(true)
        {
            AddSpawn();
            yield return new WaitForSeconds(Mathf.Max(.25f, 3f - (.05f * GameManager.score)));
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, transform.localScale);
    }

    public static float GetScreenToWorldHeight
    {
        get
        {
            Vector2 topRightCorner = new Vector2(1, 1);
            Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
            var height = edgeVector.y * 2;
            return height;
        }
    }
    public static float GetScreenToWorldWidth
    {
        get
        {
            Vector2 topRightCorner = new Vector2(1, 1);
            Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
            var width = edgeVector.x * 2;
            return width;
        }
    }
}
