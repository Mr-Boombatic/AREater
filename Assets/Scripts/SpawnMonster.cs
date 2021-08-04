using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SpawnMonster : MonoBehaviour
{
    public GameObject Monster;
    private ARRaycastManager ARRaycastManagerScript;

    private bool IsSpawned;

    void Start()
    {
        ARRaycastManagerScript = FindObjectOfType<ARRaycastManager>();
        Monster.SetActive(false);
    }
    void Update()
    {
        SpawnOnPlace();
    }

    void SpawnOnPlace()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        ARRaycastManagerScript.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        if (hits.Count > 0 && !IsSpawned)
        {
            Monster.transform.position = hits[0].pose.position;
            Monster.SetActive(true);
            IsSpawned = !IsSpawned;
        }
    }
}
