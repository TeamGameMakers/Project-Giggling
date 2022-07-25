using System;
using System.Collections;
using Base.Mono;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base.Scene
{
    /// <summary>
    /// 场景切换器。
    /// </summary>
    public static class SceneLoader
    {
        #region 成员字段
        // 获得当前激活的场景名
        public static string CurrentScene => SceneManager.GetActiveScene().name;

        // 加载事件，会传入加载进度
        public static event Action<float> LoadingActions;
        // 卸载事件，会传入卸载进度
        public static event Action<float> UnloadingActions;
        // 场景加载前行为
        public static event Action BeforeLoadedActions;
        // 场景加载后行为
        public static event Action AfterLoadedActions;
        #endregion

        #region 直接加载
        public static void LoadScene(string name)
        {
            BeforeLoadedActions?.Invoke();
            SceneManager.LoadScene(name);
            AfterLoadedActions?.Invoke();
        }

        public static void LoadSceneAsync(string name)
        {
            // 开启加载协程
            MonoManager.Instance.StartCoroutine(LoadSceneAsyncCoroutine(name));
        }

        private static IEnumerator LoadSceneAsyncCoroutine(string name)
        {
            BeforeLoadedActions?.Invoke();

            // 异步加载
            AsyncOperation ao = SceneManager.LoadSceneAsync(name);
            while (!ao.isDone) {
                // 向外分发进度
                LoadingActions?.Invoke(ao.progress);
                yield return ao;
            }

            AfterLoadedActions?.Invoke();
        }
        #endregion

        #region 切换场景
        /// <summary>
        /// 异步切换场景。
        /// 可以存在多个场景，只会替换当前激活的场景。
        /// 由于卸载场景的同步方法已被弃用，因此不提供同步切换场景。
        /// </summary>
        /// <param name="from">当前激活的场景，为空字符串则不会卸载</param>
        /// <param name="to">要切换至的场景</param>
        public static void SwitchSceneAsync(string from, string to)
        {
            // 开启加载协程
            MonoManager.Instance.StartCoroutine(SwitchSceneAsyncCoroutine(from, to));
        }

        private static IEnumerator SwitchSceneAsyncCoroutine(string from, string to)
        {
            BeforeLoadedActions?.Invoke();

            // 加载目标场景
            AsyncOperation ao = SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
            while (!ao.isDone) {
                // 向外分发进度
                LoadingActions?.Invoke(ao.progress);
                yield return ao;
            }

            // 激活目标场景
            UnityEngine.SceneManagement.Scene newScene = SceneManager.GetSceneByName(to);
            SceneManager.SetActiveScene(newScene);

            // 卸载原场景
            if (!string.IsNullOrEmpty(from)) {
                yield return UnloadSceneAsync(from);
            }

            AfterLoadedActions?.Invoke();
        }
        #endregion

        private static IEnumerator UnloadSceneAsync(string name)
        {
            UnityEngine.SceneManagement.Scene s = SceneManager.GetSceneByName(name);
            if (s.IsValid()) {
                AsyncOperation ao = SceneManager.UnloadSceneAsync(name);
                // 向外分发进度
                UnloadingActions?.Invoke(ao.progress);
                yield return ao;
            }
        }
    }
}
