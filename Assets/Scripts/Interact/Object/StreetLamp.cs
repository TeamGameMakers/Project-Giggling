using System;
using Characters.Player;
using UnityEngine;

namespace Interact
{
    // TODO: 添加怪物受伤
    public class StreetLamp : MonoBehaviour
    {
        protected Player player;

        public int damageToPlayer = 5;
        public int damageToMonster = 5;
        
        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                player = col.gameObject.GetComponent<Player>();
                player.PlayerEnterLight(damageToPlayer);
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                player.PlayerExitLight();
            }
        }
    }
}
