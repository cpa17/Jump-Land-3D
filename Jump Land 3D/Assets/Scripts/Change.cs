using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Change : MonoBehaviour
{
    [SerializeField] private Transform newCheckpoint;
    [SerializeField] private Transform cam;
    [SerializeField] private int value;
    
    private float _turnSmooth;
    
    private void OnTriggerEnter(Collider other)
    {
        Water.Checkpoint = newCheckpoint;
        
        cam.GetComponentInChildren<CinemachineFreeLook>().m_XAxis.Value = value;

    }
}
