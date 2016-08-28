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
        private bool _mirrorArea = false;
        private GameObject _currMirror;
        private bool _lockedToMirror = false;
        private Rigidbody2D _rb;
        public float Speed;

        private Animator _animator;
        void Start()
        {
            _rb = gameObject.GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            LineRenderer line1 = new LineRenderer();
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
                    var rot_y = Input.GetAxis("Vertical");

                    _currMirror.transform.eulerAngles = new Vector3(0, 0, rot.z + rot_y*0.5f);
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
                
            }
        }


        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Mirror")
            {
                _currMirror = col.transform.parent.gameObject;
                _mirrorArea = true;
            }
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if (col.tag == "Mirror")
            {
                _mirrorArea = false;
            }
        }
    }
}
