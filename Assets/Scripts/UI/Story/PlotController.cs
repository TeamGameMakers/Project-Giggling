using System.Collections.Generic;
using Data.Story;
using Story;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlotController : IStoryProcessor
    {
        protected string panelName;
        protected Image leftImage;
        protected GameObject leftImageGo;
        protected Image rightImage;
        protected GameObject rightImageGo;
        protected TextMeshProUGUI tmp;
        protected GameObject tmpGo;
        protected IChoiceList cList;
        protected GameObject cListGo;
        protected PlotSection currentSecion;

        public PlotController(string panelName)
        {
            this.panelName = panelName;
        }
        
        public void Register(Image image, bool left, GameObject parentGo = null)
        {
            if (left)
            {
                leftImage = image;
                leftImageGo = (parentGo ? parentGo : image.gameObject);
            }
            else
            {
                rightImage = image;
                rightImageGo = (parentGo ? parentGo : image.gameObject);
            }
        }

        public void Register(TextMeshProUGUI tmp, GameObject parentGo = null)
        {
            this.tmp = tmp;
            tmpGo = (parentGo ? parentGo : tmp.gameObject);
        }

        public void Register(IChoiceList list, GameObject parentGo)
        {
            cList = list;
            cListGo = parentGo;
        }
        
        public void Process(PlotSection section)
        {
            currentSecion = section;
            section.action?.Invoke();

            if (!string.IsNullOrEmpty(section.text))
            {
                tmpGo.SetActive(true);
                tmp.SetText(section.text);
            }
            else
                tmpGo.SetActive(false);

            if (section.leftSprite != null)
            {
                leftImageGo.SetActive(true);
                leftImage.sprite = section.leftSprite;
            }
            else
                leftImageGo.SetActive(false);

            if (section.rightSprite != null)
            {
                rightImageGo.SetActive(true);
                rightImage.sprite = section.rightSprite;
            }
            else
                rightImageGo.SetActive(false);
            
            // 显示选项
            if (section.choices.Count > 0)
            {
                List<string> strs = new List<string>();
                foreach (var item in section.choices)
                {
                    strs.Add(item.name);
                }
                cListGo.SetActive(true);
                cList.ShowChoices(strs);
                cList.RegisterChoose(Choose);
            }
            else
                cList.HideSelf();
        }

        public void End()
        {
            UIManager.Instance.HidePanel(panelName);
        }

        public void Choose(int i)
        {
            Debug.Log("剧情选择了:" + i);
            StoryManager.Instance.EnterStory(currentSecion.choices[i], this);
            StoryManager.Instance.MoveNext();
        }
    }
}
