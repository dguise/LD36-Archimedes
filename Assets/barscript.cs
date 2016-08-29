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
        float z = transform.position.z;
        //transform.position = startpos;
        transform.position = new Vector3(startpos.x, startpos.y,z);
    }
}
