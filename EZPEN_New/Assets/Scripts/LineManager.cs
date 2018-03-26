using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LineManager : MonoBehaviour {

	[SerializeField]
	private GameObject lineGeneratorPrefab;
	[SerializeField]
	private GameObject linePointsPrefab;
	[SerializeField]
	private GameObject movingBall;

	private Vector3 lastPos = new Vector3 (0, 0, 0);
	private float distBetweenPoints = 0.04f;
	private bool drawAlready = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		if (Input.GetKey (KeyCode.Space)) {
			Vector3 newPos = movingBall.transform.position;
			newPos.y = 0;
			if (Vector3.Distance (newPos, lastPos) > distBetweenPoints) {
				lastPos = newPos;
				CreatePointMarker (newPos);
			}
		}
			
		if (!Input.GetKey (KeyCode.Space)) {
			GenerateNewLine ();
			if (drawAlready)
				ClearAllLines ();
		}

		if (Input.GetKeyDown (KeyCode.C)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			ScreenCapture.CaptureScreenshot ("./ss.png");
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}


	private void GenerateNewLine() {
		GameObject[] allPoints = GameObject.FindGameObjectsWithTag ("LinePointMarker");
		Vector3[] allPointsPositions = new Vector3[allPoints.Length];

		if (allPoints.Length >= 2) {
			for (int i = 0; i < allPoints.Length; i++)
				allPointsPositions [i] = allPoints [i].transform.position;

			SpawnLines (allPointsPositions);
		}


	}

	private void CreatePointMarker(Vector3 pointOfPosition) {
		Instantiate (linePointsPrefab, pointOfPosition, Quaternion.identity);
	}

	private void SpawnLines(Vector3[] allPoitns) {
		GameObject newLineGenerator = Instantiate (lineGeneratorPrefab);
		LineRenderer lr = newLineGenerator.GetComponent<LineRenderer> ();

		lr.positionCount = allPoitns.Length;
		lr.SetPositions (allPoitns);
		lr.loop = false;
		drawAlready = true;
	}


	private void ClearAllLines() {
		GameObject[] allPoints = GameObject.FindGameObjectsWithTag ("LinePointMarker");
		foreach (GameObject p in allPoints)
			Destroy (p);
		drawAlready = false;
	}



}
