using GM;
using Save;
using Story;
using UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Interact
{
    public class ItemImageToCg : Interactable
    {
        public Sprite sprite;
        public CGPlayer cgPlayer;

        protected VideoPlayer player;
        
        public override void Interact(Interactor interactor)
        {
            UIManager.Instance.ShowPanel<ItemImagePanel>("ItemImagePanel", callBack: panel => {
                panel.SetImage(sprite);
                // 绑定关闭后事件
                if (!SaveManager.GetBool(cgPlayer.SaveKey))
                {
                    Button cgBtn = panel.transform.Find("CloseBgBtn").GetComponent<Button>();
                    cgBtn.onClick.AddListener(() => {
                        RootCanvas.Instance.HideAll();
                        SaveManager.RegisterBool(cgPlayer.SaveKey);
                        player.loopPointReached += source => RootCanvas.Instance.ShowAll();
                        player.Play();
                    });
                }
            });
        }
    }
}
