using System;
using Player;
using UnityEngine;

namespace Interact
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class InteractTrigger: MonoBehaviour
    {
        private Interaction _interaction;

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            
        }
    }
}