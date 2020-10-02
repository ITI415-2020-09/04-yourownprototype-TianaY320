using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree2 : MonoBehaviour
{

    [Header("Set in Inspector")]
    public GameObject limePrefab;
    public float speed = 1f;
    public float leftAndRightEdge = 10f;
    public float chanceToChangeDirections = 0.1f;
    public float secondsBetweenLimeDrops = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DropLime", 2f);
    }

    void DropLime()
    {
        GameObject apple = Instantiate<GameObject>(limePrefab);
        apple.transform.position = transform.position;
        Invoke("DropLime", secondsBetweenLimeDrops);
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
        else if ((pos.x-25) > leftAndRightEdge)
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
