// QMUL Final Year Project 2018/19
// Programme of study: BSc(Hons) Computer Science with Business Management with Industrial Experience
// Project Title: Legends: A Quest to Uncover Female Protagonist Throughout History.
// Student Name: Laraib Azam Rajper
// Student ID: 150701938
// Supervisor: Graham White

// Class Discription: User Input manager, uses Controller class enables player movement
// Features : Moving Left and Right Jumping / Crouching / Events for setting up animation / 2D Physics 
// Source: Brackeys (2018)https://github.com/Brackeys/2D-Movement/blob/master/2D%20Movement/Assets/PlayerMovement.cs
// Source: Tutorial Used: https://www.youtube.com/watch?v=dwcT-Dch0bA&t=860s

using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;

    public Animator animator; 

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

   
    void Update(){
      
        // player moverment with keyboard keys - for testing purposes on computer
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        // adding animations to the players movement
        // NB: the name of the variables must equal the name of the parameters in the animator 

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove)); 

        if (Input.GetButtonDown("Jump")){
            jump = true;
            animator.SetBool("IsJumping", true);

        }
        // NB Crouch = Sliding in animations 

        if (Input.GetButtonDown("Crouch")){
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch")){
            crouch = false;
        }

        // attacking animation with button control 
        if (Input.GetButtonDown("Attack"))
        {
            animator.SetBool("IsAttacking", true);
        }
        else if (Input.GetButtonUp("Attack"))
        {
            animator.SetBool("IsAttacking", false);
        }


    }

  

    public void OnLanding(){
        // To stop jumping 
        animator.SetBool("IsJumping", false);
       // Debug.Log("in on landing event");

        if (animator.GetBool("IsJumping") == false){
           // Debug.Log("is landing");

        }
        else{
            //Debug.Log("is jumping = true");
        }
       
    }

    public void OnCrouching(bool IsCrouching){
        animator.SetBool("IsCrouching", IsCrouching);
    }

    void FixedUpdate(){

        // Move our characterbool 
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;

        
       
    }
}