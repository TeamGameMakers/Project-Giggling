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
