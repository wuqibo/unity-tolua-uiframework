local BaseUI = require "Base.BaseUI"

local CanvasPlayerInfo = class("CanvasPlayerInfo", BaseUI)

function CanvasPlayerInfo:getPrefabPath()
    return "Prefabs/UI/PlayerInfo/CanvasPlayerInfo"
end

function CanvasPlayerInfo:isFloatUI()
    return true
end

function CanvasPlayerInfo:onAwake()
    local btnBack = self.transform:Find("Panel/BtnBack")
    UIEventManager.SetButtonClick(
        btnBack,
        function(btn, param)
            Destroy(self.gameObject)
        end
    )
end

return CanvasPlayerInfo
