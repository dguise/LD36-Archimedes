using UnityEngine;
using System.Collections;
//using Assets.Scripts;

public class mirrorscript : MonoBehaviour {

    public float Hp = 100f;
    public float Hp_start = 100f;
    public GameObject healthbar;
    public GameObject canvas_bar;
    private bool show_bar = false;
    private bool same_cycle = false;
    public float m_range = 3;
    public GameObject obj_player;
    public GameObject beam;
    private float boat_size_x =1.7f;
    private float boat_size_y = 0.7f;
    public float beam_damage = 10;
    public GameObject spriteRend;
    public Sprite focus1;
    public Sprite focus2;
    public Sprite focus3;
    public Sprite focus4;
    public Sprite mirror_broken;
    public bool is_destroyed = false;
    //public float tempval = 0.1f;
    public float[] beam_range_limits = { 4, 7, 9 };

    public GameObject burnParticle;
    private GameObject _burnParticle;
    private ParticleSystem _burnParticleSystem;

    private bool boatFound = false;

    // Use this for initialization
    void Start ()
    {
        _burnParticle = Instantiate(burnParticle);
        _burnParticleSystem = _burnParticle.GetComponent<ParticleSystem>();
    }

    void FixedUpdate()
    {
        if (is_destroyed) return;

        same_cycle = false;

        //calc beam pos
        float beam_x = Mathf.Cos(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z-90)) * m_range + transform.position.x;
        float beam_y = Mathf.Sin(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z-90)) * m_range + transform.position.y;
        //damage with beam

        GameObject[] boats;
        boats = GameObject.FindGameObjectsWithTag("Boat");
        foreach (GameObject boat in boats)
        {
            float dist = Vector2.Distance(boat.transform.position, new Vector2(beam_x, beam_y));
            if (Mathf.Abs(boat.transform.position.x - beam_x - 0.2f) <= boat_size_x && Mathf.Abs(boat.transform.position.y - beam_y) <= boat_size_y)
            {
                boatFound = true;
                _burnParticle.transform.position = new Vector3(beam_x, beam_y, -2);
                if (!_burnParticleSystem.isPlaying)
                {
                    _burnParticleSystem.Play();
                }

                boat.GetComponent<Boat>().Hurt(beam_damage*Time.fixedDeltaTime);
                Debug.DrawLine(boat.transform.position, new Vector2(beam_x, beam_y), Color.blue);
            }

            Debug.DrawLine(transform.position, new Vector2(beam_x, beam_y), Color.red);   
        }
        if (!boatFound)
        {
            _burnParticleSystem.Stop();
        }
        boatFound = false;      
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (is_destroyed) return;

        //show bar
        if (Hp < Hp_start) show_bar = true; else show_bar = false;
        if (show_bar) canvas_bar.transform.position = new Vector3(canvas_bar.transform.position.x, canvas_bar.transform.position.y, 0);
        else canvas_bar.transform.position = new Vector3(canvas_bar.transform.position.x, canvas_bar.transform.position.y, 3);
        //if (show_bar) Debug.Log("truee"); else Debug.Log("falsee");

        //move and scale beam
        beam.transform.localScale = new Vector3((m_range * 0.87f) / 3, 1, 1);
        if      (m_range < beam_range_limits[0]) beam.transform.localPosition = new Vector2(0, -0.45f);
        else if (m_range < beam_range_limits[1]) beam.transform.localPosition = new Vector2(0, -0.32f);
        else if (m_range < beam_range_limits[2]) beam.transform.localPosition = new Vector2(0, -0.20f);
        else                                     beam.transform.localPosition = new Vector2(0, -0.05f);
        //beam.transform.localPosition = new Vector2( 0, -tempval * m_range);
        //beam.transform.Position

        //if(!show_bar) transform.position= new Vector3(transform.position.x, transform.position.y, 3);

        //disable if too rotated
        float rot_max=340;
        float rot_min=200;
        float rot_curr = transform.rotation.eulerAngles.z;
        //while (rot_curr >= 360) rot_curr -= 360;
        //while (rot_curr < 0) rot_curr += 360;
        //Debug.Log(rot_curr);
        if (rot_curr > rot_max || rot_curr<90)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rot_max);
        }
        else if (rot_curr < rot_min)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rot_min);
        }

        //focus anim state
        if(m_range< beam_range_limits[0]) spriteRend.GetComponent<SpriteRenderer>().sprite = focus4;
        else if (m_range < beam_range_limits[1]) spriteRend.GetComponent<SpriteRenderer>().sprite = focus3;
        else if (m_range < beam_range_limits[2]) spriteRend.GetComponent<SpriteRenderer>().sprite = focus2;
        else spriteRend.GetComponent<SpriteRenderer>().sprite = focus1;


    }

    void Hurt(float dmg)
    {
        //play sound
        if(!GetComponent<AudioSource>().isPlaying)
         GetComponent<AudioSource>().Play();

        if (is_destroyed) return;

        Hp -= dmg;
        //Debug.Log("Hp: " + Hp);

        if (Hp <= 0)
        {
            Hp = 0;

            //release player if bound
            if (obj_player.GetComponent<Assets.Scripts.Player>()._lockedToMirror)
            {
                //test if this mirror
                if(obj_player.GetComponent<Assets.Scripts.Player>()._currMirror==gameObject)
                {
                    //release player
                    Debug.Log("Release player from mirror");
                    obj_player.transform.parent = null;
                    obj_player.GetComponent<Assets.Scripts.Player>()._animator.SetBool("Focusing", false);
                    obj_player.GetComponent<Assets.Scripts.Player>()._animator.SetBool("Moving", false);
                    obj_player.GetComponent<Assets.Scripts.Player>()._mirrorArea = false;
                    obj_player.GetComponent<Assets.Scripts.Player>()._lockedToMirror = false;
                    
                }
            }
            if (obj_player.GetComponent<Assets.Scripts.Player>()._mirrorArea)
            {
                if(obj_player.GetComponent<Assets.Scripts.Player>()._currMirror == gameObject)
                {
                    obj_player.GetComponent<Assets.Scripts.Player>()._mirrorArea = false;
                }
            }


            //TODO: play death anim
            //Destroy(gameObject);

            //destroyed marked
            is_destroyed = true;
            spriteRend.GetComponent<SpriteRenderer>().sprite = mirror_broken;
            beam.GetComponent<SpriteRenderer>().enabled = false;
            canvas_bar.transform.position = new Vector3(canvas_bar.transform.position.x, canvas_bar.transform.position.y, 3);
        }

        //update bar
        healthbar.transform.localScale = new Vector3(Hp / Hp_start, healthbar.transform.localScale.y, healthbar.transform.localScale.z);
    }

    public void heal()
    {
        Hp = Hp_start;
        is_destroyed = false;
        beam.GetComponent<SpriteRenderer>().enabled = true;
        canvas_bar.transform.position = new Vector3(canvas_bar.transform.position.x, canvas_bar.transform.position.y, 0);

        //update bar
        healthbar.transform.localScale = new Vector3(Hp / Hp_start, healthbar.transform.localScale.y, healthbar.transform.localScale.z);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Arrow" && !same_cycle)
        {
            same_cycle = true;
            Hurt(10);

            //Destroy(col.gameObject);
        }
    }
}
