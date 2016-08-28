using UnityEngine;
using System.Collections;

public class mirrorscript : MonoBehaviour {

    private float Hp = 100f;
    private float Hp_start = 100f;
    public GameObject healthbar;
    public GameObject canvas_bar;
    private bool show_bar = false;
    private bool same_cycle = false;
    public float m_range = 1;

    // Use this for initialization
    void Start ()
    {
	
	}

    void FixedUpdate()
    {
        same_cycle = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //show bar
        if (Hp < Hp_start) show_bar = true; else show_bar = false;
        if (show_bar) canvas_bar.transform.position = new Vector3(canvas_bar.transform.position.x, canvas_bar.transform.position.y, 0);
        else canvas_bar.transform.position = new Vector3(canvas_bar.transform.position.x, canvas_bar.transform.position.y, 3);
        //if (show_bar) Debug.Log("truee"); else Debug.Log("falsee");

        //if(!show_bar) transform.position= new Vector3(transform.position.x, transform.position.y, 3);
    }

    void Hurt(float dmg)
    {
        Hp -= dmg;
        //Debug.Log("Hp: " + Hp);

        if (Hp <= 0)
        {
            Hp = 0;
            //TODO: play death anim
            Destroy(gameObject, 1);
        }

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
