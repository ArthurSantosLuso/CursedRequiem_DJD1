using System;
using Unity.VisualScripting;
using UnityEngine;

public class TrailRendererScript : MonoBehaviour
{
    [SerializeField]
    private TrailRenderer tr;


    private void Start()
    {
        tr = GetComponent<TrailRenderer>();
        tr.emitting = false;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Fui Chamado");
            if (tr.emitting)
            {
                tr.emitting = false;                
            }
            else
            {
                tr.emitting = true;
            }
        }
    }
}
