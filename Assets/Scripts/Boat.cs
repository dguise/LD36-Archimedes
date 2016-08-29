using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using UnityStandardAssets.Utility;

public class Boat : MonoBehaviour
{
    public float Speed = 1f;
    private float Hp = 100f;
    private float Hp_start = 100f;
    public GameObject healthbar;
    public GameObject canvas_bar;
    public GameObject arrow;
    private bool show_bar = false;
    public float shoot_delay = 3;
    private float shoot_delay_timer = 0;
    private bool shouldShoot = false;
    private float _cooldownDmg = 0.2f;
    private float _timeStamp = 0;
    public float burn_speed = 1000;
    public float cool_speed = 1;

    private bool _sailForward = true;
    public float burn_temp=0;

    public AudioSource sound_sink;
    public AudioSource sound_shoot;
    public AudioSource sound_burn;


    private Animator _animator;
    // Use this for initialization
    void Start ()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        if (shoot_delay_timer > 0) shoot_delay_timer -= Time.fixedDeltaTime;
        if(!_sailForward && shoot_delay_timer<=0 && shouldShoot)
        {
            Instantiate(arrow, transform.position, Quaternion.Euler(0,0,-90));
            shoot_delay_timer = shoot_delay;

            //play sound
            sound_shoot.Play();
        }

        if(burn_temp>0)
        {
            burn_temp-= Time.fixedDeltaTime * cool_speed;
            if (burn_temp < 0) burn_temp = 0;

            //continue burn sound
            if (!sound_burn.isPlaying)
            {
                sound_burn.Play();
            }
        }
        sound_burn.volume = burn_temp;

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
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z * 0.5f, transform.rotation.w);
	    }

        //check if should be removed
        if(Hp<=0 && !sound_sink.isPlaying) Destroy(gameObject);

        if (Hp<Hp_start && Hp > 0) show_bar = true; else show_bar = false;
        if(show_bar) canvas_bar.transform.position=new Vector3(canvas_bar.transform.position.x, canvas_bar.transform.position.y,0);
        else canvas_bar.transform.position = new Vector3(canvas_bar.transform.position.x, canvas_bar.transform.position.y, 3);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Land")
        {
            _sailForward = false;
            shouldShoot = true;
        }
    }

    public void Hurt(float dmg)
    {
        if (Hp <= 0) return;

        Hp -= dmg * burn_temp;

        if (Hp <= 0)
        {
            //play sound
            sound_sink.Play();

            //play animation
            _animator.SetBool("Dead", true);

            Hp = 0;
            _sailForward = false;
            shouldShoot = false;
        }

        //increment burn factor
        burn_temp += Time.fixedDeltaTime* burn_speed;
        if (burn_temp > 1) burn_temp = 1;

        //play burn sound
        sound_burn.volume = burn_temp;
        if (!sound_burn.isPlaying)
        {
            sound_burn.Play();
        }
        //Debug.Log(burn_temp);

        //update bar
        healthbar.transform.localScale = new Vector3(Hp / Hp_start, healthbar.transform.localScale.y, healthbar.transform.localScale.z);
    }
    
}
