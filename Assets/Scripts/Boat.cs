using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;

public class Boat : MonoBehaviour
{
    public float Speed = 5f;
    public float Hp = 10f;

    private bool _sailForward = true;

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
        Hp -= dmg;

        if (Hp <= 0)
        {
            //TODO: play death anim
            Destroy(gameObject, 1);
        }
    }
    
}
