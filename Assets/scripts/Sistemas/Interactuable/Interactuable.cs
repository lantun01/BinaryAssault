using UnityEngine;
using  UnityEngine.Events;


[System.Serializable]
public class MyPlayerEvent: UnityEvent<Player>
{
   
}
public class Interactuable : MonoBehaviour
{
   [Tooltip("Icono que motrará el botón de interacción")]
   public Sprite interaccionIcon;
   [SerializeField] private UnityEvent interactuar;
   [SerializeField] private MyPlayerEvent interaccion;

   

   public void Interactuar()
   {
      interactuar?.Invoke();
   }

   public void Interaccion(Player p)
   {
      interactuar?.Invoke();
      interaccion.Invoke(p);
   }
}
