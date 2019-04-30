﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using Utils;

public class Player : Character, IUpdateable
{
    // Start is called before the first frame update
    [HideInInspector]
    public int hashCaminar;
    [HideInInspector]
    public int hashMelee;
    [HideInInspector]
    public int hashDash;
    public float dashSpeed;
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
        target.weight = 5;
        polvoEmission = polvoCaminar.emission;
        dashEmission = dashTrail.emission;
        Inicializar();
        Subscribir();
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
        //rb.Mover(joystick.Direction, velocidad);
        RotarArma();
        EscanearObjetivo();
        transform.Mover(joystick.Direction, 2);
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
            material.SetFloat(hologramId, 1);
            material.SetFloat(blendOutlineId, 1);
            StartCoroutine(stateMachine.SetWait(dashTime));
        }

        void DashEnd()
        {
            material.SetFloat(hologramId, 0);
            material.SetFloat(blendOutlineId, 0);
            animator.SetBool(hashDash, false);
        }


        // dashSkill.Execute(this, DashStart, DashEnd);
        DashStart();
        StartCoroutine(Corroutines.DashTransform(transform,joystick.mirada, dashTime, dashSpeed));
        StartCoroutine(Corroutines.Wait(dashTime, () => dashEmission.enabled = true, () => dashEmission.enabled = false));
        StartCoroutine(Corroutines.Wait(dashTime, DashStart, DashEnd));
    }

    private void EscanearObjetivo()
    {
        if (EnemyManager.instance)
        {
            objetivo = EnemyManager.instance.GetNerbyEnemy(transform.position);
            if (objetivo && objetivo!=previousEnemy)
            {
                target.weight = 5;
                previousEnemy = objetivo;
                CambiarObjetivo();
            }
            else
            {
                target.weight = 0;
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

    public void CustomUpdate()
    {
        stateMachine.Act(this);
    }

    public void Subscribir()
    {
        UpdateManager.instance.Subscribe(this);
    }
}