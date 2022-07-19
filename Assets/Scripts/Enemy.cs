using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector2 _fisrtPoint;
    [SerializeField] private Vector2 _secondPoint;
    [SerializeField] float _speed;
    [SerializeField] private Character _character;
    [SerializeField] private bool _faceRight;

    private Transform _target;

    private void Start()
    {
        if (_faceRight == false)
        {
            transform.localScale *= new Vector2(-1, 1);
            _faceRight = true;
        }

        _faceRight = true;
        _target = new GameObject().transform;
        _target.position = _secondPoint;    
    }

    private void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Character>(out Character character) == true)
        {
            Destroy(character.gameObject);
            Instantiate(_character.gameObject, new Vector2(0,2), Quaternion.identity);
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);

        if (transform.position == _target.position)
        {
            transform.localScale *= new Vector2(-1, 1);
            _faceRight = !_faceRight;

            if (new Vector2(transform.position.x, transform.position.y) == _fisrtPoint)
                _target.position = _secondPoint;
            else
                _target.position = _fisrtPoint;
        }
    }
}
