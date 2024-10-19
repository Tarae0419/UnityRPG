using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyTime;

    void Start()
    {
        destroyTime = 3f;

        Destroy(gameObject, destroyTime);
    }

}
