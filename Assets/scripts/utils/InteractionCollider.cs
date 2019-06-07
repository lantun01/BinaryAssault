using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class InteractionCollider : MonoBehaviour
{
    private Interactuable interactuable;
    [SerializeField] private Player player;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Sprite defaultAction;

    private void InteractuarPlayer(Player p)
    {
        p.interactuable.Interactuar();
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        interactuable = other.GetComponent<Interactuable>();
        player.interactuable = interactuable;
        player.SetAction(interactuable.Interaccion);
        if (interactuable.interaccionIcon)
        {
        player.botonAccion.sprite = interactuable.interaccionIcon;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        player.botonAccion.sprite = defaultAction;
        player.interactuable = null;
        player.stateMachine.SetPlaying();
    }
}
