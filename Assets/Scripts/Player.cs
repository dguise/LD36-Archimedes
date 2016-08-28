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

        public float Speed;
        
        void Start()
        {

        }

        void Update()
        {
            var x = Input.GetAxis("Horizontal")*Time.deltaTime * Speed;
            var y = Input.GetAxis("Vertical")*Time.deltaTime * Speed;

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
                    Vector3 rot = _currMirror.transform.eulerAngles;
                    var rot_y = Input.GetAxis("Vertical");

                    _currMirror.transform.eulerAngles = new Vector3(0, 0, rot.z + -rot_y);
                }
            }
            
            if (!_lockedToMirror)
            {
                transform.Translate(x, y, 0);
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
