using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    public float horizontal { get => Input.GetAxis("Horizontal"); }
    public float vertical { get => Input.GetAxis("Vertical"); }

    public bool skill1 { get => Input.GetButtonDown("Skill1"); } 

    public bool dash { get => Input.GetButtonDown("Dash"); }

    public bool disparar { get => Input.GetButtonDown("Disparar"); }

    public Vector2 movimiento { get
           { _movimiento.x = horizontal;
            _movimiento.y = vertical;
            if (_movimiento.sqrMagnitude != 0)
            {
                _mirada = _movimiento.normalized;
            }
            return _movimiento.normalized; }
         }

    public Vector2 mirada { get=>_mirada;}
    private Vector2 _mirada = Vector2.right;

    private Vector3 _movimiento = new Vector3();
    public bool Moviendo { get => _movimiento.sqrMagnitude > 0; }

}
