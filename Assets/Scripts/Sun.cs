﻿using UnityEngine;

namespace Assets.Scripts
{
    public class Sun : MonoBehaviour
    {

        private GameObject[] mirrors;
        public float Speed = 1f;
        public float Range = 10;
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
            RaycastHit mirrorHit;
            Vector3 direction = mirrorPos - sunPos;
            
            Physics.Raycast(sunPos, direction, out mirrorHit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Mirror"));
         

            float normX = direction.normalized.x;
            float normY = direction.normalized.y;
            
            float angle = Mathf.Rad2Deg * Mathf.Acos(normX);
            if (normY > 0)
            {
                angle *= -1;
            }
            Debug.Log(angle);
            angle -= 180;

            angle += (mirrorAngle.eulerAngles.z - 270) * 2;

            Vector3 reflectionDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), -1);

            if (mirrorHit.collider)
            {
                 RaycastHit boatHit;
                Physics.Raycast(mirrorHit.point, reflectionDirection, out boatHit, Range, 1 << LayerMask.NameToLayer("Boat"));

                if (boatHit.collider != null && boatHit.collider.tag == "Boat")
                {
                    Debug.Log("boat 1 beam");
                }

                Debug.DrawRay(mirrorHit.point, reflectionDirection * Range);
            }

            Debug.DrawLine(sunPos, mirrorPos, Color.green);
            //Debug.Log(string.Format("SunPos: {0} - MirrorPos: {1} - Hit: {2} - ", sunPos, mirrorPos, mirrorHit.point));
        }


    }
}
//var angle = Vector2.Angle(sunPos, mirrorPos);
