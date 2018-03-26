using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallMovement : MonoBehaviour {

	public float speedRate = 0.3f;

	public string url = "http://ez-pen.herokuapp.com/search";

	public float timeDuringEachFrame = 0.01f;
	private float time_since_last_time = 0.0f;

	private float tolerance = 0.05f;
	private float x_value = 0.0f;
	private float y_value = 0.0f;
	private float z_value = 0.0f;
	private float moveH = 0.0f;
	private float moveV = 0.0f;
	private string t = "";
	private float velocityX = 0.0f;
	private float velocityY = 0.0f;
	private float minX, maxX, minY, maxY;
	private float edgeFactor = 10.0f;
	// Use this for initialization
	void Start () {
		Debug.Log ("x: "+transform.position.x);
		Debug.Log ("y: " +transform.position.y);
		Debug.Log ("z: "+transform.position.z);
		time_since_last_time = Time.realtimeSinceStartup;
//		float camDistance = Vector3.Distance (transform.position, Camera.main.transform.position);
//		Vector2 bottomCorner = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, camDistance));
//		Vector2 topCorner = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, camDistance));
//		minX = bottomCorner.x - edgeFactor;
//		maxX = topCorner.x + edgeFactor;
//		minY = bottomCorner.y - edgeFactor;
//		maxY = topCorner.y + edgeFactor;
		float vertExtend = (float) Camera.main.orthographicSize;
		float horzExtend = (float) vertExtend * Screen.width / Screen.height;
		minX = -17.0f;//horzExtend - 100.0f / 2.0f;
			maxX = 17.0f;//100.0f / 2.0f - horzExtend;
			minY = -10.0f;//vertExtend - 50.0f;
			maxY = 10.0f;//50.0f - vertExtend;
	}

	// Update is called once per frame
	void Update () {

		Debug.Log ("x: "+transform.position.x);
		Debug.Log ("y: " +transform.position.y);
		Debug.Log ("z: "+transform.position.z);
		//Debug.Log ("Testing 1");

		//if (Time.realtimeSinceStartup - time_since_last_time > timeDuringEachFrame) {

			//time_since_last_time = Time.realtimeSinceStartup;

			//transform.Translate (Input.acceleration.x, Input.acceleration.y, 0.0f);
			//float moveH = Input.GetAxis ("Horizontal");
			//float moveV = Input.GetAxis ("Vertical");

		if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.X) ){
			StartCoroutine ("getWeb");
			velocityX += x_value * timeDuringEachFrame * 0.02f; 
				//(Time.realtimeSinceStartup - time_since_last_time) * 2f;
		
			velocityY += y_value * timeDuringEachFrame * 0.02f; 
				//(Time.realtimeSinceStartup - time_since_last_time) * 2f;
		
			moveH = velocityX * timeDuringEachFrame;
			moveV = velocityY * timeDuringEachFrame;
			time_since_last_time = Time.realtimeSinceStartup;
			//Debug.Log ("x_value: " + x_value + ", y_value: " + y_value);

			transform.Translate(new Vector3 (moveH, 0.0f, -moveV));
		} else if(Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.X)) {
		
			velocityX = 0;
			velocityY = 0;
			moveH = 0.0f;
			moveV = 0.0f;
			x_value = 0.0f;
			y_value = 0.0f;
		}
			
		Vector3 pos = transform.position;
		if (pos.x < minX)
			pos.x = minX;
		if (pos.x > maxX)
			pos.x = maxX;
		if (pos.z < minY)
			pos.z = minY;
		if (pos.z > maxY)
			pos.z = maxY;
		transform.position = pos;

				//x_value = 0.0f;
			//y_value = 0.0f;
			//z_value = 0.0f;

	}

	IEnumerator getWeb() {
		//Debug.Log ("Testing 2");
		using (WWW www = new WWW (url)) {
			yield return www;
			string current_text = www.text;
			//Debug.Log (current_text);
			string[] firstSplit = current_text.Split ('\"');
			if (firstSplit.Length >= 11) {
				string x = firstSplit [3];
				string y = firstSplit [7];
				string z = firstSplit [11];
				t = firstSplit [15];
						//if (Math.Abs(float.Parse(x))> Math.Abs(x_value))
				x_value = float.Parse (x);
						//if (Math.Abs(float.Parse(y))> Math.Abs(y_value))
				y_value = float.Parse (y);
						//if (Math.Abs(float.Parse(z))> Math.Abs(z_value))
				z_value = float.Parse (z);
				//sssssDebug.Log (x_value);
			}

		}
	}


}
