// QMUL Final Year Project 2018/19
// Programme of study: BSc(Hons) Computer Science with Business Management with Industrial Experience
// Project Title: Legends: A Quest to Uncover Female Protagonist Throughout History.
// Student Name: Laraib Azam Rajper
// Student ID: 150701938
// Supervisor: Graham White

// Class Discription: Class controls Main and Options Menu for application
// Features: Quit, Play, Pause and Restart Game. Adjust Graphics Quality, Volume, Full Screen, Resolution 
// Source: Brackeys (2017)
// Source: Tutorial Used: https://www.youtube.com/watch?v=YOaYQrN1oYQ

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menus : MonoBehaviour {

    public AudioMixer audioMixer;
    public GameObject mainMenu;
    Resolution[] screenResolutions;
    public TMPro.TMP_Dropdown resolutionsDropdown;
    public bool pause;

    void Start()
    {
        pause = false;
        screenResolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();

        // making resolutions strings to pass to UI element
        List<string> resOptions = new List<string>();

        int currentResIndex = 0; 

        for (int i=0; i<screenResolutions.Length; i++ )
        {
            string option = screenResolutions[i].width + " x " + screenResolutions[i].height;
            resOptions.Add(option);

            if(screenResolutions[i].width == Screen.currentResolution.width &&
            screenResolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i; 
            }
        }

        resolutionsDropdown.AddOptions(resOptions);
        resolutionsDropdown.value = currentResIndex;
        resolutionsDropdown.RefreshShownValue(); //to display value

    }

    public void SetResolution(int resIndex)
    {
        Resolution resolution = screenResolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // To load the next scene - level one of the game 
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameMaster.gm.gameOverUI.SetActive(false);
        Debug.Log("in restart");

    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
        // Play Sound 
        AudioSource source = GetComponent<AudioSource>();
        source.Play();
    }

    // To enable player to quit the game 
    public void QuitGame() 
    {
        Application.Quit(); 
       
        // Play Sound 
        AudioSource source = GetComponent<AudioSource>();
        source.Play();
    }

    public void Paused()
    {
        pause = !pause;
        if(!pause)
        {
            Time.timeScale = 1;
        }
        else if(pause){
            Time.timeScale = 0;
        }
        // Play Sound 
        AudioSource source = GetComponent<AudioSource>();
        source.Play();
    }

    // To enable the player to increase/decrease the volume 
    public void SetVolume(float volume) 
    {
        audioMixer.SetFloat("volume",volume);
    }

    // To enable the user to see the game in full screen on the device
    public void SetFullScreen(bool isFullScreen) 
    {
        Screen.fullScreen = isFullScreen;
    }

    // method used for adjusting quality setting of graphics
    public void SetGraphics(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }


}
