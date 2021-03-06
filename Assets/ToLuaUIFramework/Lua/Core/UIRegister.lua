UIRegister = {}

local uiRequires = {}
local uis = {}
local hadRegister = false

function UIRegister.init()
    if not hadRegister then
        local uilist = require "Common.UIRegisterList"
        for key, value in pairs(uilist) do
            uiRequires[key] = value
        end
        hadRegister = true
    end
end

function UIRegister.register()
    CommandManager.add(
        CommandID.OpenUI,
        function(uiID)
            local _uiID = tonumber(uiID)
            local ui = uis[_uiID]
            if not ui then
                ui = uiRequires[_uiID]:new()
                if ui.transform then
                    ui.transform:GetComponent("LuaBehaviour"):SetUIID(_uiID)
                else
                    ui.uiID = _uiID
                end
                uis[_uiID] = ui
            else
                ui:createGameObject()
            end
        end
    )
end

function onUIDestroy(_uiID)
    uis[_uiID] = nil
end

return UIRegister
