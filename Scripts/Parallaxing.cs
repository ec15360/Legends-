// QMUL Final Year Project 2018/19
// Programme of study: BSc(Hons) Computer Science with Business Management with Industrial Experience
// Project Title: Legends: A Quest to Uncover Female Protagonist Throughout History.
// Student Name: Laraib Azam Rajper
// Student ID: 150701938
// Supervisor: Graham White

// Class Discription: Places selected elements at different depths, moves them in varying directions, to emulate 3D perspective view
// Features : Scrolling/ 3D background/forgrounds
// Source: Brackeys (2016)
// Source: Tutorial Used: https://www.youtube.com/watch?v=5E5_Fquw7BM&t=69s

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

    public Transform[] backgrounds;     // Arraylist of the Transfrom position of all back/foregrounds to be parallaxed. 
    public float[] scaleOfParallax;     // The proportion of the cameras movement to move the backgrounds by 
    public float smoothingValue = 1f;   // How smooth the parallax is going to be [must be > 0]

    private Transform cameraPos;        // Reference to the main camera's transform 
    private Vector3 previousCamPos;     // The position of the camera in the previous frame 

    // The Awake() called before Start(). 
    // Assning references between scripts and between objects
    void Awake(){

        // Set up camera reference
        cameraPos = Camera.main.transform;    

    }

    // The previous frame, had the current frames camera position 
    // Making sure parallaxed list is as long as backgrounds list 
    // Assinging the backgrounds z position to corresponding scaleOfParallax 
    void Start () {
        previousCamPos = cameraPos.position;
        scaleOfParallax = new float[backgrounds.Length];    

        for (int i = 0; i < backgrounds.Length; i++){
            scaleOfParallax[i] = backgrounds[i].position.z * -1;     

        }
	}
	

	void Update () {

        for (int i = 0; i < backgrounds.Length; i++){

            // Parallax effect =  opposite of the camera movement.
            // Due to the previous frame multipled by the scale 
            float parallax = (previousCamPos.x - cameraPos.position.x) * scaleOfParallax[i];

            // Setting a target X position, this is the current position plus the parallax level
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // Create a target position, which is the backgrounds current position with its target x position 
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // Fading effect between current position and target position using the lerp function
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothingValue * Time.deltaTime);

        }

        // Setting previousCamPos to main camera's position at the end of the frame 
        previousCamPos = cameraPos.position;
    }
}
