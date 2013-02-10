using UnityEngine;
using System.Collections;
using System.Linq;

public class Restart : MonoBehaviour {
	
	public Transform Respawn;
	public Collider Player;
	public Transform ToDel;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other){
		if(other == Player){
			Player.transform.position = Respawn.position;
			Player.audio.Play();
			var children = ToDel.GetComponentsInChildren<Transform>();
			children.Where(x => x != ToDel).ToList().ForEach(x => Destroy(x.gameObject));
		}
	}
}
