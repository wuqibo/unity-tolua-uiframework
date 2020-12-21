using System;
using System.Collections.Generic;
using UnityEngine;

namespace LuaFramework
{
    public class BaseEventData : object
    {
        public object[] args;

        public BaseEventData(params object[] args)
        {
            this.args = args;
        }

        public override string ToString()
        {
            string str = GetType().FullName + ": [";
            for (int i = 0; i < args.Length; i++)
            {
                str += args[i] + ",";
            }
            str = str.Substring(0, str.Length - 1);
            str += "]";
            return str;
        }
    }
    public class EventManager
    {
        private static Dictionary<EventEnum, List<Action<BaseEventData>>> eventsBuffer = new Dictionary<EventEnum, List<Action<BaseEventData>>>();

        /// <summary>
        /// 注册事件监听
        /// </summary>
        public static void Add(EventEnum eventEnum, Action<BaseEventData> callback)
        {
            if (!eventsBuffer.ContainsKey(eventEnum))
            {
                eventsBuffer.Add(eventEnum, new List<Action<BaseEventData>>());
            }
            List<Action<BaseEventData>> callbacks = eventsBuffer[eventEnum];
            if (!callbacks.Contains(callback))
            {
                callbacks.Add(callback);
            }
        }


        /// <summary>
        /// 注销事件监听
        /// </summary>
        public static void Remove(EventEnum eventEnum, Action<BaseEventData> callback)
        {
            if (eventsBuffer.ContainsKey(eventEnum))
            {
                List<Action<BaseEventData>> callbacks = eventsBuffer[eventEnum];
                if (callbacks.Contains(callback))
                {
                    callbacks.Remove(callback);
                }
            }
        }

        /// <summary>
        /// 清空所有事件
        /// </summary>
        public static void Clear(EventEnum eventEnum)
        {
            if (eventsBuffer.ContainsKey(eventEnum))
            {
                eventsBuffer.Remove(eventEnum);
            }
        }

        /// <summary>
        /// 清空所有事件
        /// </summary>
        public static void ClearAll()
        {
            Debug.Log(">>>>>>>>>>清空所有事件");
            eventsBuffer.Clear();
        }


        /// <summary>
        /// 触发事件
        /// </summary>
        public static void Emit(EventEnum eventEnum, params object[] args)
        {
            Debug.Log("触发事件: " + eventEnum);
            if (eventsBuffer.ContainsKey(eventEnum))
            {
                List<Action<BaseEventData>> callbacks = eventsBuffer[eventEnum];
                for (int i = 0; i < callbacks.Count; i++)
                {
                    if (callbacks[i] != null)
                    {
                        callbacks[i].Invoke(new BaseEventData(args));
                    }
                }
            }
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        public static void Emit(EventEnum eventEnum, BaseEventData eventData)
        {
            Debug.Log("触发事件: " + eventEnum);
            if (eventsBuffer.ContainsKey(eventEnum))
            {
                List<Action<BaseEventData>> callbacks = eventsBuffer[eventEnum];
                for (int i = 0; i < callbacks.Count; i++)
                {
                    if (callbacks[i] != null)
                    {
                        callbacks[i].Invoke(eventData);
                    }
                }
            }
        }

    }
}