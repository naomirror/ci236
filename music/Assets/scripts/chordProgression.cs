using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//https://answers.unity.com/questions/296347/move-transform-to-target-in-x-seconds.html
public class chordProgression : MonoBehaviour {
	public Transform startPos;
	public Transform endPos;
	public Button Cmaj;
	public Button Fmaj;
	public Button Gmaj;
	public Button AMin;
	GameObject th;
	string[] one = {"CMaj", "GMaj", "AMin", "FMaj" };
	string[] two = {"GMaj", "AMin", "FMaj", "CMaj" };
	string[] three = {"AMin", "FMaj", "CMaj", "GMaj" };
	string[] four = {"FMaj", "CMaj", "GMaj", "AMin" };
	float t = 0;
	Vector3[] cBounds = new Vector3[4];
	Vector3[] gBounds = new Vector3[4];
	Vector3[] fBounds = new Vector3[4];
	Vector3[] aBounds = new Vector3[4];
	// Use this for initialization
	void Start () {
		setPositions (one);
		Cmaj.GetComponent<RectTransform>().GetWorldCorners(cBounds);
		Gmaj.GetComponent<RectTransform>().GetWorldCorners(gBounds);
		Fmaj.GetComponent<RectTransform>().GetWorldCorners(fBounds);
		AMin.GetComponent<RectTransform>().GetWorldCorners(aBounds);
		th = GameObject.Find("TouchHandler");
	}
	bool sound = false;
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime / 8.0f;
		transform.position = Vector3.Lerp (startPos.position, endPos.position, t);
		if (transform.position == endPos.position) {
			t = 0;
			transform.position = startPos.position;
		}
		if ((int)transform.position.x >= (int)Camera.main.ScreenToWorldPoint(cBounds[0]).x &&
			(int)transform.position.x <= (int)Camera.main.ScreenToWorldPoint(cBounds[1]).x){
			if (th.GetComponent<notes> ().checkChordAgainstPlayerInput ("CMaj")) {
				Debug.Log ("you fucking genius");
			}
		}
		if ((int)transform.position.x >= (int)Camera.main.ScreenToWorldPoint(gBounds[0]).x &&
			(int)transform.position.x <= (int)Camera.main.ScreenToWorldPoint(gBounds[1]).x){
			if (th.GetComponent<notes> ().checkChordAgainstPlayerInput ("GMaj")) {
				Debug.Log ("you fucking genius2");
			}
		}
		if ((int)transform.position.x >= (int)Camera.main.ScreenToWorldPoint(fBounds[0]).x &&
			(int)transform.position.x <= (int)Camera.main.ScreenToWorldPoint(fBounds[1]).x){
			if (th.GetComponent<notes> ().checkChordAgainstPlayerInput ("FMaj")) {
				Debug.Log ("you fucking genius3");
			}
		}
		if ((int)transform.position.x >= (int)Camera.main.ScreenToWorldPoint(aBounds[0]).x &&
			(int)transform.position.x <= (int)Camera.main.ScreenToWorldPoint(aBounds[1]).x){
			if (th.GetComponent<notes> ().checkChordAgainstPlayerInput ("AMin")) {
				Debug.Log ("you fucking genius4");
			}
		}
		if (sound) {
			if ((int)transform.position.x == (int)Camera.main.ScreenToWorldPoint (Cmaj.GetComponent<RectTransform> ().position).x) {
				Cmaj.onClick.Invoke ();
			} else if ((int)transform.position.x == (int)Camera.main.ScreenToWorldPoint (Gmaj.GetComponent<RectTransform> ().position).x) {
				Gmaj.onClick.Invoke ();
			} else if ((int)transform.position.x == (int)Camera.main.ScreenToWorldPoint (Fmaj.GetComponent<RectTransform> ().position).x) {
				Fmaj.onClick.Invoke ();
			} else if ((int)transform.position.x == (int)Camera.main.ScreenToWorldPoint (AMin.GetComponent<RectTransform> ().position).x) {
				AMin.onClick.Invoke ();
			}
		}
	}
	void setPositions(string[] arr){
		for (int i = 0; i < arr.Length; i++) {
			Button currentB = GameObject.Find(arr [i]).GetComponent<Button>();
			currentB.GetComponent<RectTransform> ().localPosition = new Vector3 ((256 * i) - 256, 228, 0);
		}
	}
}
