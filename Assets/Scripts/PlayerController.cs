using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputController _inputController;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private Transform _playerRespawn;

    private void Awake()
    {
        _inputController = GetComponent<InputController>();
    }

    private void Start()
    {
        _player = Instantiate(_player, _playerRespawn.position, Quaternion.identity);
    }

    private void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        _player.SetVelocity(_inputController.GetVelocity());

        _player.Rotate(_inputController.GetTurn());
    }
}
