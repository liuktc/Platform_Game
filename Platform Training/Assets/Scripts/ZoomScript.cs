using UnityEngine;
using System.Collections;

public class ZoomScript : MonoBehaviour
{
	float targetOrtho;
	public float zoom = 2f;
	public float zoomTime = 2f;
	public float moveTime = 2f;
	public float deZoomTime = 2f;
	public float resetMoveTime = 2f;

	Vector3 pointToZoom;
	bool bzoom = false;
	bool deZoom = false;
	Vector3 InitialPoint;
	float InitialZoom;

	public float slowdownFactor;
	public float slowdownLenght;
	bool resetTimeScale = false;

	void Start()
	{
		//ZoomTo(pointToZoom);
	}
	void Update()
	{
		InitialPoint = GetComponent<CameraFollow>().focusPosition;
		if (bzoom)
		{
			ZoomIn();
		}
		else
		{
			if (deZoom)
			{
				//GetComponent<CameraFollow>().active = true;
				ZoomOut();
			}
		}
		if (resetTimeScale)
		{
			Time.timeScale += (1 / slowdownLenght) * Time.unscaledDeltaTime;
			Time.timeScale = Mathf.Clamp(Time.timeScale, 0.0f, 1.0f);
		}
	}

	void ZoomOut()
	{
		//Debug.Log("DeZooming to " + InitialPoint);
		MoveTo(InitialPoint, false);
		Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, InitialZoom, (1 / deZoomTime) * Time.deltaTime * Mathf.Abs(Camera.main.orthographicSize - InitialZoom));
		//Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetOrtho, smoothSpeed);
	}
	void ZoomIn()
	{
		//Debug.Log("Zooming to " + pointToZoom);
		MoveTo(pointToZoom,true);
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
				//Debug.Log("finished zooming");
				resetTimeScale = true;
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
			//Debug.Log(Mathf.Abs(Camera.main.orthographicSize - targetOrtho));
			if (Mathf.Abs(transform.position.x - t.x) <= 0.1f && Mathf.Abs(Camera.main.orthographicSize - InitialZoom) <= 0.1 && Mathf.Abs(transform.position.y - t.y) <= 0.1f)
			{
				//Debug.Log("finished deZooming");
				bzoom = false;
				deZoom = false;
				GetComponent<CameraFollow>().active = true;
			}
		}
	}
	public void ZoomTo(Transform t)
	{
		DoSlowMotion();
		GetComponent<CameraFollow>().active = false;
		targetOrtho = Camera.main.orthographicSize / zoom;
		bzoom = true;
		pointToZoom = new Vector3(t.position.x,t.position.y,t.position.z);
		InitialPoint = transform.position; 
		InitialZoom = Camera.main.orthographicSize;
	}

	void DoSlowMotion()
	{
		Time.timeScale = 1 / slowdownFactor;
		Time.fixedDeltaTime = Time.timeScale * 0.02f;
	}
}
