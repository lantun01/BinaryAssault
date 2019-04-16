using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public TransformVariable playerTransform;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((playerTransform.value.position - transform.position).normalized * Time.deltaTime*2);
    }

    public Vector3 TargetPos()
    {
        Vector3 pos = new Vector3();
        pos.x = transform.position.x;
        pos.y = sprite.bounds.min.y;
        return pos;

    }
}
