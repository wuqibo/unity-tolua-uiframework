using LuaFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LuaFramework
{
    public class CommandController
    {
        private static CommandController _instance;
        public static CommandController Instance { get { return _instance ?? (_instance = new CommandController()); } }
        private Dictionary<Type, ICommand> managers = new Dictionary<Type, ICommand>();

        public void AddManager(Type managerType)
        {
            if (!managers.ContainsKey(managerType))
            {
                object manager = Main.Instance.gameObject.AddComponent(managerType);
                managers.Add(managerType, manager as ICommand);
            }
        }

        public void ExeCommand(CommandEnum command)
        {
            Debug.Log("执行命令:" + command);
            foreach (var item in managers.Values)
            {
                if (item.ExeCommand(command))
                {
                    break;
                }
            }
        }

    }
}