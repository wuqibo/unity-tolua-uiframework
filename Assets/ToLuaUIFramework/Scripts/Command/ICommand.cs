using System;
using UnityEngine;

namespace ToLuaUIFramework
{
    public interface ICommand
    {
        /// <summary>
        /// 返回true则立即终止命令,否则下一个模块的接口将继续被触发
        /// </summary>
        bool ExeCommand(CommandEnum command);
    }
}