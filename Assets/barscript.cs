using UnityEngine;
using System.Collections;

public class barscript : MonoBehaviour {

    private Vector2 startpos;
    
    // Use this for initialization
	void Start ()
    {
        startpos = transform.position;

    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
        transform.position = startpos;
    }
}
