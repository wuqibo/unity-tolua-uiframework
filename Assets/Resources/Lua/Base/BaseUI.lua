local LuaBehaviour = require "Base.LuaBehaviour"

local BaseUI = class("BaseUI", LuaBehaviour)

function BaseUI:isFloatUI()
    return false
end

function BaseUI:_createGameObject()
    local prefabPath = self:getPrefabPath()
    UIManager:CreateUI(
        prefabPath,
        function(go)
            self:_onGameObjectCreated(go)
        end,
        self:isFloatUI(),
        self:destroyABAfterSpawn(),
        self:destroyABAfterAllSpawnDestroy()
    )
end

--不在栈顶的UI重新到栈顶显示
function BaseUI:resume()
    UIManager:ResumeUI(self.gameObject)
end

return BaseUI
