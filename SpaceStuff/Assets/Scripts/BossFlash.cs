using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFlash : MonoBehaviour {

    public float delayTimer = 0f;
    public Color mainColor;
    public Color changeColor;
    public float colorChangeAlpha = 0f;
    public bool begin = false;
    public bool beginIntro = false;
    public bool beginOutro = false;

    void Start () {
        Image image = gameObject.GetComponent<Image>();
        changeColor = image.color;
        mainColor = image.color;
        introChange = image.color;
        gameObject.GetComponent<Image>().color = Color.black;
        Debug.Log(gameObject.GetComponent<Image>().color);
    }

    bool g = false;
    void beginFlash()
    {
        if (delayTimer <= 3f)
        {
            delayTimer += Time.deltaTime;
            colorChangeAlpha += Time.deltaTime * 0.05f;
        }
        else
        {
            if (delayTimer >= 3f)
            {
                if (!g)
                {
                    colorChangeAlpha = 1f;
                    g = true;
                }
                colorChangeAlpha -= Time.deltaTime / 2;
            }
        }
        changeColor.a = colorChangeAlpha;
        changeColor.r = 255;
        changeColor.g = 255;
        changeColor.b = 255;
        mainColor = changeColor;
        gameObject.GetComponent<Image>().color = mainColor;
        //OUTRO HERE
        if (beginOutro)
        {
            Debug.Log("Outro begin!");
            IntroTimer += Time.deltaTime / 2;
            introChange.r = 0;
            introChange.g = 0;
            introChange.b = 0;
            introChange.a = IntroTimer;
            Debug.Log(introChange.a);
            gameObject.GetComponent<Image>().color = introChange;
        }
    }
    float IntroTimer = 1f;
    Color introChange;
    /*void Outro()
    {
        if (!beginOutro)
            return;
        Debug.Log("Outro begin!");
        IntroTimer += Time.deltaTime / 2;
        introChange.r = 0;
        introChange.g = 0;
        introChange.b = 0;
        introChange.a = IntroTimer;
        Debug.Log(introChange.a);
        gameObject.GetComponent<Image>().color = introChange;
    }*/
    void Intro()
    {
        if (beginIntro)
            return;
        IntroTimer -= Time.deltaTime / 3;
        introChange.r = 0;
        introChange.g = 0;
        introChange.b = 0;
        introChange.a = IntroTimer;
        gameObject.GetComponent<Image>().color = introChange;

        if (IntroTimer <= 0f)
            beginIntro = true;
    }

	void Update () {
        Intro();
        if (!begin)
            return;
        beginFlash();
    }
}
