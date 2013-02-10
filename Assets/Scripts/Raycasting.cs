using UnityEngine;
using System.Collections;
using System.Linq;

public class Raycasting : MonoBehaviour {
	
	public Transform theSphere;
	public Transform pillar;

	public Transform Father;
	public float scaleDownTime = 1.0f;
	public bool Deleting = false;
	public GameObject newSound;
	public GameObject destroySound;
	private GameObject ToDel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		/*if ( Input.GetKeyDown( KeyCode.S ) ) {
			
			SimpleSmoothMouseLook ssml = this.GetComponent<SimpleSmoothMouseLook>();
			ssml.lockCursor = !ssml.lockCursor;
			
		}*/
		
		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		RaycastHit hit = new RaycastHit();
		
		if ( Physics.Raycast( ray, out hit ) ) {
			
			theSphere.position = Vector3.Lerp( theSphere.position, hit.point + hit.normal * .75f, .075f );
			
			if(Input.GetMouseButtonDown(0)){
				var theNew = (Transform)Instantiate(pillar, hit.point, Quaternion.identity);
				theNew.parent = Father;
			}
			if(Input.GetMouseButton(0)){
				var items = Father.GetComponentsInChildren<Transform>().Where(x => x != Father);
				if(items.Count() > 0){
					var last = items.Last();
					last.localScale+= Vector3.up * 0.1f;
					if(!newSound.audio.isPlaying) newSound.audio.Play();
				}
			}
			if(Input.GetKeyDown(KeyCode.R) && !Deleting) {
				var children = Father.GetComponentsInChildren<Transform>().Where(x => x != Father);
				if(children.Count() > 0) {
					destroySound.audio.Play();
					Deleting = true;
					ToDel = Father.GetComponentsInChildren<Transform>().Where(x => x != Father).Last().gameObject;
					iTween.ScaleTo( children.Last().gameObject, iTween.Hash(
						"scale", Vector3.zero,
						"time", scaleDownTime,
						"oncomplete", "DidScaleDown",
						"oncompletetarget", this.gameObject ) );
				}
			}

				/*
				for ( int i=0; i<2; i++ ) {
					Quaternion spikeOrientation = Quaternion.FromToRotation( Vector3.up, hit.normal );
					spikeOrientation *= Quaternion.Euler( Random.Range( -30.0f, 30.0f ), Random.Range( -30.0f, 30.0f ), 0 );
					Instantiate( theSpike, hit.point, spikeOrientation );
				}
				
				for ( int i=0; i<3; i++ ) {
					Quaternion clusterOrientation = Quaternion.FromToRotation( Vector3.up, hit.normal );
					Vector3 clusterOffset = clusterOrientation * new Vector3( Random.Range( -2.5f, 2.5f ), 0, Random.Range( -2.5f, 2.5f ) );
					Instantiate( theCluster, hit.point + clusterOffset, clusterOrientation );
				}*/
			
		}
		
	}
	
	void DidScaleDown() {
		
		Destroy( ToDel );
		Deleting = false;
	}
	
	void OnDrawGizmos() {
		
		Ray mouseRay = Camera.main.ScreenPointToRay( Input.mousePosition );
		
		Gizmos.color = Color.red;
		Gizmos.DrawRay( mouseRay );
		
		Gizmos.color = Color.green;
		Gizmos.DrawLine( mouseRay.origin, mouseRay.origin + mouseRay.direction * 10.0f );
		
		Gizmos.color = Color.blue;
		for ( int i=0; i<10; i++ ) {
			
			Gizmos.DrawSphere( mouseRay.origin + mouseRay.direction * (float)i, .5f );
			
		}
		
	}
}
