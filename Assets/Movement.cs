using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public static int lowestSortLayer;

    private Collider2D colliderComp;
    void Awake()
    {
        transform.localEulerAngles = transform.eulerAngles.With(z: Random.Range(0f, 20f));
    }
    void Start()
    {
        GameManager.Instance.AddToList(this);
        colliderComp = GetComponent<Collider2D>();
        transform.Scale(1f, 1f);
        Wrj.Utils.MapToCurve.EaseIn.Scale(transform, Vector3.one, 15f);
        Wrj.Utils.MapToCurve.Ease.Move(transform, transform.localPosition.With(x: transform.localPosition.x * -1f, y: transform.localPosition.y * -1f), Random.Range(5f, 10f));
        Wrj.Utils.MapToCurve.Ease.Rotate(transform, transform.localEulerAngles.With(z: transform.localEulerAngles.z * -1f), Random.Range(.2f, .7f), pingPong: int.MaxValue);
        spriteRenderer.sortingOrder = lowestSortLayer--;
    }
    void Update()
    {
        CheckForTouch();
    }

    void Hit()
    {
        GameManager.AddToScore();
        GameObject explosion = Instantiate(GameManager.Instance.explosionPrototype.gameObject);
        explosion.transform.position = transform.position;
        GameManager.Instance.RemoveFromList(this);
        explosion.SetActive(true);
        GameObject.Destroy(this.gameObject);
    }

    void CheckForTouch()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

            var wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            var touchPosition = new Vector2(wp.x, wp.y);

            if (colliderComp == Physics2D.OverlapPoint(touchPosition))
            {
                Hit();
            }
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            var wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var touchPosition = new Vector2(wp.x, wp.y);

            if (colliderComp == Physics2D.OverlapPoint(touchPosition))
            {
                Hit();
            }
            return;
        }
    }
}
