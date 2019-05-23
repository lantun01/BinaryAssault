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
        dispararHash = animator? 0: Animator.StringToHash("disparar");
        
        if (data)
        {
            SetArma(data);
        }
    }
    

    public void AnimarDisparar()
    {
        if (animator!=null)
        {
        animator.SetTrigger(dispararHash);
        }
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
