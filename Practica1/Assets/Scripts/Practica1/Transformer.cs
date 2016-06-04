using UnityEngine;
using System.Collections;

public class Transformer : MonoBehaviour {
    public bool world;
    public float rotationSpeed = 5.0f;
    public MeshRenderer renderer;
    public Rigidbody rb;

	// Use this for initialization
	void Start () {
        this.renderer = this.GetComponent<MeshRenderer>();
        this.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        this.renderer.material.SetColor("_Color", Color.black);
        this.rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        this.renderer.material.SetColor("_Color", Color.Lerp(Color.black, Color.white, 0.1f));
	}
}
