using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{

    [Header("Set in Inspector")]
    public GameObject prefabArrow;
    public float velocityMult = 8f;

    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject arrow;
    public bool aimingMode;
    private Rigidbody arrowRigidbody;

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
        arrowRigidbody = arrow.GetComponent<Rigidbody>();
        arrowRigidbody.isKinematic = true;
    }

    void Update()
    {
        if (!aimingMode) return;

        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 mouseDelta = mousePos3D - launchPos;
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        Vector3 arrPos = launchPos + mouseDelta;
        arrow.transform.position = arrPos;

        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            arrowRigidbody.isKinematic = false;
            arrowRigidbody.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = arrow;
            arrow = null;
        }
    }
}
