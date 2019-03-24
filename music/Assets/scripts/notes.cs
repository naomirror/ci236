using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notes : MonoBehaviour {
	public  AudioClip C;
	public  AudioClip Df;
	public  AudioClip D;
	public  AudioClip Ef;
	public  AudioClip E;
	public  AudioClip F;
	public  AudioClip Gf;
	public  AudioClip G;
	public  AudioClip Af;
	public  AudioClip A;
	public  AudioClip Bf;
	public  AudioClip B;
	public AudioSource Audio;

	string[] CMajor = { "C", "E", "F" };
	// Use this for initialization
	void Start () {
		playChord (CMajor);
	}
	
	// Update is called once per frame
	void Update () {
		soundOnMultiTouch ();
	}

	void soundOnMultiTouch() {
		for(int i = 0; i < Input.touchCount; i++)
		{
			if (Input.GetTouch (i).phase == TouchPhase.Began) {
				Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint (Input.GetTouch (i).position); 
				Vector2 touchRay2D = new Vector2 (touchWorldPos.x, touchWorldPos.y);
				RaycastHit2D hit = Physics2D.Raycast (touchRay2D, Vector2.zero);
				if (hit) {
					string note = hit.collider.gameObject.name;
					playNote (note);
					//soundOnClick p = (soundOnClick)hit.collider.gameObject.GetComponent (typeof(soundOnClick));
					//p.playSelf ();
				}
			}
		}
	}

	void playNote(string note){
		Debug.Log (note);
		switch (note) {
		case "C": 
			Audio.PlayOneShot (C);
			Audio.SetScheduledEndTime(AudioSettings.dspTime + 1.50f);
			break;
		case "D": 
			Audio.PlayOneShot (D);
			Audio.SetScheduledEndTime(AudioSettings.dspTime + 1.50f);
			break;
		case "Db": 
			Audio.PlayOneShot (Df);
			Audio.SetScheduledEndTime(AudioSettings.dspTime + 1.50f);
			break;
		case "E": 
			Audio.PlayOneShot (E);
			Audio.SetScheduledEndTime(AudioSettings.dspTime + 1.50f);
			break;
		case "F": 
			Audio.PlayOneShot (F);
			Audio.SetScheduledEndTime(AudioSettings.dspTime + 1.50f);
			break;
		case "Gb": 
			Audio.PlayOneShot (Gf);
			Audio.SetScheduledEndTime(AudioSettings.dspTime + 1.50f);
			break;
		case "G": 
			Audio.PlayOneShot (G);
			Audio.SetScheduledEndTime(AudioSettings.dspTime + 1.50f);
			break;
		case "Ab": 
			Audio.PlayOneShot (Af);
			Audio.SetScheduledEndTime(AudioSettings.dspTime + 1.50f);
			break;
		case "A": 
			Audio.PlayOneShot (A);
			Audio.SetScheduledEndTime(AudioSettings.dspTime + 1.50f);
			break;
		case "Bb": 
			Audio.PlayOneShot (Bf);
			Audio.SetScheduledEndTime(AudioSettings.dspTime + 1.50f);
			break;
		case "B": 
			Audio.PlayOneShot (B);
			Audio.SetScheduledEndTime(AudioSettings.dspTime + 1.50f);
			break;
		}
	}

	void playChord(string[] notes){
		foreach (string note in notes){
			playNote (note);
		}
	}
}
