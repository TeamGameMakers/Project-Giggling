using System;
using Characters.Monsters;
using Characters.Player;
using GM;
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
            // 如果是 UI 或 CG 状态则不计算伤害
            if (GameManager.State == GameState.UI || GameManager.State == GameState.CG)
            {
                return;
            }
            
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
