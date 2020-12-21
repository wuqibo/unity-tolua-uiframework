local LuaBehaviour = class("LuaBehaviour")

function LuaBehaviour:ctor()
end

function LuaBehaviour:_createGameObject()
    local prefabPath = self:getPrefabPath()
    ResManager:CreatePrefab(
        prefabPath,
        function(go)
            self:_onGameObjectCreated(go)
        end,
        self:destroyABAfterSpawn(),
        self:destroyABAfterAllSpawnDestroy()
    )
end

function LuaBehaviour:_onGameObjectCreated(go)
    self.gameObject = go
    self.transform = go.transform
    self:onAwake()
    self:onEnable()
    local csharpLuaBehaviour = go:GetComponent(typeof(LuaFramework.LuaBehaviour))
    csharpLuaBehaviour:SetEnableAction(
        function()
            self:onEnable()
        end
    )
    csharpLuaBehaviour:SetStartAction(
        function()
            self:onStart()
        end
    )
    csharpLuaBehaviour:SetDisableAction(
        function()
            self:onDisable()
        end
    )
    csharpLuaBehaviour:SetDestroyAction(
        function()
            self:onDestroy()
        end
    )
end

--由子类重写来定义
function LuaBehaviour:getPrefabPath()
    return ""
end

--由子类重写来定义
function LuaBehaviour:destroyABAfterSpawn()
    return false
end

--由子类重写来定义
function LuaBehaviour:destroyABAfterAllSpawnDestroy()
    return false
end

function LuaBehaviour:onAwake()
    --print("-LuaBehaviour----onAwake")
end

function LuaBehaviour:onEnable()
    --print("-LuaBehaviour----onEnable")
end

function LuaBehaviour:onStart()
    --print("-LuaBehaviour----onStart")
end

function LuaBehaviour:onDisable()
    --print("-LuaBehaviour----onDisable")
end

function LuaBehaviour:onDestroy()
    --print("-LuaBehaviour----onDestroy")
end

return LuaBehaviour
