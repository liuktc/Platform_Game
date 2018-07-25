using UnityEngine;
using System.Collections;

public class Zoom : MonoBehaviour
{
	float targetOrtho;
	public float zoom = 2f;
	public float zoomTime = 2f;
	public float moveTime = 2f;
	public float deZoomTime = 2f;
	public float resetMoveTime = 2f;

	public Transform pointToZoom;
	bool bzoom = false;
	bool deZoom = false;
	Vector3 InitialPoint;
	float InitialZoom;

	void Start()
	{
		targetOrtho = Camera.main.orthographicSize / zoom;
		//ZoomTo(pointToZoom);
	}
	void Update()
	{
		if (bzoom)
		{
			ZoomIn();
		}
		else
		{
			if (deZoom)
			{
				ZoomOut();
			}
		}
	}

	void ZoomOut()
	{
		Debug.Log("DeZooming to " + InitialPoint);
		MoveTo(InitialPoint, false);
		Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, InitialZoom, (1 / zoomTime) * Time.deltaTime * Mathf.Abs(Camera.main.orthographicSize - InitialZoom));
		//Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetOrtho, smoothSpeed);
	}
	void ZoomIn()
	{
		Debug.Log("Zooming to " + pointToZoom.position);
		MoveTo(pointToZoom.position,true);
		Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, (1 / zoomTime) * Time.deltaTime * Mathf.Abs(Camera.main.orthographicSize - targetOrtho));
		//Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetOrtho, smoothSpeed);	}
	void MoveTo(Vector3 t,bool zoomin)
	{
		if (zoomin)
		{
			Vector3 v = new Vector3(Mathf.MoveTowards(transform.position.x, t.x, (1 / moveTime) * Time.deltaTime * Mathf.Abs(transform.position.x - t.x)), Mathf.MoveTowards(Camera.main.transform.position.y, t.y, (1 / moveTime) * Time.deltaTime * Mathf.Abs(transform.position.y - t.y)), Camera.main.transform.position.z);
			//Vector3 v = new Vector3(Mathf.Lerp(Camera.main.transform.position.x, t.position.x, smoothSpeed ), Mathf.Lerp(Camera.main.transform.position.y, t.position.y, Time.deltaTime), Camera.main.transform.position.z);
			//Debug.Log(v);
			transform.position = v;
			if (Mathf.Abs(transform.position.x - t.x) <= 0.1f && Mathf.Abs(Camera.main.orthographicSize - targetOrtho) <= 0.1 && Mathf.Abs(transform.position.y - t.y) <= 0.1f)
			{
				Debug.Log("finished zooming");
				bzoom = false;
				deZoom = true;
			}
		}
		else
		{
			Vector3 v = new Vector3(Mathf.MoveTowards(transform.position.x, t.x, (1 / resetMoveTime) * Time.deltaTime * Mathf.Abs(transform.position.x - t.x)), Mathf.MoveTowards(Camera.main.transform.position.y, t.y, (1 / resetMoveTime) * Time.deltaTime * Mathf.Abs(transform.position.y - t.y)), Camera.main.transform.position.z);
			//Vector3 v = new Vector3(Mathf.Lerp(Camera.main.transform.position.x, t.position.x, smoothSpeed ), Mathf.Lerp(Camera.main.transform.position.y, t.position.y, Time.deltaTime), Camera.main.transform.position.z);
			//Debug.Log(v);
			transform.position = v;
			if (Mathf.Abs(transform.position.x - t.x) <= 0.1f && Mathf.Abs(Camera.main.orthographicSize - targetOrtho) <= 0.1 && Mathf.Abs(transform.position.y - t.y) <= 0.1f)
			{
				Debug.Log("finished deZooming");
				bzoom = false;
				deZoom = false;
			}
		}
	}
	void ZoomTo(Transform t)
	{
		bzoom = true;
		pointToZoom = t;
		InitialPoint = transform.position; 
		InitialZoom = Camera.main.orthographicSize;
	}
}
