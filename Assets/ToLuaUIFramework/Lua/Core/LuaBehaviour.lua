local LuaBehaviour = class("LuaBehaviour")

function LuaBehaviour:ctor(...)
    self:createGameObject(...)
end

function LuaBehaviour:createGameObject(parent)
    local prefabPath = self:prefabPath()
    ResManager:SpawnPrefab(
        prefabPath,
        parent,
        function(go, isSingletonActiveCallback)
            self:onGameObjectSpawn(go, isSingletonActiveCallback)
        end,
        self:destroyABAfterSpawn(),
        self:destroyABAfterAllSpawnDestroy()
    )
end

function LuaBehaviour:onGameObjectSpawn(go, isSingletonActiveCallback)
    self.gameObject = go
    self.transform = go.transform
    if not isSingletonActiveCallback then
        self:onAwake()
        self:onEnable()
    end
    local csharpLuaBehaviour = go:GetComponent("LuaBehaviour")
    csharpLuaBehaviour:SetLuaClazz(self)
end

--由子类重写来定义
function LuaBehaviour:prefabPath()
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
