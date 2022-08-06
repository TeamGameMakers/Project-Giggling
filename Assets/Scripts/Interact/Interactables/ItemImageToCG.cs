using GM;
using Save;
using Story;
using UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Interact
{
    public class ItemImageToCG : Interactable
    {
        public Sprite sprite;
        public CGPlayer cgPlayer;

        public bool setNativeSize = true;
        
        public Vector3 scale = Vector3.one;
        
        protected VideoPlayer player;

        protected override void Awake()
        {
            base.Awake();
            player = cgPlayer.gameObject.GetComponent<VideoPlayer>();
        }

        public override void Interact(Interactor interactor)
        {
            UIManager.Instance.ShowPanel<ItemImagePanel>("ItemImagePanel", callBack: panel => {
                panel.SetImage(sprite, scale, setNativeSize);
                // 绑定关闭后事件
                if (!SaveManager.GetBool(cgPlayer.SaveKey))
                {
                    panel.CgBtn.onClick.AddListener(() => {
                        RootCanvas.Instance.HideAll();
                        SaveManager.RegisterBool(cgPlayer.SaveKey);
                        player.loopPointReached += source => RootCanvas.Instance.ShowAll();
                        GameManager.SwitchGameState(GameState.CG);
                        player.Play();
                    });
                }
            });
        }
    }
}
