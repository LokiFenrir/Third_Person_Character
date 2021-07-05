using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviourPun
{
    //Vars

    Animator _anim;
    CharacterController _charaControl;
    Rigidbody _rb;

    [SerializeField]
    float _speed = 2.5f;

    [SerializeField]
    float _turn;

    [SerializeField]
    float _animSpeed;

    float _gravity = 9.8f;
    float floorDistance;

    Vector3 _verticalVector;


    bool _isGrounded()
    {

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit))
            return hit.distance <= floorDistance;

        return false;                                                                                         
                                                                                        // Used to find the ground by setting up a raycast to hit the ground below the plaeyr
                                                                                        // it then returns the positive value unless it is false then it returns false.

    }//_isGround


    //Funcs

    void Awake()
    {

        _anim = GetComponent<Animator>();
        _charaControl = GetComponent<CharacterController>();
        _verticalVector = Vector3.zero;
                                    
                                                                                                // Gets the following Componenets in the players functionailty.

    }//Awake


    void Update()
    {

        if (base.photonView.IsMine)
        {

            CharacterMover();
            VerticalMovement();
                                                                                                // Checks if the photonView belongs to the character, calls the functions.

        }

    }//Update


    void CharacterMover()
    {

        float hori = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
        float vert = Input.GetAxis("Vertical") * _speed * Time.deltaTime;

        Vector3 move = new Vector3(hori, 0 , vert);

        _charaControl.Move(move); 

        _anim.SetFloat("Speed", move == Vector3.zero ? 0f : 1f, _animSpeed, Time.deltaTime);

        if (move != Vector3.zero )
            RotateCharacter(move);

                                                                                                // Gets the floats if you are pressing up or down arrows, it then adds this to
                                                                                                // a new Vector3 with the above mentioned floats. It then moves the character
                                                                                                // controller foward with the new Vector3. The character is then moved by the
                                                                                                // the input and it affectes the animation blend tree. If the Vector3 is not null
                                                                                                // the charcater is rotated.

    }//CharacterMover


    void RotateCharacter(Vector3 moveVec)
    {

        Quaternion targetRot = Quaternion.LookRotation(moveVec, Vector3.up);

        Quaternion newRot = Quaternion.Lerp(transform.rotation, targetRot, _turn * Time.deltaTime);

        transform.rotation = newRot;

                                                                                                    // Takes in a Vector3 and turns the character to the Vector3 and turns to look
                                                                                                    // at the direction which the character is moving in.

    }//RotateCharacter


    void VerticalMovement()
    {

        if (_isGrounded())
        {

            if (_verticalVector.y < 0)
                _verticalVector.y = 0;

        }
        else
            _verticalVector.y -= _gravity * Time.deltaTime;

        _charaControl.Move(_verticalVector);

                                                                                                 // Checks if the character is grounded, if it is, the vertical vector will
                                                                                                 // be 0. If it is not grounded, gravity is applied. The character controller 
                                                                                                 // is then add by the _verticalVector.

    }//VerticalMovement


}// MOVEMENT
