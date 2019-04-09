using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Player : Character
{
    // Start is called before the first frame update
    [HideInInspector]
    public PlayerInput input;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public int hashCaminar;
    [HideInInspector]
    public int hashMelee;
    public Animator animator;
    public Arma arma;
    public LayerMask layermask;
    public TransformVariable transformValue;
    public CinemachineTargetGroup cineGroup;
    public GameEvent RecibirAtaque;
    public FloatVariable saludRatio;

    [SerializeField]
    private Image botonArma;
    private List<ArmaData> armas = new List<ArmaData>();
    private int armaActual;
    private int cantidadArmas;
    private PlayerStateMachine stateMachine = new PlayerStateMachine();
    private Material material;
    private int hologramId;
    private TrailRenderer trail;
    private Enemigo objetivo;
    private Enemigo previousEnemy;
    private CinemachineTargetGroup.Target target = new CinemachineTargetGroup.Target();

    private void Awake()
    {
        animator = GetComponent<Animator>();
        input = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
        stateMachine.Inicializar();
        hashCaminar = Animator.StringToHash("caminando");
        hashMelee = Animator.StringToHash("melee");
        material = GetComponent<SpriteRenderer>().material;
        hologramId = Shader.PropertyToID("_Hologram_Value_1");
        trail = GetComponent<TrailRenderer>();
        transformValue.value = transform;
        target.weight = 1;
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
        StartCoroutine(stateMachine.SetWait(1f));
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
            if ((transform.position-objetivo.transform.position).sqrMagnitude<4)
            {
                animator.SetTrigger(hashMelee);
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

        //armaActual = armaActual == (cantidadArmas - 1 )? 0 : armaActual++;

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
}