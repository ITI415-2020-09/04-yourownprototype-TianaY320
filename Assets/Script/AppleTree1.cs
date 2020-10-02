using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree1 : MonoBehaviour
{

    [Header("Set in Inspector")]
    public GameObject lemonPrefab;
    public float speed = 1f;
    public float leftAndRightEdge = 10f;
    public float chanceToChangeDirections = 0.1f;
    public float secondsBetweenLemonDrops = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DropLemon", 2f);
    }

    void DropLemon()
    {
        GameObject apple = Instantiate<GameObject>(lemonPrefab);
        apple.transform.position = transform.position;
        Invoke("DropLemon", secondsBetweenLemonDrops);
    }

    // Update is called once per frame
    void Update()
    {
         Vector3 pos = transform.position;
        pos.x +=speed * Time.deltaTime;
        transform.position = pos;

        if ((pos.x-10) < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed);
        }
        else if ((pos.x-20) > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed);
        }
        
    }

    void FixedUpdate()
    {
        if (Random.value < chanceToChangeDirections)
        {
            speed *= -1;
        }
    }
}
