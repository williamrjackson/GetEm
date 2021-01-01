using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public Transform explosionParent;
    public Transform explosionSprite;
    // Start is called before the first frame update
    void Start()
    {
        explosionSprite.Alpha(0f, 1f);
        explosionParent.Scale(Vector3.one * 2f, 1f);
        float rotation = (Wrj.Utils.CoinFlip) ? -90f : 90f;
        explosionParent.LinearRotate(explosionSprite.localEulerAngles.With(z: rotation), 1f);
        Wrj.Utils.Delay(1.2f, () => Destroy(gameObject));
    }
}
