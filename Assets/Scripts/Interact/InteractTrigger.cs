using System;
using UnityEngine;

namespace Interact
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class InteractTrigger: MonoBehaviour
    {
        private Interaction _interaction;
    }
}