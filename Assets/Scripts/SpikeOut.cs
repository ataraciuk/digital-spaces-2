using UnityEngine;
using System.Collections;

public class SpikeOut : MonoBehaviour {
	
	public float scaleUpMin = 5.0f;
	public float scaleUpMax = 10.0f;
	public float scaleUpTime = 0.5f;
	public float scaleDownTime = 0.5f;
	public float scaleDownDelay = 1.0f;
	
	// Use this for initialization
	void Start () {
		this.transform.localScale = Vector3.zero;
		
		Vector3 nextScale = new Vector3( 1.0f, Random.Range( scaleUpMin, scaleUpMax ), 1.0f );
		iTween.ScaleTo( this.gameObject, iTween.Hash( "scale", nextScale,
													  "time", scaleUpTime,
													  "oncomplete", "DidScaleUp",
													  "oncompletetarget", gameObject ) );
	}
	
	void DidScaleUp() {
		
		iTween.ScaleTo( this.gameObject, iTween.Hash( "scale", Vector3.zero,
													  "time", scaleDownTime,
													  "delay", scaleDownDelay,
													  "oncomplete", "DidScaleDown",
													  "oncompletetarget", gameObject ) );
		
	}
	
	void DidScaleDown() {
		
		Destroy( gameObject );
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
