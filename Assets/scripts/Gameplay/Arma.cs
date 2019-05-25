using DG.Tweening;
using UnityEngine;
using  Utils;

[RequireComponent(typeof(AudioSource))]
public class Arma : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Vector3 posicionDerecha;
    public Vector3 posicionIzquierda;
    [SerializeField]
    private LineRenderer mira;

    [Header("Parámetros animación de disparo")]
    [SerializeField] private float animDuration,animElasticity;
    [SerializeField] private int animVibrato;
    [SerializeField] private Vector3 animationScale;

    
   [SerializeField] private ArmaData data;
    private int dispararHash;
    private bool armado;
    private Animator animator;
    private bool cooldown;
    private AudioSource _audioSource;
    

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        mira = GetComponent<LineRenderer>();
        dispararHash = animator?  Animator.StringToHash("disparar"):-1;
        
        if (data)
        {
            SetArma(data);
        }
    }
    

    public void AnimarDisparar()
    {
//        if (dispararHash!=-1)
//        {
//        animator.SetTrigger(dispararHash);
//        }

        transform.DOPunchScale(animationScale, animDuration, animVibrato, animElasticity);
    }

    public void VolvearSprite(bool value)
    {
        sprite.flipY = value;
        if (value)
        {
            transform.localPosition = posicionIzquierda;

        }
        else
        {
            transform.localPosition = posicionDerecha;
        }
    }

    public void ActivarMira()
    {
        if (data.tieneMira)
        {
        mira.enabled = true;
        }
    }

    public void DesactivarMira()
    {
        if (data && data.tieneMira)
        {
        mira.enabled = false;
        }
    }

    public void SetArma(ArmaData armaData)
    {
        data = armaData;
        sprite.sprite = data.sprite;
        _audioSource.clip = armaData.audio;
    }

    public void ActualizarMira(Vector3 posicion)
    {
        if (data && data.tieneMira)
        {
        mira.SetPosition(0, transform.position);
        mira.SetPosition(1, posicion);
        }
    }

    internal void Disparar(Vector3 mirada, float damageModifier)
    {
        if (cooldown || !data) return;
        AnimarDisparar();
        _audioSource.Play();
        float damage = data.damage + damageModifier;
        data.Disparar(transform, mirada, damage );
        StartCoroutine(Corroutines.Wait(data.fireRate,() => cooldown = true,()=>cooldown = false));
    }

    internal void Disparar(Vector3 mirada)
    {
        Disparar(mirada,0);
    }
}
