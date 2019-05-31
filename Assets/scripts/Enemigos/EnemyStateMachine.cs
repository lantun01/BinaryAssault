using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [HideInInspector] public Enemigo enemy;
    [HideInInspector] public GameObject player;
    [HideInInspector] public Transform attackTarget;
    public Arma arma;
    public EnemyState currentState;
    public float distanciaAtaque;
    public float distanciaCuerpo;
    public EnemyState remainState;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<Enemigo>();
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void TransitionToState(EnemyState nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
        }
    }
}
