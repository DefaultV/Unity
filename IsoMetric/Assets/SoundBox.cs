using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBox : MonoBehaviour {
    public AudioClip selectMove;
    public AudioClip selectImpact;
    public AudioClip selectImpact_R;
    public AudioClip SwitchWood;
    public AudioClip Open;
    public AudioClip GlassHit;
    public AudioClip Potion;
    public AudioClip Hit;
    public AudioClip Miss;
    public AudioClip SpellBuff;

    void Start () {
		
	}
	
	
	void Update () {
		
	}

    public void play(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
