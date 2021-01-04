using System;
using System.Collections.Generic;
using UnityEngine;

namespace ToLuaUIFramework
{
    public class CommandController : MonoBehaviour
    {
        private static CommandController _instance;
        public static CommandController Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = Main.Instance.GetComponent<CommandController>();
                    if (!_instance)
                    {
                        _instance = Main.Instance.gameObject.AddComponent<CommandController>();
                    }
                }
                return _instance;
            }
        }
        private Dictionary<Type, ICommand> managers = new Dictionary<Type, ICommand>();
        Queue<CommandEnum> commandsQueue = new Queue<CommandEnum>();

        private void Update()
        {
            while (commandsQueue.Count > 0)
            {
                CommandEnum command = commandsQueue.Dequeue();
                foreach (var item in managers.Values)
                {
                    if (item.ExeCommand(command))
                    {
                        break;
                    }
                }
            }
        }

        public void AddManager(Type managerType)
        {
            if (!managers.ContainsKey(managerType))
            {
                object manager = Main.Instance.gameObject.AddComponent(managerType);
                managers.Add(managerType, manager as ICommand);
            }
        }

        public void Execute(CommandEnum command)
        {
            Debug.Log("执行命令:" + command);
            commandsQueue.Enqueue(command);
        }

    }
}