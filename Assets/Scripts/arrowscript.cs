using UnityEngine;
using System.Collections;

public class arrowscript : MonoBehaviour
{

    float lifeleft = 10;
    public float arrow_speed = 1;
    public GameObject m_manager;

    // Use this for initialization
    void Start()
    {
        GameObject[] managers;
        managers = GameObject.FindGameObjectsWithTag("EditorOnly");
        foreach(GameObject manager in managers)
        {
            m_manager = manager;
        }
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

        if (col.tag == "Player")
        {

            //end game
            m_manager.GetComponent<Assets.Scripts.LevelManager>().gameover_call();
        }
    }
}
