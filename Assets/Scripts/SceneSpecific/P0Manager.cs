using System.Collections.Generic;
using Save;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace SceneSpecific
{
    public class P0Manager : MonoBehaviour
    {
        public new Camera camera;
        public new Transform light;
        public SpriteRenderer image;

        public List<Sprite> notClearSprites = new List<Sprite>();
        public List<Sprite> clearSprites = new List<Sprite>();

        private void Start()
        {
            UIManager.Instance.ShowPanel<StartPanel>("StartPanel");
            UIManager.Instance.HidePanel("GamePanel", true);
            // 设置图片
            if (SaveManager.GetGameClear() == 1)
            {
                image.sprite = clearSprites[Random.Range(0, clearSprites.Count)];
            }
            else
            {
                image.sprite = notClearSprites[Random.Range(0, notClearSprites.Count)];
            }
        }

        private void Update()
        {
            // 在鼠标位置发光
            Vector3 pos = camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            pos.z = 0;
            light.position = pos;
        }
    }
}
