using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Function
{
    public static class RandomSelector
    {
        // 超过该随机次数，则会采用其他获取方式，或返回空
        public static int randomThreshold = 10;
        // 异步刷新次数阈值
        public static int randomThresholdAsync = 32;
    
        /// <summary>
        /// 从列表中随机获得一个符合条件的数据。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filter"></param>
        /// <param name="def">没有符合的数据时返回的默认数据</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T RandomData<T>(IList<T> data, IFilter<T> filter = null, T def = default(T))
        {
            // 没有过滤器，就直接返回
            if (filter == null) return data[Random.Range(0, data.Count)];
        
            int index;
            int times = 0;
            while (true) {
                // 随机索引
                index = Random.Range(0, data.Count);
                // 检查可用
                if (filter.Filter(data[index])) 
                    return data[index];
                else {
                    ++times;
                    // 超过阈值
                    if (times > randomThreshold) {
                        Debug.Log($"已随机{randomThreshold}次，未能找到合适的结果");
                        foreach (var item in data) {
                            if (filter.Filter((item))) return item;
                        }
                        Debug.LogWarning("不存在合适的结果，返回默认值");
                        return def;
                    }
                }
            }
        }

        public static IEnumerator RandomDataPAsync<T>(IList<T> positions, Action<T> callback, IFilter<T> filter, 
            T def = default(T), int randomPerFrame = 8)
        {
            int index;
            int times = 0;
            while (true)
            {
                // 随机索引
                index = Random.Range(0, positions.Count);
                // 检查可用
                if (filter.Filter(positions[index])) {
                    callback(positions[index]);
                    yield break;
                }
                else {
                    // 记录随机次数
                    ++times;
                    // 超过阈值
                    if (times > randomThresholdAsync) {
                        Debug.Log($"已随机{randomThresholdAsync}次，未能找到合适的结果");
                    
                        foreach (var item in positions) {
                            // 遍历也遵循每帧阈值
                            if (times % randomPerFrame == 0) yield return null;
                        
                            if (filter.Filter((item))) {
                                callback(positions[index]);
                                yield break;
                            }
                            ++times;
                        }
                    
                        Debug.LogWarning("不存在合适的结果，返回默认值");
                        callback(def);
                        yield break;
                    }
                    // 超过每帧上限
                    if (times % randomPerFrame == 0) {
                        yield return null;
                    }
                }
            }
        }
    }
}
