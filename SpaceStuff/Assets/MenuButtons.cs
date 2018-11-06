using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour {

    public bool paused = false;
    GameObject Jukebox;
    GameObject PlayerPlane;
    void Start()
    {
        ActivateChildren();
        Jukebox = GameObject.Find("MainMenuSong");
        PlayerPlane = GameObject.Find("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
            
    }

    public void Pause() {
        paused = !paused;

        if (paused)
        {
            Time.timeScale = 0;
            Jukebox.GetComponent<AudioSource>().volume = 0.02f;
            PlayerPlane.GetComponent<PlaneController>().introSequence = 3f;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            Jukebox.GetComponent<AudioSource>().volume = 0.15f;
            PlayerPlane.GetComponent<PlaneController>().introSequence = -1f;
            
            Cursor.visible = false;
        }
        ActivateChildren();
    }

    public void Resume() {
        Pause();
    }
    public void Restart() {
        Pause();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    public void Options() { }

    public void ExitToAct() {
        Pause();
        UnityEngine.SceneManagement.SceneManager.LoadScene("ActSelection");
    }
    public void ExitToMenu() {
        Pause();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }


    bool activeChildren = true;
    void ActivateChildren()
    {
        activeChildren = !activeChildren;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(activeChildren);
        }
    }
}
