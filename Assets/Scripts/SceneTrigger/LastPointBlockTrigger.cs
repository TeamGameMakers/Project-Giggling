using System;
using Characters.Monsters;
using UnityEngine;

namespace SceneTrigger
{
    public class LastPointBlockTrigger : MonoBehaviour
    {
        public GameObject block;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (Monster.Monsters.Count == 0)
            {
                block.SetActive(false);
            }
        }
    }
}
