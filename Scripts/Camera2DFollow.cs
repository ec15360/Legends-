// QMUL Final Year Project 2018/19
// Programme of study: BSc(Hons) Computer Science with Business Management with Industrial Experience
// Project Title: Legends: A Quest to Uncover Female Protagonist Throughout History.
// Student Name: Laraib Azam Rajper
// Student ID: 150701938
// Supervisor: Graham White

// Class Discription: Allows the camera to follow the player as they move, clamps the camera on player death 
// Source: Brackeys (2016)
// Source: Tutorial Used: https://www.youtube.com/watch?v=UbPiCgCkHTE&t=1085s

using UnityEngine;
using System.Collections;

public class Camera2DFollow : MonoBehaviour {

    public Transform cameraTarget;
    public float dampingVar = 1;
    public float lookingAheadValue = 3;
    public float yPositionRestrictionValue = -1;
    public float lookingAheadSnapbackSpeed = 0.5f;
    public float lookingAheadMovementThresholdValue = 0.1f;
  
    float offsetForZ;
    float nextTimeForSearching = 0;

    Vector3 lookingAheadPosition;
    Vector3 currentVelocityValue;
    Vector3 perviousTargetPosition;

    // This method is used for initialization of variables, called at the start of the scene on which the obj exists 
    void Start () {
        lastTargetPosition = cameraTarget.position;
        offsetForZ = (transform.position - cameraTarget.position).z;
        transform.parent = null;
    }
    
    // This methods is called once per frame, used primarily forupdating var values
    void Update () {

        if (cameraTarget == null) {
            FindPlayer ();
            return;
        }

        // The following code indicates that we should only update lookingAheadPosition if the player is accelerating or has changed direction
        float xMoveDelta = (cameraTarget.position - perviousTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookingAheadMovementThresholdValue;

        if (updateLookAheadTarget) {
            lookingAheadPosition = lookingAheadValue * Vector3.right * Mathf.Sign(xMoveDelta);
        } else {
            lookingAheadPosition = Vector3.MoveTowards(lookingAheadPosition, Vector3.zero, Time.deltaTime * lookingAheadSnapbackSpeed);    
        }
        
        Vector3 aheadTargetPos = cameraTarget.position + lookingAheadPosition + Vector3.forward * offsetForZ;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocityValue, dampingVar);

        // The next set of instructions clamsp the camera so that camera does not follow player on death
        newPos = new Vector3 (newPos.x, Mathf.Clamp (newPos.y, yPositionRestrictionValue, Mathf.Infinity), newPos.z);

        transform.position = newPos;

        perviousTargetPosition = cameraTarget.position;        
    }

    void FindPlayer () {
        if (nextTimeForSearching <= Time.time) {
            GameObject searchResult = GameObject.FindGameObjectWithTag ("Player");
            if (searchResult != null)
                cameraTarget = searchResult.transform;
            nextTimeForSearching = Time.time + 0.5f;
        }
    }
}
