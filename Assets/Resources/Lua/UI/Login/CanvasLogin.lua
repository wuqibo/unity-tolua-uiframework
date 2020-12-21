local BaseUI = require "Base.BaseUI"
local CanvasLogin = class("CanvasLogin", BaseUI)

function CanvasLogin:getPrefabPath()
    return "Prefabs/UI/Login/CanvasLogin"
end

function CanvasLogin:onAwake()
    Destroy(GameObject.Find("CanvasLoadAB"))

    local btnStart = self.transform:Find("Panel/BtnStart")
    UIEventManager.SetButtonClick(
        btnStart,
        function(btn, param)
            Destroy(self.gameObject)
            local CanvasLobby = require "UI.Lobby.CanvasLobby"
            CanvasLobby:new()
        end
    )
end

return CanvasLogin
