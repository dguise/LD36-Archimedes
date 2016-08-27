using UnityEngine;

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
                RaycastBeam(gameObject.transform.position, mirror.transform.position);
            }
            transform.position = transform.position + Vector3.down * Time.deltaTime * Speed;
            Debug.Log(1 << LayerMask.NameToLayer("Mirror"));

        }

        void RaycastBeam(Vector2 sunPos, Vector2 mirrorPos)
        {
            RaycastHit mirrorHit;
            Vector3 direction = mirrorPos - sunPos;
            
            Physics.Raycast(sunPos, direction, out mirrorHit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Mirror"));
            

            if (mirrorHit.collider)
            {
                RaycastHit boatHit;
                Physics.Raycast(mirrorHit.point, mirrorHit.normal, out boatHit, Range, 1 << LayerMask.NameToLayer("Boat"));

                if (boatHit.collider != null && boatHit.collider.tag == "Boat")
                {
                    Debug.Log("boat 1 beam");
                }

                Debug.DrawRay(mirrorHit.point, mirrorHit.normal * Range);
            }

            Debug.DrawLine(sunPos, mirrorPos, Color.green);
            //Debug.Log(string.Format("SunPos: {0} - MirrorPos: {1} - Hit: {2} - ", sunPos, mirrorPos, mirrorHit.point));
        }


    }
}
//var angle = Vector2.Angle(sunPos, mirrorPos);
