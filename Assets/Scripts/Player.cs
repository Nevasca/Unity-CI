using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private float _rotationSpeed = 10f;

    private const string INPUT_HORIZONTAL = "Horizontal";
    private const string INPUT_VERTICAL = "Vertical";

    private Rigidbody _rigidbody;
    private Transform _camera;

    private Vector2 _input = Vector2.zero;
    private Vector3 _inputDirection = Vector3.zero;
    private Quaternion _rotation = Quaternion.identity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main.transform;
    }

    private void Update()
    {
        SetInputs();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void SetInputs()
    {
        _input.y = Input.GetAxisRaw(INPUT_VERTICAL);
        _input.x = Input.GetAxisRaw(INPUT_HORIZONTAL);

        _inputDirection = _input.y * _camera.forward + _input.x * _camera.right;
        _inputDirection.y = 0f;
    }

    private void Move()
    {
        _rigidbody.MovePosition(transform.position + _moveSpeed * Time.deltaTime * _inputDirection);
    }

    private void Rotate()
    {
        if (_inputDirection == Vector3.zero)
            return;

        _rotation = Quaternion.Slerp(_rigidbody.rotation, Quaternion.LookRotation(_inputDirection), _rotationSpeed * Time.deltaTime);
        _rigidbody.MoveRotation(_rotation);
    }
}