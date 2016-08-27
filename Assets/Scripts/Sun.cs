using UnityEngine;

namespace Assets.Scripts
{
    public class Sun : MonoBehaviour
    {

        private GameObject mirror;

        // Use this for initialization
        void Start () {
            mirror = GameObject.FindGameObjectWithTag("Mirror");
        }
	
        // Update is called once per frame
        void Update ()
        {
            Vector2 sunPos = gameObject.transform.position;        
            Vector2 mirrorPos = mirror.transform.position;


            RaycastHit mirrorHit;
            Vector3 direction = mirrorPos - sunPos;
            Physics.Raycast(sunPos, direction, out mirrorHit, 1 << LayerMask.NameToLayer("Mirror"));

            if (mirrorHit.collider)
            {
                RaycastHit boatHit;
                Physics.Raycast(mirrorHit.point, mirrorHit.normal*100f, out boatHit, 1 << LayerMask.NameToLayer("Boat"));

                if (boatHit.collider != null && boatHit.collider.tag == "Boat")
                {
                    Debug.Log("boat 1 beam");
                }

                Debug.DrawRay(mirrorHit.point, mirrorHit.normal*100f);
            }

            Debug.DrawLine(sunPos, mirrorPos, Color.green);
            Debug.Log(string.Format("SunPos: {0} - MirrorPos: {1} - Hit: {2} - ", sunPos, mirrorPos, mirrorHit.point));
        }

    }
}
//var angle = Vector2.Angle(sunPos, mirrorPos);
