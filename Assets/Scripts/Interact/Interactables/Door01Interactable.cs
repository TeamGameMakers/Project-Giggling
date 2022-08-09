using GM;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interact
{
    /// <summary>
    /// 未完成。
    /// </summary>
    public class Door01Interactable : Interactable
    {
        public new Camera camera;

        // 门 Sprite
        public SpriteRenderer doorRenderer;
        public Sprite doorOpen;
        protected Collider2D doorBlock;
        
        // 窥探显示
        public SpriteRenderer targetRenderer;
        public GameObject peekPanel;
        protected bool timing = false;
        protected float curTime = 0;
        protected bool showImage = false;
        
        // 该交互对象上的所有组件
        private MonoBehaviour[] m_monos;    // 检索不到 Collider
        private Collider2D m_coll;
        
        protected override void Awake()
        {
            base.Awake();
            doorBlock = doorRenderer.gameObject.GetComponent<Collider2D>();
            m_monos = GetComponents<MonoBehaviour>();
            m_coll = GetComponent<Collider2D>();
        }

        protected override void Update()
        {
            base.Update();
            
            // 开始计时
            if (timing)
            {
                curTime += Time.deltaTime;
                // TODO: 根据时间不同，切换图片
            }

            // 按左键也可关闭窥探
            if (showImage && Mouse.current.leftButton.wasPressedThisFrame)
            {
                HidePeek();
            }
        }

        public override void Interact(Interactor interactor)
        {
            if (Keyboard.current.qKey.wasPressedThisFrame)
            {
                if (!showImage)
                    ShowPeek();
                else
                    HidePeek();
            }
            // 按 E 开门，在没有窥探时才行
            else if (!showImage)
            {
                // 切换为开门图片
                doorRenderer.sprite = doorOpen;
                // 关闭该交互
                foreach (var m in m_monos)
                    m.enabled = false;
                m_coll.enabled = false;
                transform.GetChild(0).gameObject.SetActive(false);
                // 关闭碰撞体
                doorBlock.enabled = false;
                
                // TODO: 在不正确的时候打开死亡
            }
        }

        public void StartTiming()
        {
            timing = true;
        }

        private void ShowPeek()
        {
            showImage = true;
            GameManager.SwitchGameState(GameState.UI, false);
            
            Vector3 mid = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            mid.z = 0;
            peekPanel.transform.position = mid;
            peekPanel.SetActive(true);
        }

        private void HidePeek()
        {
            showImage = false;
            GameManager.SwitchGameState(GameState.Playing, false);
            peekPanel.SetActive(false);
        }
    }
}
