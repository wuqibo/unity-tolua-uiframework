local BaseUI = require "Base.BaseUI"
local CanvasFight = class("CanvasFight", BaseUI)

function CanvasFight:getPrefabPath()
    return "Prefabs/UI/Fight/CanvasFight"
end

function CanvasFight:onAwake()
    local btnBack = self.transform:Find("Panel/BtnBack")
    UIEventManager.SetButtonClick(
        btnBack,
        function(btn, param)
            Destroy(self.gameObject)
            local CanvasLobby = require "UI.Lobby.CanvasLobby"
            CanvasLobby:new()
        end
    )
end

return CanvasFight
