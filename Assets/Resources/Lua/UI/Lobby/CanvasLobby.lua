local BaseUI = require "Base.BaseUI"
local Alert = require "UI.Common.Alert"

local CanvasLobby = class("CanvasLobby", BaseUI)

function CanvasLobby:getPrefabPath()
    return "Prefabs/UI/Lobby/CanvasLobby"
end

function CanvasLobby:onAwake()
    self.updateHandler = UpdateBeat:CreateListener(self.update, self)

    local btnLogout = self.transform:Find("Panel/BtnLogout")
    UIEventManager.SetButtonClick(
        btnLogout,
        function(btn, param)
            Destroy(self.gameObject)
            local CanvasLogin = require "UI.Login.CanvasLogin"
            CanvasLogin:new()
        end
    )

    local btnPlayerInfo = self.transform:Find("Panel/BtnPlayerInfo")
    UIEventManager.SetButtonClick(
        btnPlayerInfo,
        function(btn, param)
            local CanvasPlayerInfo = require "UI.PlayerInfo.CanvasPlayerInfo"
            CanvasPlayerInfo:new()
        end
    )

    local btnBtnShop = self.transform:Find("Panel/BtnShop")
    UIEventManager.SetButtonClick(
        btnBtnShop,
        function(btn, param)
            local CanvasShop = require "UI.Shop.CanvasShop"
            CanvasShop:new()
        end
    )

    local btnFight = self.transform:Find("Panel/BtnFight")
    UIEventManager.SetButtonClick(
        btnFight,
        function(btn, param)
            local CanvasRoom = require "UI.Room.CanvasRoom"
            CanvasRoom:new()
        end
    )
end

function CanvasLobby:onEnable()
    UpdateBeat:AddListener(self.updateHandler)
end

function CanvasLobby:onDisable()
    UpdateBeat:RemoveListener(self.updateHandler)
end

function CanvasLobby:update()
    if Input.GetMouseButtonUp(1) then
        Alert:new()
    end
end

return CanvasLobby
