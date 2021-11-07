using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    public float MoveSpeed;
    private bool movespeedup = false;

    private float originallySpeed;

    private Rigidbody myRigid;

    [SerializeField]
    private float lookSensitivity;

    private void Awake()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Vector3 position = new Vector3(-23f, 1f, -23f);
        this.transform.position = position;

        originallySpeed = MoveSpeed;
    }

    private void Update()
    {
        Move();
        CharacterRotation();
    }

    //플레이어 방향키로 움직임(W,S,A,D)
    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirY = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            MoveSpeed = (MoveSpeed / 2) + originallySpeed;
            movespeedup = true;
        }

        if (movespeedup)
        {
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                MoveSpeed = originallySpeed;
                movespeedup = false;
            }
        }

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirY;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * MoveSpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    //좌우 캐릭터 회전
    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;

        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }
}