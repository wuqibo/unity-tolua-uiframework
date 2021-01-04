--Unity对象
GameObject = UnityEngine.GameObject
Input = UnityEngine.Input
Slider = UnityEngine.UI.Slider

--C#对象
EventManager = ToLuaUIFramework.EventManager
LuaManager   = ToLuaUIFramework.LuaManager.instance
ResManager   = ToLuaUIFramework.ResManager.instance
UIManager    = ToLuaUIFramework.UIManager.instance
SoundManager = ToLuaUIFramework.SoundManager.instance

--第三方插件
JSON = require "cjson"

--Lua工具
BButton             = ToLuaUIFramework.BButton
BButtonTrigger        = ToLuaUIFramework.BButtonTrigger
CommandManager      = require "CommandManager"
EventManager        = require "EventManager"