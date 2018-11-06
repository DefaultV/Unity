using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
    Button btn;
    // Use this for initialization
    void Start () {
        Cursor.visible = true;
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(OnStart);
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnStart()
    {
        switch (btn.name)
        {
            case "StartButton":
                SceneManager.LoadScene("ActSelection");
                break;
            case "Act1Button":
                SceneManager.LoadScene("Scene111");
                break;

            case "BackButton":
                SceneManager.LoadScene("MainMenu");
                break;
            case "ExitButton":
                Application.Quit();
                break;
        }     
    }
}
