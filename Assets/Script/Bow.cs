using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{

    [Header("Set in Inspector")]
    public GameObject prefabArrow;

    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject arrow;
    public bool aimingMode;

    void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }

    void OnMouseEnter()
    {
        //print("Bow:OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        //print("Bow:OnMouseExit()");
        launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {
        aimingMode = true;
        arrow = Instantiate(prefabArrow) as GameObject;
        arrow.transform.position = launchPos;
        arrow.GetComponent<Rigidbody>().isKinematic = true;
    }
}
