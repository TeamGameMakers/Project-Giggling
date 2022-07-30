using System;
using System.IO;
using Base.Persistence;
using Base.Scene;
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
        
        #endregion

        #region 游戏存档

        public static string saveName = "giggling";
        
        private static SaveData m_data;

        private static SaveData Data => m_data ??= new SaveData();

        // 注册数据
        /// <summary>
        /// 注册基础类型或简单类。
        /// 会将类型转换为 string 存入字典。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj">除非是 ScriptableObject, 否则要添加 Serializable 特性</param>
        public static void Register(string key, object obj)
        {
            Type type = obj.GetType();
            if (type.IsValueType)
            {
                Data.Add(key, obj.ToString());
            }
            else
            {
                Data.Add(key, JsonUtility.ToJson(obj));
            }
        }

        // 存储
        public static void Save()
        {
            // --构建 SaveData--
            m_data = new SaveData();
            m_data.name = saveName;
            // 场景名
            m_data.Add("SceneName", SceneLoader.CurrentScene);
            
            BeforeSaveActions?.Invoke();

            // --持久化--
            Write(m_data);
        }
        
        #endregion
    }
}