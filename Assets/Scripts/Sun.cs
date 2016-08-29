using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Sun : MonoBehaviour
    {

        private GameObject[] mirrors;
        public float Speed = 1f;
        //public float Range = 100;

        private float _timeStamp = 0f;
        private float _cooldownDmg = 2f;


        // Use this for initialization
        void Start ()
        {
            mirrors = GameObject.FindGameObjectsWithTag("Mirror");
        }
	
        // Update is called once per frame
        void Update ()
        {
            /*foreach(GameObject mirror in mirrors)
            {
                if (mirror != null)
                {
                    //mirrorscript mirrScript = mirror.GetComponent<mirrorscript>();
                    //float range = mirrScript.m_range;
                    float range = mirror.GetComponent<mirrorscript>().m_range;
                    //Debug.Log(range);
                    RaycastBeam(gameObject.transform.position, mirror.transform.position, mirror.transform.rotation, range);
                }
                
            }
            transform.position = transform.position + Vector3.down * Time.deltaTime * Speed;*/

        }

        /*void RaycastBeam(Vector2 sunPos, Vector2 mirrorPos, Quaternion mirrorAngle, float range)
        {
            
            Vector3 direction = mirrorPos - sunPos;

            RaycastHit2D mirrorHit =  Physics2D.Raycast(sunPos, direction, Mathf.Infinity, 1 << LayerMask.NameToLayer("Mirror"));
         

            float normX = direction.normalized.x;
            float normY = direction.normalized.y;
            
            float angle = Mathf.Rad2Deg * Mathf.Acos(normX);
            if (normY > 0)
            {
                angle *= -1;
            }
            angle -= 180;

            angle += (mirrorAngle.eulerAngles.z - 270) * 2;

            Vector3 reflectionDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), -1);

            if (mirrorHit.collider)
            {
                
                RaycastHit2D boatHit = Physics2D.Raycast(mirrorHit.point, reflectionDirection, range, 1 << LayerMask.NameToLayer("Boat"));
                
                if (boatHit.collider != null && boatHit.collider.tag == "Boat")
                {
                    boatHit.collider.gameObject.SendMessage("Hurt", 20 * (boatHit.distance / range));
                }

                Debug.DrawRay(mirrorHit.point, reflectionDirection * range);
            }

            Debug.DrawLine(sunPos, mirrorPos, Color.green);
        }*/


    }
}