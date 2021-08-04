using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Feading : MonoBehaviour
{
    private ARRaycastManager ARRaycastManagerScript;
    private Vector2 TouchPosition;
    public GameObject ObjectToSpawn;
    private Camera camera;
    public float addingForceToObject;

    void Start()
    {
        ARRaycastManagerScript = FindObjectOfType<ARRaycastManager>();
        camera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            var touch = Input.GetTouch(0);
            TouchPosition = touch.position;
            ARRaycastManagerScript.Raycast(TouchPosition, hits, TrackableType.Planes);

            var heading = hits[0].pose.position - camera.transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;

            var meat = Instantiate(ObjectToSpawn, camera.transform.position, ObjectToSpawn.transform.rotation);
            meat.GetComponent<Rigidbody>().AddForce(camera.transform.forward * addingForceToObject, ForceMode.Impulse);
        }
    }
}
