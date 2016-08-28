using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Utility;

public class Boat : MonoBehaviour
{
    private float Speed = 1f;
    private float Hp = 100f;
    private float Hp_start = 100f;
    public GameObject healthbar;
    public GameObject canvas_bar;
    private bool show_bar = false;

    private float _cooldownDmg = 0.5f;
    private float _timeStamp = 0;

    private bool _sailForward = true;

    public AudioSource sound_sink;
    public AudioSource sound_shoot;

    // Use this for initialization
    void Start () 
    {
        

    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (_sailForward)
	    {
	        transform.position = transform.position + Vector3.right*Time.deltaTime*Speed;
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z * 0.5f, transform.rotation.w);
	    }
	    else
	    {
	        //shoot
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z * 0.5f, transform.rotation.w);
	    }

        //check if should be removed
        if(Hp<=0 && !sound_sink.isPlaying) Destroy(gameObject, 1);

        if (Hp<Hp_start) show_bar = true; else show_bar = false;
        if(show_bar) canvas_bar.transform.position=new Vector3(canvas_bar.transform.position.x, canvas_bar.transform.position.y,0);
        else canvas_bar.transform.position = new Vector3(canvas_bar.transform.position.x, canvas_bar.transform.position.y, 3);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Land")
        {
            _sailForward = false;
        }
    }

    void Hurt(float dmg)
    {
        if (Hp <= 0) return;

        Hp -= dmg;
        Debug.Log("Hp: " + Hp);

        if (Hp <= 0)
        {
            //play sound
            sound_sink.Play();

            Hp = 0;
            _sailForward = false;
            //TODO: play death anim
            //Destroy(gameObject, 1);
        }

        //update bar
        healthbar.transform.localScale = new Vector3(Hp / Hp_start, healthbar.transform.localScale.y, healthbar.transform.localScale.z);
    }
    
}
