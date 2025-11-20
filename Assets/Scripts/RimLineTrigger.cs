using System;
using UnityEngine;

public class RimLineTrigger : MonoBehaviour
{
    public HoopController hoop;

    private void OnTriggerEnter2D(Collider2D obj)
    {
        hoop?.OnRimEnter(obj);
    }
    private void OnTriggerExit2D(Collider2D obj)
    {
        hoop?.OnRimExit(obj);
    }
}
