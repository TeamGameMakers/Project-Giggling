using System;
using UnityEngine;

namespace Puzzle
{
    public class PinLockUnlockZone : MonoBehaviour
    {
        private bool m_inside = false;
        public bool Inside => m_inside;

        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("碰撞体进入: " + col.gameObject.name);
            if (col.gameObject.CompareTag("PuzzleKey"))
                m_inside = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("PuzzleKey"))
                m_inside = false;
        }
    }
}