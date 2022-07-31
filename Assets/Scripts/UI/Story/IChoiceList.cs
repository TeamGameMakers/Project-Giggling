using System;
using System.Collections.Generic;

namespace UI
{
    /// <summary>
    /// 显示选项与接受选择。
    /// </summary>
    public interface IChoiceList
    {
        void ShowChoices(List<string> choices);

        void HideSelf();

        void RegisterChoose(Action<int> chooseActions);
    }
}