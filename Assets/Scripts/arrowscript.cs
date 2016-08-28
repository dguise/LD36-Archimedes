using UnityEngine;
using System.Collections;

public class arrowscript : MonoBehaviour
{

    float lifeleft = 10;
    public float arrow_speed = 1;

    // Use this for initialization
    void Start()
    {

    }

    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x + arrow_speed, transform.position.y);

        lifeleft -= Time.fixedDeltaTime;
        if (lifeleft <= 0) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Mirror")
        {
            Destroy(gameObject);

        }
    }
}
