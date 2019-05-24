﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//https://answers.unity.com/questions/296347/move-transform-to-target-in-x-seconds.html
public class chordProgression : MonoBehaviour {
	Text accuracyText;
	public Text scores;
	accuracy level;
	float levelOneAccuracy = 0.0f;
	float levelTwoAccuracy = 0.0f;
	float levelThreeAccuracy = 0.0f;
	float levelFourAccuracy = 0.0f;
	float averageAccuracy;
	public Transform startPos;
	public Transform endPos;
	public Button Cmaj;
	public Button Fmaj;
	public Button Gmaj;
	public Button AMin;
	GameObject th;

	bool c1 = false;
	bool c2 = false;
	bool c3 = false;
	bool c4 = false;

	//ivIvIV wiki 
	string[] one = {"CMaj", "GMaj", "AMin", "FMaj" };
	string[] two = {"GMaj", "AMin", "FMaj", "CMaj" };
	string[] three = {"AMin", "FMaj", "CMaj", "GMaj" };
	string[] four = {"FMaj", "CMaj", "GMaj", "AMin" };

	float t = 0;
	int sel = 1;

	Vector3[] cBounds = new Vector3[4];
	Vector3[] gBounds = new Vector3[4];
	Vector3[] fBounds = new Vector3[4];
	Vector3[] aBounds = new Vector3[4];

	GameObject button;
	// Use this for initialization
	void Start () {
		arrangementSelector (sel);
		th = GameObject.Find("TouchHandler");
		accuracyText = GameObject.Find ("Accuracy").GetComponent<Text>();
		level = accuracyText.GetComponent<accuracy> ();
		accuracyText.text = "Accuracy: " + level.calculatedAccuracy ();
		button = GameObject.Find ("Button");
		button.SetActive (false);
	}
	bool sound = false;
	// Update is called once per frame
	void Update () {
		Debug.Log ("acc" +level.calculatedAccuracy ());
		Debug.Log ("hit"+level.notesHit);
		Debug.Log ("totes"+level.notesTotal);
		t += Time.deltaTime / 16.0f;
		transform.position = Vector3.Lerp (startPos.position, endPos.position, t);
		if (transform.position == endPos.position) {
			if (th.GetComponent<notes> ().cmajDone && th.GetComponent<notes> ().gmajDone && th.GetComponent<notes> ().fmajDone && th.GetComponent<notes> ().aminDone) {
				th.GetComponent<notes> ().resetBools ();
				updateAccuracy (sel);
				sel += 1;
				Debug.Log ("changing");
				arrangementSelector (sel);
				accuracyText.text = "Accuracy " + level.calculatedAccuracy ();
			} else {
				th.GetComponent<notes> ().cmajDone = false;
				th.GetComponent<notes> ().gmajDone = false;
				th.GetComponent<notes> ().fmajDone = false;
				th.GetComponent<notes> ().aminDone = false;
			}
			c1 = false;
			c2 = false;
			c3 = false;
			c4 = false;
			t = 0;
			transform.position = startPos.position;
		}
		if (c1 == false && (int)transform.position.x >= (int)(cBounds [2]).x) {
			c1 = true;
			Debug.Log("working");
			if (th.GetComponent<notes> ().cmajDone) {
				Debug.Log("working");
				level.notesHit += 1;
			} 
				level.notesTotal += 1;
			accuracyText.text = "Accuracy: " + level.calculatedAccuracy ();
		}
		//changing from int to float with intent to test later
		if (((int)transform.position.x >= (int)(cBounds [0]).x &&
		    (int)transform.position.x <= (int)(cBounds [1]).x)) {
			th.GetComponent<notes> ().lastChord = "CMaj";
		}
			if ((int)transform.position.x >= (int)(gBounds [0]).x &&
			   (int)transform.position.x <= (int)(gBounds [1]).x) {
				th.GetComponent<notes> ().lastChord = "GMaj";
			}
			if ((int)transform.position.x >= (int)(gBounds [2]).x && !c2) {
				c2 = true;
			if (th.GetComponent<notes> ().gmajDone) {
					level.notesHit += 1;
				}
					level.notesTotal += 1;
				
				accuracyText.text = "Accuracy: " + level.calculatedAccuracy ();
			}
			if ((int)transform.position.x >= (int)(fBounds [0]).x &&
			   (int)transform.position.x <= (int)(fBounds [1]).x) {
				th.GetComponent<notes> ().lastChord = "FMaj";
			}
			if ((int)transform.position.x >= (int)(fBounds [2]).x && !c3) {
				c3 = true;
				if (th.GetComponent<notes> ().fmajDone) {
					level.notesHit += 1;
				}
					level.notesTotal += 1;
				
				accuracyText.text = "Accuracy: " + level.calculatedAccuracy ();
			}
			if ((int)transform.position.x >= (int)(aBounds [0]).x &&
			   (int)transform.position.x <= (int)(aBounds [1]).x) {
				th.GetComponent<notes> ().lastChord = "AMin";
			}
			if ((int)transform.position.x >= (int)(aBounds [2]).x && !c4) {
				c4 = true;
				if (th.GetComponent<notes> ().aminDone) {
					level.notesHit += 1;
				}
					level.notesTotal += 1;
				accuracyText.text = "Accuracy: " + level.calculatedAccuracy ();
			}
			//redundant I think 
			if (sound) {
				if ((int)transform.position.x == (int)(Cmaj.GetComponent<RectTransform> ().position).x) {
					Cmaj.onClick.Invoke ();
				} else if ((int)transform.position.x == (int)(Gmaj.GetComponent<RectTransform> ().position).x) {
					Gmaj.onClick.Invoke ();
				} else if ((int)transform.position.x == (int)(Fmaj.GetComponent<RectTransform> ().position).x) {
					Fmaj.onClick.Invoke ();
				} else if ((int)transform.position.x == (int)(AMin.GetComponent<RectTransform> ().position).x) {
					AMin.onClick.Invoke ();
				}
			}
	}
	void setPositions(string[] arr){
		for (int i = 0; i < arr.Length; i++) {
			Button currentB = GameObject.Find(arr [i]).GetComponent<Button>();
			currentB.GetComponent<RectTransform> ().localPosition = new Vector3 ((256 * i) - 356, 228, 0);
		}
	}

	void arrangementSelector(int i){
		switch (i) {
		case 1:
			setPositions (one);
			break;
		case 2:
			setPositions (two);
			Debug.Log ("lo"+levelOneAccuracy);
			break;
		case 3:
			setPositions (three);
			Debug.Log ("ltw"+levelTwoAccuracy);
			break;
		case 4:
			setPositions (four);
			Debug.Log ("ltr"+levelThreeAccuracy);
			break;
		default :
			sel = 1;
			Debug.Log ("finished!");
			Debug.Log ("lf" + levelFourAccuracy);
			averageAccuracy = ((levelOneAccuracy + levelTwoAccuracy + levelThreeAccuracy + levelFourAccuracy) / 4);
			Debug.Log ("over" + averageAccuracy);
			scores.text = "C–G–Am–F (optimistic): " + levelOneAccuracy + "\n" + " G–Am–F–C: " +
			levelTwoAccuracy + "\n" + " Am–F–C–G (pessimistic): " + levelThreeAccuracy + "\n" +
			" F–C–G–Am: " + levelFourAccuracy + "\n" + " Average: " + averageAccuracy;
			button.SetActive (true);
			break;
		}
		setBounds ();
	}

	void setBounds(){
		Cmaj.GetComponent<RectTransform>().GetWorldCorners(cBounds);
		Gmaj.GetComponent<RectTransform>().GetWorldCorners(gBounds);
		Fmaj.GetComponent<RectTransform>().GetWorldCorners(fBounds);
		AMin.GetComponent<RectTransform>().GetWorldCorners(aBounds);
	}

	void updateAccuracy(int s){
		switch (s) {
		case 1:
			levelOneAccuracy = level.calculatedAccuracy ();
			break;
		case 2:
			levelTwoAccuracy = level.calculatedAccuracy ();
			break;
		case 3:
			levelThreeAccuracy = level.calculatedAccuracy ();
			break;
		case 4: 
			levelFourAccuracy = level.calculatedAccuracy ();
			break;
		default:
			break;
		}
		level.resetAccuracy ();
	}

}
