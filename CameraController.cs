using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviourPun
{
    //Vars

    [SerializeField]
    Transform _camera;

    [SerializeField]
    Vector3 _camOffset;

    [Range(0.01f, 1f)]
    public float _smoothnees = 0.5f;

    public bool _lookPlayer = false;

    //Func

    void Start()
    {

        _camera = Camera.main.transform;


        if (base.photonView.IsMine)
        {


            _camOffset = new Vector3(0, 1.9f, -2f);

        }
        

    }//Start


    void LateUpdate()
    {


       if (base.photonView.IsMine)
        {
            Vector3 newPos = transform.position + _camOffset;

            _camera.position = Vector3.Slerp(_camera.position, newPos, _smoothnees);

            if (_lookPlayer)
                transform.LookAt(transform);
        }

    }//LateUpdate


}// CAMERA CONTROLLER
