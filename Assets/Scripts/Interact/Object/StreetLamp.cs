using System;
using Characters.Monsters;
using Characters.Player;
using UnityEngine;

namespace Interact
{
    public class StreetLamp : MonoBehaviour
    {
        protected Player player;
        protected Monster monster;

        public float damageToPlayer = 5;
        public float damageToMonster = 5;

        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            AkSoundEngine.PostEvent("PlayerInTheLight", gameObject);
        }

        protected virtual void OnTriggerStay2D(Collider2D col)
        {
            Debug.Log("Damage");
            if (col.CompareTag("Player"))
            {
                player = col.gameObject.GetComponent<Player>();
                player.PlayerStayLight(damageToPlayer);
            }
            else if (col.CompareTag("Monster"))
            {
                monster = col.gameObject.GetComponent<Monster>();
                monster.MonsterStayRoadLight(damageToMonster);
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            AkSoundEngine.PostEvent("PlayerOutOfLight", gameObject);
            
            if (other.CompareTag("Player"))
            {
                player.PlayerExitLight();
            }
            else if (other.CompareTag("Monster"))
            {
                monster.MonsterExitRoadLight();
                monster = null;
            }
        }
    }
}
