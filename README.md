# Unity-ToLua-UIFramework 

（示例工程适合Unity 2019.1.9f1,其他版本可能出现语法兼容问题而报错，请自行根据版本变换语法）

#### 介绍

- 基于toLua扩展的Unity热跟新实用框架，继承MonoBehaviour常用的生命周期，方便管理组件逻辑。

- 组件内定制AssetBundle回收方案，内存管理你说了算。

- 使用UI栈自动管理层级，消息机制唤起UI，高度解耦，代码简单高效。

- 集成DOTween并扩展transform.DOAlpha方法，方便整个界面多节点一次性透明动画。

- Game目录下常用功能界面已形成，接着往下开发即可，开箱即用，方便快捷。

- 更多功能不断完善中......


#### 快速使用

- 两种模式：

1.  开发模式：Config.cs里将UseAssetBundle设置为false。lua代码和预设体都放入Resources目录里开发，方便即写即跑快速调试，出母包时将Resources文件名改名以排除打包
2.  发布模式：Config.cs里将UseAssetBundle设置为true。菜单ToLuaUIFramework->Build XXX AssetBundle（此时Resources不要改名）

- 开始码代码：

1.  修改Config.cs中的GameResourcesPath定义您的开发目录，必须是Resources目录

2.  Resources内创建Lua目录和Prefabs目录，强烈建议两个文件夹名称不要修改。UI图集、模型、音效等素材可放在Resources目录外。以保证开发目录干净整洁

3.  Prefabs内放好预设体。建议Lua目录内创建结构一样的子目录结构，目录内创建对应的UI控制Lua脚本。如果是UI，继承BaseUI，否则继承LuaBahaviour。方式如下:  
  
FirstUI.lua继承BaseUI:  
```
    local BaseUI = require "Core.BaseUI"  
    local FirstUI = class("FirstUI", BaseUI)  
    return FirstUI  
```
FirstActor.lua继承LuaBehaviour:  
```
    local LuaBehaviour = require "Core.LuaBehaviour"  
    local FirstActor = class("FirstActor", LuaBehaviour)  
    return FirstActor  
```
  
4.  必须重写的方法prefabPath()(指定所绑定的预设体的路径)：  
```
   local BaseUI = require "Core.BaseUI"  
   local FirstUI = class("FirstUI", BaseUI)  
  
   function FirstUI:prefabPath()  
      return "Prefabs/UI/FirstUIPrefab"  
   end  
  
   return FirstUI  
```

5. 选择性重写的方法：指定创建的父级。不重写或重写返回nil或""，则物体创建在跟场景，指定父级名称后，框架用 GameObject.Find("").transform 查找节点当做父级。
   第二种指定父级的方式见第10点。用本方法指定优先级更高。当返回nil或""时，第10点的方法才有效。    
```
function FirstUI:parentName()
    return ""
end
```

6.  实现经典熟悉的生命周期函数，以及按钮绑定方法、DOTween使用  
```
   local BaseUI = require "Core.BaseUI"  
   local FirstUI = class("FirstUI", BaseUI)  
  
   function FirstUI:prefabPath()  
      return "Prefabs/UI/FirstUIPrefab"  
   end  
  
   function FirstUI:onAwake()  
      --按钮的绑定
      self.btnClose = self.transform:Find("BtnClose")  
      self.btnClose:OnClick(function()  
          Destroy(self.gameObect)
      end)  
      self.btnClose:OnPointerDown(function()  
          Log("按下了关闭按钮")
      end) 

      --按钮事件支持传递一个参数，放在首位
      self.btnOpenSecondUI = self.transform:Find("BtnOpenSecondUI")  
      self.btnOpenSecondUI:OnClick(传递的参数, function(传递的参数)  
          CommandManager.execute(CommandID.OpenUI, UIID.您定义的UIID)  
      end) 

   end  

   function FirstUI:onEnable()  
   end  

   function FirstUI:onStart()  
      --DOTween使用（本框架扩展了DOTween的整界面同时透明动画的方法，支持重载以忽略某些参数，详见DOTweenExtend.cs）     
      --self.transform:DOAlpha(初始透明度，目标透明度，动画时长，缓动方式，是否包含所有子节点)  
      self.transform:DOAlpha(0, 1, 1.5, Ease.OutExpo, true):OnComplete(function()
          Log("透明淡入完成")  
      end)  
   end  

   function FirstUI:onDisable()  
   end  

   function FirstUI:onDestroy()  
   end  
  
   return FirstUI  
```

7.  Update方法的实现。出于性能考虑，Update方法需要手动注册和注销： 
```
   local BaseUI = require "Core.BaseUI"  
   local FirstUI = class("FirstUI", BaseUI)  
  
   function FirstUI:onAwake()  
      --定义回调
      self.updateHandler = UpdateBeat:CreateListener(self.update, self)
   end  

   function FirstUI:onEnable()  
       --开始Update
       UpdateBeat:AddListener(self.updateHandler)
   end  

   function FirstUI:onDisable()  
       --停止Update
       UpdateBeat:RemoveListener(self.updateHandler)
   end  

   function FirstUI:update()  
       --这里每帧执行一次
   end  
  
   return FirstUI  
```

8.  注册UI,以实现通过发送命令展示UI，高度解耦：  
    按照Demo定义UIID,然后添加到UIRegisterList.lua内的列表里即可  

9.  通过命令启动第一个UI  
    打开ToLuaUIFramework/Lua/Main.lua脚本，替换第19行开启UI命令里的UIID成你的UIID即可  
```
    CommandManager.execute(CommandID.OpenUI, UIID.您定义的UIID)  
```

10.  不通过发送消息开启UI的方法（即常规创建预设体的方法）如下：（如果按第5点用函数指定了有效的父级名称，则在此new()传入的父级无效。）
```
    local classPath = require "LobbyUI.Lobby.LobbyMain"
    local lobbyUI = classPath:new(parent) 
    --parent没有可不传
    local lobbyUI = classPath:new()
```
    
#### 关于UI栈  

1. 特性
- 继承BaseUI后，UI自动受UI栈管理。即每生成一个UI界面都会自动压入UI栈，Destroy后自动出栈，默认情况下除栈顶之外的其他UI自动隐藏，只需发送命令打开新UI即可，前一个UI无需多加代码关闭。栈顶的UI被Destroy出栈后，新的栈顶UI会自动显示，无需多写代码激活。如果不希望关闭UI后自动显示前一个UI,可在打开新UI命令前，先将当前UI进行Destroy即可
- 继承BaseUI后，通过发送命令打开新UI时，UI将被当成一个单例使用。系统先从栈内查找，如果找到则直接移到栈顶显示，不会重复创建新UI

2.  特殊情况：
- 情况1：某些UI需要常驻被覆盖也不隐藏的，只需重写以下方法并返回true即可
```
    function FirstUI:keepActive()  
       return true
    end  
```
- 情况2：当新打开的UI属于悬浮弹窗，则前一个UI要保持显示不能隐藏的，只需在悬浮窗UI里重写以下方法并返回true即可
```
    function FirstUI:isFloat()  
       return true
    end  
```
3. 刷新UI栈：当创建UI后又动态在onAwake里指定Canvas的Camera，或者因为动态添加特效需要调整层级的，必须用以下方法刷新一次UI栈，以便框架重新排列sortingOrder的关系。  
```
    UIManager:RefreshStack() 
```

#### AssetBundle的操作

1. 导出AssetBundle

- 第1步：导出前配置好Config.cs里的4个目录：OutputPath，RemoteUrl，ExportLuaPaths，ExportResPath  

- 第2步：菜单执行ToLuaUIFramework->Build XXX AssetBundle  

- 第3步：Config.cs里将UseAssetBundle设置为true, 将开发Resources目录改名以免资源被打入母包中，开始出包

2. 预加载（此步骤可做可不做）

如需预加载本地AssetBundle资源，可仿照Game目录下的的ResPreload.lua执行预加载，预加载的目录可自行配进paths数组里  
```
    local paths = {
        "Prefabs/LobbyUI"
    }
    ResManager:PreloadLocalAssetBundles(
        paths,
        function(progress)
            self.slider.value = progress
            if progress == 1 then
                --预加载完成
            end
        end
    )
```

3. 如果Lua类重写该方法，在创建预设体之后将会立即清除内存里的AssetBundle资源  
```
    function LuaBehaviour:destroyABAfterSpawn()
        return true
    end
```

4. 如果Lua类重写该方法，在所有被创建出来的预设体被删除之后将会立即清除内存里的AssetBundle资源  
```
    function LuaBehaviour:destroyABAfterAllSpawnDestroy()
        return true
    end
```

5. 销毁所有已加载的AssetBundle，当传入参数为true时，将连同预设体克隆出来的的资源一起清除，如果对象还在场景中，体现为资源丢失的紫色效果  
```
    ResManager:UnloadAllAssetBundles(false)
```
