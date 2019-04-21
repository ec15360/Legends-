// QMUL Final Year Project 2018/19
// Programme of study: BSc(Hons) Computer Science with Business Management with Industrial Experience
// Project Title: Legends: A Quest to Uncover Female Protagonist Throughout History.
// Student Name: Laraib Azam Rajper
// Student ID: 150701938
// Supervisor: Graham White

// Class Discription: Reusable method for loading a particular level on input, reusable method for higher performance

using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSelector : MonoBehaviour {

    public void LoadingScene(string scene)
    {
        SceneManager.LoadScene(scene);

    }
}
