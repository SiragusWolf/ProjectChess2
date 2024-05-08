using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject _Player;
    private Vector3 _playerPosition;

    private void Awake()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        _playerPosition = _Player.transform.position;
        transform.position = new Vector3(_playerPosition.x, _playerPosition.y, transform.position.z);
    }

    private void Update()
    {
        _playerPosition = _Player.transform.position;
        transform.position = new Vector3(_playerPosition.x, _playerPosition.y, transform.position.z);
    }

    /*private void OnEnable()
    {
        PlayerMovement.onPlayerMoved += MoveCamera;
    }

    private void OnDisable()
    {
        PlayerMovement.onPlayerMoved -= MoveCamera;
    }

    private void MoveCamera()
    {
        transform.position = new Vector3(_playerPosition.x, _playerPosition.y, transform.position.z);
    }*/
}
