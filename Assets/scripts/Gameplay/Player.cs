using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public PlayerInput input;
    [HideInInspector]
    public Rigidbody2D rb;
    public Animator animator;
    public Arma arma;
    public int hastCaminar;
    public float velocidad;
    public LayerMask layermask;
    public Vector3[] posRecord = new Vector3[60];
    public TransformVariable transformValue;
    public CinemachineTargetGroup cineGroup;



    private PlayerStateMachine state = new PlayerStateMachine();
    private Material material;
    private int hologramId;
    private TrailRenderer trail;
    private Enemigo objetivo;
    private Enemigo previousEnemy;
    private CinemachineTargetGroup.Target target = new CinemachineTargetGroup.Target();
    private int posCounter;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        input = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
        state.Inicializar();
        hastCaminar = Animator.StringToHash("caminando");
        material = GetComponent<SpriteRenderer>().material;
        hologramId = Shader.PropertyToID("_Hologram_Value_1");
        trail = GetComponent<TrailRenderer>();
        posRecord[0] = transform.position;
        posCounter++;
        transformValue.value = transform;
        target.weight = 1;
    }

    private void Update()
    {
        state.Act(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ArmaDrop>())
        {
            CambiarArma(collision.GetComponent<ArmaDrop>().Recoger());
        }
        else
        {

        }
    }

    internal void VoltearSprite(bool voltear)
    {

        if (objetivo)
        {
            if (ObjetivoDerecha())
            {
                Voltear(false);
            }
            else
            {
                Voltear(true);
            }
        }
        else
        {
            Voltear(voltear);
        }
    }

    private void Voltear(bool voltear)
    {
        GetComponent<SpriteRenderer>().flipX = voltear;
        arma.VolvearSprite(voltear);

        if (voltear)
        {
            arma.transform.localPosition = new Vector3(-0.3f, 0, 0);
        }
        else
        {
            arma.transform.localPosition = new Vector3(0.3f, 0, 0);
        }
    }

    internal void Disparar()
    {
        if (objetivo)
        {
            arma.Disparar((objetivo.transform.position-arma.transform.position).normalized);
        }
        else
        {
            arma.Disparar(input.mirada);

        }
    }

    internal void Mover()
    {
        rb.Mover(input.movimiento, velocidad);
        RotarArma();
        EscanearObjetivo();
    }

    internal void RotarArma()
    {
        if (objetivo)
        {
            arma.ActivarMira();
            arma.ActualizarMira(objetivo.transform.position);
            arma.transform.Rotar(objetivo.transform.position-arma.transform.position);
            Debug.DrawLine(arma.transform.position, objetivo.transform.position, Color.yellow);
        }
        else
        {
            arma.DesactivarMira();
            arma.transform.Rotar(input.mirada);
        }
    }

    internal void Dash()
    {
        animator.SetBool("dashing", true);
        trail.emitting = true;
        material.SetFloat(hologramId, 1);
        StartCoroutine(state.SetWait(1f));
        StartCoroutine(ChangeProperty(hologramId, 1f));
        rb.Mover(input.mirada, velocidad*5);
    }


    internal IEnumerator ChangeProperty(int id, float time)
    {
        material.SetFloat(id, 1);
        for (int i = 0; i < time*10; i++)
        {
            yield return null;
        }
        material.SetFloat(id, 0);
        animator.SetBool("dashing", false);
        trail.emitting = false;
    }

    private void EscanearObjetivo()
    {
        if (EnemyManager.instance)
        {
            objetivo = EnemyManager.instance.GetNerbyEnemy(transform.position);
            if (objetivo!=previousEnemy)
            {
                previousEnemy = objetivo;
                CambiarObjetivo();
            }
        }
    }

    private void CambiarObjetivo()
    {
        target.target = objetivo.transform;
        cineGroup.m_Targets[1] = target;
    }

    private bool ObjetivoDerecha()
    {
            return transform.position.x < objetivo.transform.position.x;
       
    }

    internal void CambiarArma(ArmaData data)
    {
        arma.SetArma(data);

    }
}