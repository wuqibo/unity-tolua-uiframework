local LuaBehaviour = require "Core.LuaBehaviour"

local BaseUI = class("BaseUI", LuaBehaviour)

--由子类重写，在UI栈内被别的UI覆盖时是否隐藏自己
function BaseUI:keepActive()
    return false
end

--由子类重写，如果定义了浮动UI,则在UI栈内的下层UI将始终显示
function BaseUI:isFloat()
    return false
end

function BaseUI:createGameObject(parent)
    local prefabPath = self:prefabPath()
    local parentName = self:parentName()
    if parentName and parentName ~= "" then
        local parentGo = GameObject.Find(parentName)
        if parentGo then
            parent = parentGo.transform
        end
    end
    UIManager:SpawnUI(
        prefabPath,
        parent,
        function(go, isSingletonActiveCallback)
            self:onGameObjectSpawn(go, isSingletonActiveCallback)
        end,
        self:keepActive(),
        self:isFloat(),
        self:destroyABAfterSpawn(),
        self:destroyABAfterAllSpawnDestroy()
    )
end

function BaseUI:parentName()
    return "MainCanvas"
end

return BaseUI
