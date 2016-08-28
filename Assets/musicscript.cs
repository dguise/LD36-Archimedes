using UnityEngine;
using System.Collections;

public class musicscript : MonoBehaviour {

    public AudioSource source_intro;
    public AudioSource source_loop;

    bool in_loop = false;

    // Use this for initialization
    void Start ()
    {
        source_intro.Play();
        source_loop.loop = true;

    }
	
	// Update is called once per frame
	void Update()
    {
	    if(!in_loop && !source_intro.isPlaying)
        {
            source_loop.Play();
            in_loop = true;
        }
	}
}
