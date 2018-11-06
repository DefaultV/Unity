using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float power = 0.1f;
    public float duration = -0.4f;
    public float slowDown = 0;
    public bool sShake = false;
    public Transform camera;

    Vector3 startPos;
    float initDuration;


    void Start()
    {
        camera = Camera.main.transform;
        startPos = camera.localPosition;
        initDuration = duration;
    }

    void Update()
    {
        if (sShake)
        {
            duration += Time.deltaTime;
            if (duration >= 0)
            {
                camera.localPosition = startPos + Random.insideUnitSphere * power;
            }
            else
            {
                //sShake = false;
                //duration = initDuration;
                
            }
        }
        else
        {
            duration = -0.4f;
            camera.position = startPos;
        }
    }
}