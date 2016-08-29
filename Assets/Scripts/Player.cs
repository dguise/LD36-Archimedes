using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    internal class Player : MonoBehaviour
    {
        public bool _mirrorArea = false;
        public GameObject _currMirror;
        public bool _lockedToMirror = false;
        private Rigidbody2D _rb;
        public float Speed;

        public Animator _animator;
        void Start()
        {
            _rb = gameObject.GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;
            var y = Input.GetAxis("Vertical") * Time.deltaTime * Speed;

            if (_mirrorArea)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (_lockedToMirror)
                    {
                        _lockedToMirror = false;
                    }
                    else
                    {
                        _lockedToMirror = true;
                    }
                }
                
                if (_lockedToMirror)
                {
                    _rb.velocity = new Vector2(0, 0);
                    gameObject.transform.parent = _currMirror.transform;

                    _animator.SetBool("Focusing", true);
                    Vector3 rot = _currMirror.transform.eulerAngles;
                    float rot_sens = 0.2f;
                    var rot_y = -Input.GetAxis("Vertical")* rot_sens;

                    _currMirror.transform.eulerAngles = new Vector3(0, 0, rot.z + rot_y * 0.5f);


                    mirrorscript mirrScript = _currMirror.GetComponent<mirrorscript>();

                    float key_sens = 1f * Time.fixedDeltaTime;
                    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                    {
                        mirrScript.m_range += key_sens;
                    }
                    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                    {
                        mirrScript.m_range -= key_sens;
                    }

                    if (mirrScript.m_range < 1) mirrScript.m_range = 1;
                    if (mirrScript.m_range > 18) mirrScript.m_range = 18;
                }
            }
            
            if (!_lockedToMirror)
            {
                
                gameObject.transform.parent = null;
                if (x > 1 || x < -1 || y > 1 || y < -1)
                {
                    _animator.SetBool("Focusing", false);
                    _animator.SetBool("Moving", true);
                }
                else
                {
                    _animator.SetBool("Moving", false);
                }
                
                _rb.velocity = new Vector2(x, y);


                if (_rb.velocity.x > 0.5f || _rb.velocity.x < -0.5f || _rb.velocity.y < -0.5f || _rb.velocity.y > 0.5f)
                {
                    var angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg-90;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);    
                }

                //update link to mirrors
                GameObject[] mirrors;
                mirrors = GameObject.FindGameObjectsWithTag("Mirror");
                foreach (GameObject mirror in mirrors)
                {
                    mirror.GetComponent<mirrorscript>().obj_player = gameObject;
                }

            }
        }


        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "MirrorControlArea")
            {
                _currMirror = col.transform.parent.gameObject;

                //mirror must not be destroyed
                if(!_currMirror.GetComponent<mirrorscript>().is_destroyed)
                    _mirrorArea = true;
            }
        }

        /*void OnTriggerStay(Collider2D col)
        {
            if (col.tag == "MirrorControlArea")
            {
                _currMirror = col.transform.parent.gameObject;

                //mirror must not be destroyed
                if (!_currMirror.GetComponent<mirrorscript>().is_destroyed)
                    _mirrorArea = true;
            }
        }*/

        void OnTriggerExit2D(Collider2D col)
        {
            if (col.tag == "MirrorControlArea")
            {
                _mirrorArea = false;
            }
        }
    }
}
