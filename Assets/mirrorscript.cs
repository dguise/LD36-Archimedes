using UnityEngine;
using System.Collections;

public class mirrorscript : MonoBehaviour {

    private float Hp = 100f;
    private float Hp_start = 100f;
    public GameObject healthbar;
    public GameObject canvas_bar;
    private bool show_bar = false;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //show bar
        if (Hp < Hp_start) show_bar = true; else show_bar = false;
        if (show_bar) canvas_bar.transform.position = new Vector3(canvas_bar.transform.position.x, canvas_bar.transform.position.y, 0);
        else canvas_bar.transform.position = new Vector3(canvas_bar.transform.position.x, canvas_bar.transform.position.y, 10000);
        if (show_bar) Debug.Log("truee"); else Debug.Log("falsee");

        //if(!show_bar) transform.position= new Vector3(transform.position.x, transform.position.y, 3);
    }

    void Hurt(float dmg)
    {
        Hp -= dmg;
        Debug.Log("Hp: " + Hp);

        if (Hp <= 0)
        {
            Hp = 0;
            //TODO: play death anim
            Destroy(gameObject, 1);
        }

        //update bar
        healthbar.transform.localScale = new Vector3(Hp / Hp_start, healthbar.transform.localScale.y, healthbar.transform.localScale.z);
    }
}
