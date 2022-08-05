using GM;
using Save;
using Story;
using UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Interact
{
    public class ItemCGInteractable : ItemInteractable
    {
        public CGPlayer cgPlayer;
        protected VideoPlayer player;

        protected override void Awake()
        {
            base.Awake();
            player = cgPlayer.gameObject.GetComponent<VideoPlayer>();
        }

        public override void Interact(Interactor interactor)
        {
            Debug.Log("与 CG 道具互动");
            UIManager.Instance.ShowPanel<ItemInfoPanel>("ItemInfoPanel", callBack: panel => {
                panel.UpdateInfo(item);
                Debug.Log(SaveManager.GetBool(cgPlayer.SaveKey));
                if (!SaveManager.GetBool(cgPlayer.SaveKey))
                {
                    Debug.Log("开始播放 CG");
                    Button cgBtn = panel.transform.Find("CloseBgBtn").GetComponent<Button>();
                    cgBtn.onClick.AddListener(() => {
                        RootCanvas.Instance.HideAll();
                        player.loopPointReached += source => RootCanvas.Instance.ShowAll();
                        SaveManager.RegisterBool(cgPlayer.SaveKey);
                        player.Play();
                    });
                }
            });
        }
    }
}
