﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Player : Character
{
    // Start is called before the first frame update
    [HideInInspector]
    public int hashCaminar;
    [HideInInspector]
    public int hashMelee;
    [HideInInspector]
    public int hashDash;
    public Animator animator;
    public Arma arma;
    public LayerMask layermask;
    public TransformVariable transformValue;
    public CinemachineTargetGroup cineGroup;
    public GameEvent RecibirAtaque;
    public GameEvent disparar;
    public FloatVariable saludRatio;
    public ParticleSystem dashTrail;
    public DashSkill dashSkill;
    public bool enCombate;

    [SerializeField]
    private Image botonArma;
    [SerializeField]
    private float dashTime;
    public FloatingJoystick joystick;

    private List<ArmaData> armas = new List<ArmaData>();
    public ParticleSystem polvoCaminar;
    private ParticleSystem.EmissionModule polvoEmission;
    private ParticleSystem.EmissionModule dashEmission;
    private int armaActual;
    private int cantidadArmas;
    private PlayerStateMachine stateMachine = new PlayerStateMachine();
    private Material material;
    private int hologramId, blendOutlineId; //Shaders prop
    private TrailRenderer trail;
    private Enemigo objetivo;
    private Enemigo previousEnemy;
    private CinemachineTargetGroup.Target target = new CinemachineTargetGroup.Target();
    private SpriteRenderer spriteRenderer;
    private delegate void AccionPostDash();
    private bool armado;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine.Inicializar();
        hashCaminar = Animator.StringToHash("caminando");
        hashMelee = Animator.StringToHash("melee");
        hashDash = Animator.StringToHash("dashing");
        material = GetComponent<SpriteRenderer>().material;
        hologramId = Shader.PropertyToID("_Hologram_Value_1");
        blendOutlineId = Shader.PropertyToID("_OperationBlend_Fade_1");
        trail = GetComponent<TrailRenderer>();
        transformValue.value = transform;
        target.weight = 1;
        polvoEmission = polvoCaminar.emission;
        dashEmission = dashTrail.emission;
        Inicializar();
    }

    private void Update()
    {
        stateMachine.Act(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ArmaDrop>())
        {
            AgregarArma(collision.GetComponent<ArmaDrop>().Recoger());
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
        spriteRenderer.flipX = voltear;
        arma.VolvearSprite(voltear);

        if (voltear)
        {
            arma.transform.localPosition = new Vector3(-0.13f, -0.25f, 0);
        }
        else
        {
            arma.transform.localPosition = new Vector3(0.13f, -0.25f, 0);
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
            arma.Disparar(joystick.mirada);

        }

        disparar?.Raise();
    }

    internal void Mover()
    {
        rb.Mover(joystick.Direction, velocidad);
        RotarArma();
        EscanearObjetivo();
    }

    internal void RotarArma()
    {
        if (objetivo)
        {
            arma.ActualizarMira(objetivo.transform.position);
            arma.transform.Rotar(objetivo.transform.position-arma.transform.position);
        }
        else
        {
            arma.DesactivarMira();
            arma.transform.Rotar(mirada);
        }
    }

    public void Dash()
    {

        void DashStart()
        {
            animator.SetBool(hashDash, true);
            //trail.emitting = true;
            material.SetFloat(hologramId, 1);
            material.SetFloat(blendOutlineId, 1);
            dashEmission.enabled = true;
            StartCoroutine(stateMachine.SetWait(dashTime, () => { dashEmission.enabled = false; }));
        }

        void DashEnd()
        {
            StartCoroutine(ChangeProperty(hologramId, dashTime));
            StartCoroutine(ChangeProperty(blendOutlineId, dashTime));
        }
        dashSkill.Execute(this, DashStart, DashEnd);
    }



    internal IEnumerator ChangeProperty(int id, float time)
    {
        material.SetFloat(id, 1);
        for (int i = 0; i < time*10; i++)
        {
            yield return null;
        }
        material.SetFloat(id, 0);
        animator.SetBool(hashDash, false);
        trail.emitting = false;
    }

    private IEnumerator Translate(Transform trans, Vector3 direccion, float velocidad, float tiempo)
    {
        Vector3 dir = direccion;
        velocidad = velocidad / (tiempo * 30);
        for (int i = 0; i < tiempo*30; i++)
        {
            trans.position += dir * velocidad;
            yield return null;
        }
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

    public void AgregarArma(ArmaData armaData)
    {
        cantidadArmas++;
        armaActual = cantidadArmas-1;
        armas.Add(armaData);
        CambiarArma(armaData);
    }

    public void SiguienteArma()
    {
        if (armaActual == cantidadArmas - 1)
        {
            armaActual = 0;
        }
        else
        {
            armaActual++;
        }

        if (cantidadArmas>0)
        {
            CambiarArma(armas[armaActual]);
        }

    }

    internal void CambiarArma(ArmaData data)
    {
        arma.SetArma(data);
        botonArma.sprite = data.sprite;

    }

    public void GetDamage(int cantidad)
    {
        saludActual -= cantidad;
        saludRatio.value = GetSaludRatio();
        RecibirAtaque?.Raise();

    }

    public void Actuar()
    {
        stateMachine.act(this);
    }

    public void SetEmisionPolvo(bool value)
    {
        polvoEmission.enabled = value;
    }

    public void EntrarEnCombate()
    {
        enCombate = true;
        arma.ActivarMira();
    }

    public void AbandonarCombate()
    {
        enCombate = false;
        arma.DesactivarMira();
    }
}