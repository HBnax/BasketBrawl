using System;
using UnityEngine;

public class NetTrigger : MonoBehaviour
{
   public HoopController hoop;

   private void OnTriggerEnter2D(Collider2D obj)
   {
      hoop?.OnNetEnter(obj);
   }
}
