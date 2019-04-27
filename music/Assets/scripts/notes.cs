using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class notes : MonoBehaviour
{
	public  AudioSource C;
	public  AudioSource Db;
	public  AudioSource D;
	public  AudioSource Eb;
	public  AudioSource E;
	public  AudioSource F;
	public  AudioSource Gb;
	public  AudioSource G;
	public  AudioSource Ab;
	public  AudioSource A;
	public  AudioSource Bb;
	public  AudioSource B;
	public AudioSource Audio;
	string lastChord;
	bool[] notesDown = new bool[]{false,false,false,false,false,false,false,false,false,false,false,false};// 0: C, 1: Db, 2: D, 3: Eb, 4: E, 5: F, 6: Gb, 7: G, 8: Ab, 9: A, 10: Bb, 11: B
	// Use this for initialization
	void Start ()
	{
		C = GameObject.Find ("C").GetComponent<AudioSource> ();
		Db = GameObject.Find ("Db").GetComponent<AudioSource> ();
		D = GameObject.Find ("D").GetComponent<AudioSource> ();
		Eb = GameObject.Find ("Eb").GetComponent<AudioSource> ();
		E = GameObject.Find ("E").GetComponent<AudioSource> ();
		F = GameObject.Find ("F").GetComponent<AudioSource> ();
		Gb = GameObject.Find ("Gb").GetComponent<AudioSource> ();
		G = GameObject.Find ("G").GetComponent<AudioSource> ();
		Ab = GameObject.Find ("Ab").GetComponent<AudioSource> ();
		A = GameObject.Find ("A").GetComponent<AudioSource> ();
		Bb = GameObject.Find ("Bb").GetComponent<AudioSource> ();
		B = GameObject.Find ("B").GetComponent<AudioSource> ();
		//playChord (CMajor);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log (notesDown.ToString ());
		soundOnMultiTouch ();
		if (checkChordAgainstPlayerInput (lastChord)) {
			Debug.Log ("correct");
		}
	}

	void soundOnMultiTouch ()
	{
		for (int i = 0; i < Input.touchCount; i++) {
			AudioSource note; 
			Touch currentTouch = Input.GetTouch (i);
			Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint (Input.GetTouch (i).position); 
			Vector2 touchRay2D = new Vector2 (touchWorldPos.x, touchWorldPos.y);
			RaycastHit2D hit = Physics2D.Raycast (touchRay2D, Vector2.zero);
			if (hit) {
				note = hit.collider.gameObject.GetComponent<AudioSource> ();
				if (currentTouch.phase == TouchPhase.Began) {
					playNote (note);
					toggleNoteDown (note);
					note.SetScheduledEndTime (AudioSettings.dspTime + 1.5f);
				} else if (currentTouch.phase != TouchPhase.Began && currentTouch.phase != TouchPhase.Ended) {
					note.SetScheduledEndTime (AudioSettings.dspTime + 1.5f);
				} else if (currentTouch.phase == TouchPhase.Ended && note.time > 1.5f) {
					playNote (note, false);
					toggleNoteDown (note);
				}
				//soundOnClick p = (soundOnClick)hit.collider.gameObject.GetComponent (typeof(soundOnClick));
				//p.playSelf ();
			}
		}
	}
	void toggleNoteDown(AudioSource note){// 0: C, 1: Db, 2: D, 3: Eb, 4: E, 5: F, 6: Gb, 7: G, 8: Ab, 9: A, 10: Bb, 11: B
		switch (note.name) {
		case "C":{
			notesDown[0] = ! notesDown[0];
				break;
			}
		case "Db":{
				notesDown[1] = ! notesDown[1];
				break;
			}
		case "D":{
				notesDown[2] = ! notesDown[2];
				break;
			}
		case "Eb":{
				notesDown[3] = ! notesDown[3];
				break;
			}
		case "E":{
				notesDown[4] = ! notesDown[4];
				break;
			}
		case "F":{
				notesDown[5] = ! notesDown[5];
				break;
			}
		case "Gb":{
				notesDown[6] = ! notesDown[6];
				break;
			}
		case "G":{
				notesDown[7] = ! notesDown[7];
				break;
			}
		case "Ab":{
				notesDown[8] = ! notesDown[8];
				break;
			}
		case "A":{
				notesDown[9] = ! notesDown[9];
				break;
			}
		case "Bb":{
				notesDown[10] = ! notesDown[10];
				break;
			}
		case "B":{
				notesDown[11] = ! notesDown[11];
				break;
			}
		}
	}
	void playNote (AudioSource note, bool play = true)
	{
		if (play) {
			note.Play ();
			Debug.Log (note);
			note.SetScheduledEndTime (AudioSettings.dspTime + 1.5f);
		} else {
			note.Stop ();
		}
	}
	bool checkChordAgainstPlayerInput(string chord){
		// 0= C, 1= Db, 2= D, 3= Eb, 4= E, 5= F, 6= Gb, 7= G, 8= Ab, 9= A, 10= Bb, 11= B
		bool res = false;
		int downCount = 0;
		foreach (bool n in notesDown) {
			if (n) {
				downCount += 1;
			}
		}
	//	if (downCount != 3) {
	//		return false;
	//	}
		switch (chord) {
		case "CMaj":
				if (notesDown [0] && notesDown [4] && notesDown [8]) {
					res = true;
				}//ceg
					break;
		case "FMaj":
				if (notesDown [5] && notesDown [9] && notesDown [0]) {
					res = true;
				}//fac
			break;
		case "GMaj":
				if (notesDown [7] && notesDown [11] && notesDown [2]) {
					res = true;
				}//gbd

			break;
		case "AMin":
				if (notesDown [9] && notesDown [0] && notesDown [4]) {
					res = true;
				}//ace
			break;

		case "BbMaj":
			if (notesDown [10] && notesDown [2] && notesDown [5]) {
				res = true;
			}//Bbdf
			break;
		case "DMin":
			if (notesDown [2] && notesDown [5] && notesDown [9]) {
				res = true;
			}//dfa
			break;
		case "DMaj":
			if (notesDown [2] && notesDown [6] && notesDown [9]) {
				res = true;
			}//dgba
			break;
		case "AMaj":
			if (notesDown [9] && notesDown [1] && notesDown [4]) {
				res = true;
			}//adbe
			break;
		case "EMin":
			if (notesDown [4] && notesDown [7] && notesDown [11]) {
				res = true;
			}//egb
			break;
		case "BMin":
			if (notesDown [11] && notesDown [2] && notesDown [6]) {
				res = true;
			}//bdgb must be higher
			break;
		case "EMaj":
			if (notesDown [4] && notesDown [8] && notesDown [6]) {
				res = true;
			} //eabb
			break;
		case "C#Min": if (notesDown [1] && notesDown [4] && notesDown [8]) {
				res = true;
			} //dbeab
			break;
		case "BMaj": if (notesDown [6] && notesDown [3] && notesDown [11]) {
				res = true;
			} //bebgb
			break;
		case "F#Min": if (notesDown [11] && notesDown [9] && notesDown [1]) {
				res = true;
			} //gbadb
			break;
		default:
			break;	
		}
	
		return false;
	}
	public void playChord (string chord)
	{ // for reference: https://www.piano-keyboard-guide.com/basic-piano-chords.html

		lastChord = chord;
		Debug.Log (lastChord);
		switch (chord) {
		case "CMaj":
			playNote (C);
			playNote (E);
			playNote (G);
			break;
		case "FMaj":
			playNote (F);
			playNote (A);
			playNote (C);
			break;
		case "GMaj":
			playNote (G);
			playNote (B);
			playNote (D);
			break;
		case "AMin":
			playNote (A);
			playNote (C);
			playNote (E);
			break;
		case "BbMaj":
			playNote (Bb);
			playNote (D);
			playNote (F);
			break;
		case "DMin":
			playNote (D);
			playNote (F);
			playNote (A);
			break;
		case "DMaj":
			playNote (D);
			playNote (Gb);
			playNote (A);
			break;
		case "AMaj":
			playNote (A);
			playNote (Db);
			playNote (E);
			break;
		case "EMin":
			playNote (E);
			playNote (G);
			playNote (B);
			break;
		case "BMin":
			playNote (B);
			playNote (D);
			playNote (Gb); //must be higher
			break;
		case "EMaj":
			playNote (E);
			playNote (Ab);
			playNote (B);
			break;
		case "C#Min":
			playNote (Db);
			playNote (E);
			playNote (Ab);
			break;
		case "BMaj":
			playNote (B);
			playNote (Eb);
			playNote (Gb);
			break;
		case "F#Min":
			playNote (Gb);
			playNote (A);
			playNote (Db);
			break;
		default:
			break;	
		}
	}
}
