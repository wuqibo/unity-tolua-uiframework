using System;
using System.Collections.Generic;
using UnityEngine;

namespace ToLuaUIFramework
{
    public class BaseMsg : object
    {
        public object[] args;

        public BaseMsg(params object[] args)
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
    public class MessageCenter
    {
        private static Dictionary<MsgEnum, List<Action<BaseMsg>>> eventsBuffer = new Dictionary<MsgEnum, List<Action<BaseMsg>>>();

        /// <summary>
        /// 注册事件监听
        /// </summary>
        public static void Add(MsgEnum msgEnum, Action<BaseMsg> callback)
        {
            if (!eventsBuffer.ContainsKey(msgEnum))
            {
                eventsBuffer.Add(msgEnum, new List<Action<BaseMsg>>());
            }
            List<Action<BaseMsg>> callbacks = eventsBuffer[msgEnum];
            if (!callbacks.Contains(callback))
            {
                callbacks.Add(callback);
            }
        }


        /// <summary>
        /// 注销事件监听
        /// </summary>
        public static void Remove(MsgEnum msgEnum, Action<BaseMsg> callback)
        {
            if (eventsBuffer.ContainsKey(msgEnum))
            {
                List<Action<BaseMsg>> callbacks = eventsBuffer[msgEnum];
                if (callbacks.Contains(callback))
                {
                    callbacks.Remove(callback);
                }
            }
        }

        /// <summary>
        /// 清空所有事件
        /// </summary>
        public static void Clear(MsgEnum msgEnum)
        {
            if (eventsBuffer.ContainsKey(msgEnum))
            {
                eventsBuffer.Remove(msgEnum);
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
        public static void Dispatch(MsgEnum msgEnum, params object[] args)
        {
            Debug.Log("触发事件: " + msgEnum);
            if (eventsBuffer.ContainsKey(msgEnum))
            {
                List<Action<BaseMsg>> callbacks = eventsBuffer[msgEnum];
                for (int i = 0; i < callbacks.Count; i++)
                {
                    if (callbacks[i] != null)
                    {
                        callbacks[i].Invoke(new BaseMsg(args));
                    }
                }
            }
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        public static void Dispatch(MsgEnum msgEnum, BaseMsg msg)
        {
            Debug.Log("触发事件: " + msgEnum);
            if (eventsBuffer.ContainsKey(msgEnum))
            {
                List<Action<BaseMsg>> callbacks = eventsBuffer[msgEnum];
                for (int i = 0; i < callbacks.Count; i++)
                {
                    if (callbacks[i] != null)
                    {
                        callbacks[i].Invoke(msg);
                    }
                }
            }
        }

    }
}