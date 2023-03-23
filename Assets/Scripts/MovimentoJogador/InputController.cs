using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    private PlayerControls _playerControls;


    void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    public Vector2 GetPlayerMoviment()
    {
        return _playerControls.Player.Move.ReadValue<Vector2>();
    }

    public bool GetPlayerJumpInThisFrame()
    {
        return _playerControls.Player.Jump.triggered;
    }

    public Vector2 GetPlayerLook()
    {
        return _playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool PegarItem()
    {
        return _playerControls.Player.PegarItem.triggered;
    }

    public bool DescartarItem()
    {
        return _playerControls.Player.Descartar.triggered;
    }

    public bool LightOnOff()
    {
        return _playerControls.Player.LightOnOff.triggered;
    }

    public int ItemMao()
    {
        if (_playerControls.Player.Item1.triggered)
        {
            return 0;
        }
        if (_playerControls.Player.Item2.triggered)
        {
            return 1;
        }
        if (_playerControls.Player.Item3.triggered)
        {
            return 2;
        }
        if (_playerControls.Player.Item4.triggered)
        {
            return 3;
        }
        if (_playerControls.Player.Item5.triggered)
        {
            return 4;
        }
        if (_playerControls.Player.Item6.triggered)
        {
            return 5;
        }
        return -1;
    }

    public bool ItemInventario()
    {
        return _playerControls.Player.Inventario.triggered;
    }

    public bool Agachar()
    {
        return _playerControls.Player.Agachar.triggered;
    }
    private void OnDisable()
    {
        _playerControls.Disable();
    }
}
