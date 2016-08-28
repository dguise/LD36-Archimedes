using UnityEngine;
using System.Collections;

public class mirrorscript : MonoBehaviour {

    private float Hp = 100f;
    private float Hp_start = 100f;
    public GameObject healthbar;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
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
