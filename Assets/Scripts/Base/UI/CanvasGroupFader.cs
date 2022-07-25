using System;
using System.Collections;
using Base.Interface;
using UnityEngine;

namespace Base.UI
{
    /// <summary>
    /// 使用 UGUI 的 CanvasGroup 实现淡入淡出。
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupFader : MonoBehaviour, IFloatFader
    {
        protected CanvasGroup m_canvasGroup;

        protected bool m_isFading = false;
        public bool IsFading => m_isFading;

        [Tooltip("渐变时长")]
        public float fadeDuration = 0.5f;

        protected virtual void Awake()
        {
            m_canvasGroup = GetComponent<CanvasGroup>();
            if (m_canvasGroup == null) {
                m_canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }

        /// <summary>
        /// 使用 FadeCoroutine 协程实现的渐变。
        /// </summary>
        /// <param name="target">目标透明度</param>
        /// <param name="callback">结束后的回调函数</param>
        public void Fade(float target, Action<float> callback)
        {
            StartCoroutine(FadeCoroutine(target, callback));
        }

        protected IEnumerator FadeCoroutine(float target, Action<float> callback)
        {
            yield return FadeCoroutine(target);
            callback(target);
        }

        public IEnumerator FadeCoroutine(float target)
        {
            m_isFading = true;
            // 阻挡射线
            m_canvasGroup.blocksRaycasts = true;

            // 计算速度
            float fadeSpeed = Mathf.Abs(m_canvasGroup.alpha - target) / fadeDuration;

            while (!Mathf.Approximately(m_canvasGroup.alpha, target)) {
                m_canvasGroup.alpha = Mathf.MoveTowards(m_canvasGroup.alpha, target, fadeSpeed * Time.deltaTime);
                yield return m_canvasGroup.alpha;
            }

            m_isFading = false;
            m_canvasGroup.blocksRaycasts = false;
        }
    }
}
