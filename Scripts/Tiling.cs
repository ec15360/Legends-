// QMUL Final Year Project 2018/19
// Programme of study: BSc(Hons) Computer Science with Business Management with Industrial Experience
// Project Title: Legends: A Quest to Uncover Female Protagonist Throughout History.
// Student Name: Laraib Azam Rajper
// Student ID: 150701938
// Supervisor: Graham White

// Class Discription: Takes an element, check if camera can see element, check if there is a buddy to right or left, if no adds buddy
// Features: Ensures that background elements are spawned in camera can see them, reduces redundant elements exsisting 
// Source: Brackeys (2016)
// Source: Tutorial Used: https://www.youtube.com/watch?v=CwGjwnjmg2w&t=402s

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour
{

    public int offsetXPos = 2;           // Introducing a offset between elements fo no 2 buddies overlap
    public bool rightBuddyExists = false;
    public bool leftBuddyExists = false;
    public Transform parentObjects;
    public bool reverseScale = false;   // Reverse vaiable added for non-tilable objects
    private float spriteWidth = 0f;     // Width of element being cloned
    private Camera camera;
    private Transform playerTransform;

    void Awake()
    {
        camera = Camera.main;
        playerTransform = transform;
    }

    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;

    }

    void Update()
    {
        // Does the element need a buddy next to it? If not, do nothing
        if (leftBuddyExists == false || rightBuddyExists == false)
        {
            // Calculating the cameras extend 
            // Extend = half the width of what the camera can see in world coordinates
            float camHorizontalExtend = camera.orthographicSize * Screen.width / Screen.height;

            // Calculate the X position of where the camera can detect the edge of the element 
            float edgeVisiblePositionRight = (playerTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisiblePositionLeft = (playerTransform.position.x - spriteWidth / 2) + camHorizontalExtend;

            // Check to see if camera can see edge of element, If yes, call MakeNewBuddy function
            if (camera.transform.position.x >= edgeVisiblePositionRight - offsetXPos && rightBuddyExists == false)
            {
                MakeNewBuddy(1);
                rightBuddyExists = true;
            }
            else if (camera.transform.position.x <= edgeVisiblePositionLeft + offsetXPos && leftBuddyExists == false)
            {
                MakeNewBuddy(-1);
                leftBuddyExists = true;
            }
        }
    }

    // Function renderes a buddy for the element on the side required. 
    void MakeNewBuddy(int rightOrLeft)
    {
        // Calculating placement psosition for new buddy 
        Vector3 newPosition = new Vector3(playerTransform.position.x + spriteWidth * rightOrLeft, playerTransform.position.y, playerTransform.position.z);

        // Instantating new buddy element then storing it in a variable called newBuddy
        Transform newBuddy = Instantiate(playerTransform, newPosition, playerTransform.rotation) as Transform;

        // Reversing original element, if not tileable, taking away mismatched seems between elements
        if (reverseScale == true)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
        }

        newBuddy.transform.parent = parentObjects.transform;
        if (rightOrLeft > 0)
        {
            newBuddy.GetComponent<Tiling>().leftBuddyExists = true;
        }
        else
        {
            newBuddy.GetComponent<Tiling>().rightBuddyExists = true;
        }
    }
}
