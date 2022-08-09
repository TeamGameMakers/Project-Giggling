using System.Collections;
using Data;
using UnityEngine;

namespace Characters.Monsters
{
    public class MonsterDieState : MonsterState
    {
        private readonly SpriteRenderer _spriteRenderer;

        public MonsterDieState(Monster monster, string name = null) : base(monster, name)
        {
            _spriteRenderer = _monster.GetComponent<SpriteRenderer>();
        }

        public override void Enter()
        {
            base.Enter();
            _monster.StartCoroutine(MonsterFade(_data.fadeSpeed));
            _monster.tag = "Untagged";
            AkSoundEngine.PostEvent("MonsterStopBurn", _monster.gameObject);

            switch (_data.monsterType)
            {
                case MonsterDataSO.MonsterType.Normal:
                    AkSoundEngine.PostEvent("C_dead", _monster.gameObject);
                    break;
                case MonsterDataSO.MonsterType.Elite:
                    AkSoundEngine.PostEvent("B_dead", _monster.gameObject);
                    break;
            }
        }

        private IEnumerator MonsterFade(float fadeSpeed)
        {
            var fadeColor = Color.white;
            _core.AIMovement.StopMoving();
            while (_spriteRenderer.color.a > 0)
            {
                fadeColor.a -= fadeSpeed * Time.deltaTime;
                _spriteRenderer.color = fadeColor;
                yield return null;
            }
            
            _monster.MonsterDie();
        }
    }
}