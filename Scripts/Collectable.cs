// QMUL Final Year Project 2018/19
// Programme of study: BSc(Hons) Computer Science with Business Management with Industrial Experience
// Project Title: Legends: A Quest to Uncover Female Protagonist Throughout History.
// Student Name: Laraib Azam Rajper
// Student ID: 150701938
// Supervisor: Graham White

// Class Discription: Class interactes with game master to add points to players current score
// Features: Placed on coin object, add points to player obj on collision

using UnityEngine;

public class Collectable : MonoBehaviour {

    public int value;
	  
    void OnTriggerEnter2D(Collider2D other)
    {
        // Collect Function 
        GameMaster.gm.Collect(value,gameObject);

        // Play Sound 
        AudioSource source = GetComponent<AudioSource>();
        source.Play();

       
    }
}
