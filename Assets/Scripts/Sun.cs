using UnityEngine;

namespace Assets.Scripts
{
    public class Sun : MonoBehaviour
    {

        private GameObject[] mirrors;
        public float Speed = 1f;
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

        }

        void RaycastBeam(Vector2 sunPos, Vector2 mirrorPos)
        {
            RaycastHit mirrorHit;
            Vector3 direction = mirrorPos - sunPos;
            Physics.Raycast(sunPos, direction, out mirrorHit, 1 << LayerMask.NameToLayer("Mirror"));

            if (mirrorHit.collider)
            {
                RaycastHit boatHit;
                Physics.Raycast(mirrorHit.point, mirrorHit.normal * 100f, out boatHit, 1 << LayerMask.NameToLayer("Boat"));

                if (boatHit.collider != null && boatHit.collider.tag == "Boat")
                {
                    Debug.Log("boat 1 beam");
                }

                Debug.DrawRay(mirrorHit.point, mirrorHit.normal * 100f);
            }

            Debug.DrawLine(sunPos, mirrorPos, Color.green);
            Debug.Log(string.Format("SunPos: {0} - MirrorPos: {1} - Hit: {2} - ", sunPos, mirrorPos, mirrorHit.point));
        }


    }
}
//var angle = Vector2.Angle(sunPos, mirrorPos);
