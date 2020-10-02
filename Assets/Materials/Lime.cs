using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lime : MonoBehaviour
{
    [Header("Set in Inspector")]
    public static float bottomY = -9f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < bottomY)
        {
            Destroy(this.gameObject, 2f);

        }
    }
}
