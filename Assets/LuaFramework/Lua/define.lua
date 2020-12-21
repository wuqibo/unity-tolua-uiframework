--Unity对象
GameObject = UnityEngine.GameObject
Input = UnityEngine.Input

--C#对象
EventManager = LuaFramework.EventManager
LuaManager   = LuaFramework.LuaManager.instance
ResManager   = LuaFramework.ResManager.instance
SoundManager = LuaFramework.SoundManager.instance
UIManager    = LuaFramework.UIManager.instance

--第三方插件
JSON = require "cjson"

--Lua工具
Button              = LuaFramework.Button
ButtonTriggerMethod = Button.TriggerMethod
ButtonChange        = LuaFramework.ButtonChange
UIEventManager      = require "UIEventManager"
