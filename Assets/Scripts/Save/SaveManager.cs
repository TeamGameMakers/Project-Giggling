using System;
using System.IO;
using Base.Event;
using Base.Persistence;
using Base.Scene;
using GM;
using UnityEngine;

namespace Save
{
    public static class SaveManager
    {
        #region 事件
        // 存档前事件
        public static event Action BeforeSaveActions;
        // 读档后事件
        public static event Action AfterLoadActions;
        // 删档前事件
        public static event Action BeforeDeleteActions;
        // 删档后事件
        public static event Action AfterDeleteActions;
        #endregion
        
        #region 持久化

        public static readonly IDataPersistence Persistence = BinaryPersistence.Instance;

        public static string SaveDirectory => Application.persistentDataPath + "/Saves";

        public static string GetFullFilePath(string file)
        {
            return SaveDirectory + "/" + file;
        }
        
        private static void InitDirectory()
        {
            if (!Directory.Exists(SaveDirectory)) {
                Directory.CreateDirectory(SaveDirectory);
            }
        }
        
        public static bool ExistsSave(string name)
        {
            return File.Exists(GetFullFilePath(name));
        }
        
        private static void Write(SaveData data)
        {
            InitDirectory();
            Persistence.Write(data, GetFullFilePath(data.name));
        }

        private static SaveData Read(string name)
        {
            if (ExistsSave(name))
            {
                return Persistence.Read<SaveData>(GetFullFilePath(name));
            }

            return null;
        }

        public static void Delete(string name)
        {
            if (ExistsSave(name))
            {
                BeforeDeleteActions?.Invoke();
                File.Delete(GetFullFilePath(name));
                AfterDeleteActions?.Invoke();
            }
        }

        #endregion

        #region 临时数据

        // 从哪个场景切换过来
        private static string m_fromScene = "";
        
        /// <summary>
        /// 获得进入该场景的场景名。
        /// 如果是读档，则返回 string.Empty.
        /// </summary>
        /// <returns></returns>
        public static string GetFromScene()
        {
            string res = m_fromScene;
            if (!string.IsNullOrEmpty(m_fromScene))
                m_fromScene = string.Empty;

            return res;
        }

        public static void RegisterFromScene(string sceneName)
        {
            m_fromScene = sceneName;
        }

        #endregion
        
        #region 获取数据

        /// <summary>
        /// 覆盖当前读取的存档内容。
        /// </summary>
        public static void Load()
        {
            m_data = Read(saveName);
            AfterLoadActions?.Invoke();
        }

        /// <summary>
        /// 获取值类型，需要自行使用 Parse 转换。
        /// </summary>
        /// <param name="key"></param>
        /// <returns>没有则返回空字符串</returns>
        public static string GetValue(string key)
        {
            return Data.GetItem(key);
        }

        public static bool GetBool(string key)
        {
            return Data.GetBool(key);
        }
        
        /// <summary>
        /// 取出指定的对象。
        /// 不会处理值对象。
        /// 会特别处理 ScriptableObject.
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValue<T>(string key, T def = default(T))
        {
            Type type = typeof(T);
            if (type.IsValueType)
            {
                return def;
            }
            else if (typeof(ScriptableObject).IsAssignableFrom(type))
            {
                T obj = Activator.CreateInstance<T>();
                JsonUtility.FromJsonOverwrite(Data.GetItem(key), obj);
                return obj;
            }
            else
            {
                return JsonUtility.FromJson<T>(Data.GetItem(key));
            }

            return def;
        }

        public static int GetGameClear()
        {
            return PlayerPrefs.GetInt(m_gameClear, 0);
        }

        #endregion

        #region 游戏存档

        public static string saveName = "giggling";
        
        private static SaveData m_data;

        private static SaveData Data => m_data ??= new SaveData();

        private static string m_gameClear = "giggling_game_clear";

        // 注册数据
        /// <summary>
        /// 注册基础类型或简单类。
        /// 会将类型转换为 string 存入字典。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj">除非是 ScriptableObject, 否则要添加 Serializable 特性</param>
        public static void Register(string key, object obj)
        {
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogWarning("存档不可注册空键");
                return;
            }
            
            Type type = obj.GetType();
            if (type == typeof(string))
            {
                Data.Add(key, obj as string);
            }
            else if (type.IsValueType)
            {
                Data.Add(key, obj.ToString());
            }
            else
            {
                Data.Add(key, JsonUtility.ToJson(obj));
            }
        }

        public static void RegisterBool(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogWarning("存档不可注册空键");
                return;
            }
            
            Data.AddBool(key);
        }

        // 存储
        public static void Save()
        {
            Debug.Log("存档");
            
            // --构建 SaveData--
            Data.name = saveName;
            // 场景名
            Data.Add("SceneName", SceneLoader.CurrentScene);
            
            // 记录玩家位置，要获取玩家位置
            string playerPos = JsonUtility.ToJson(GameManager.Player.position);
            Data.Add("PlayerPosition", playerPos);
            
            // 在获取时就会记录
            // 记录玩家手电筒
            // bool hasFlashLight = EventCenter.Instance.FuncTrigger<bool>("GetPlayerFlashLight");
            // if (hasFlashLight)
            // {
            //     RegisterBool("hasFlashLight");
            // }
            
            // 存储玩家数据
            EventCenter.Instance.EventTrigger("SaveStatusData");
            
            BeforeSaveActions?.Invoke();

            // --持久化--
            Write(m_data);
        }

        // 储存游戏通关
        public static void GameClearRecord()
        {
            PlayerPrefs.SetInt(m_gameClear, 1);
            PlayerPrefs.Save();
        }
        
        #endregion
    }
}
