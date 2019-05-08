using UnityEngine;
using  UnityEngine.Events;


[System.Serializable]
public class MyPlayerEvent: UnityEvent<Player>
{
   
}
public class Interactuable : MonoBehaviour
{
   
   [SerializeField] private UnityEvent interactuar;
   [SerializeField] private MyPlayerEvent interaccion;

   public Sprite interaccionIcon;
   

   public void Interactuar()
   {
      interactuar?.Invoke();
   }

   public void Interaccion(Player p)
   {
      interaccion.Invoke(p);
   }
}
