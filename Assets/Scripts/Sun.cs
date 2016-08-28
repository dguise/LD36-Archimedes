using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Sun : MonoBehaviour
    {

        private GameObject[] mirrors;
        public float Speed = 1f;
        public float Range = 100;

        private float _timeStamp = 0f;
        private float _cooldownDmg = 2f;


        // Use this for initialization
        void Start () {
            mirrors = GameObject.FindGameObjectsWithTag("Mirror");
        }
	
        // Update is called once per frame
        void Update ()
        {
            foreach(GameObject mirror in mirrors)
            {
                RaycastBeam(gameObject.transform.position, mirror.transform.position, mirror.transform.rotation);
                
            }
            transform.position = transform.position + Vector3.down * Time.deltaTime * Speed;

        }

        void RaycastBeam(Vector2 sunPos, Vector2 mirrorPos, Quaternion mirrorAngle)
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
                
                RaycastHit2D boatHit = Physics2D.Raycast(mirrorHit.point, reflectionDirection, Range, 1 << LayerMask.NameToLayer("Boat"));
                
                if (boatHit.collider != null)
                {
                    if (_timeStamp <= Time.time)
                    {
                        //todo: does not work, this doesnt allow additive beams. Do this in boat.
                        _timeStamp = Time.time + _cooldownDmg;
                        boatHit.collider.gameObject.SendMessage("Hurt", 10);
                        Debug.Log("wat");
                    }
                }

                Debug.DrawRay(mirrorHit.point, reflectionDirection * Range);
            }

            Debug.DrawLine(sunPos, mirrorPos, Color.green);
        }


    }
}
//var angle = Vector2.Angle(sunPos, mirrorPos);
