local BaseUI = require "Core.BaseUI"
local LobbyMain = class("LobbyMain", BaseUI)

function LobbyMain:prefabPath()
    return "Prefabs/LobbyUI/Lobby/LobbyMain"
end

function LobbyMain:onAwake()
    self.updateHandler = UpdateBeat:CreateListener(self.update, self)
    self.aaa = 150

    local btnLogout = self.transform:Find("BtnLogout")
    btnLogout:OnClick(
        function()
            CommandManager.execute(CommandID.OpenUI, UIID.Login)
        end
    )

    local btnPlayerInfo = self.transform:Find("BtnPlayerInfo")
    btnPlayerInfo:OnClick(
        function()
            CommandManager.execute(CommandID.OpenUI, UIID.PlayerInfo)
        end
    )

    local btnBtnShop = self.transform:Find("BtnShop")
    btnBtnShop:OnClick(
        function()
            CommandManager.execute(CommandID.OpenUI, UIID.Shop)
        end
    )

    local btnFight = self.transform:Find("BtnFight")
    btnFight:OnClick(
        function()
            CommandManager.execute(CommandID.OpenUI, UIID.RoomSelect)
        end
    )

    local btnAlert = self.transform:Find("BtnAlert")
    btnAlert:OnClick(
        function()
            CommandManager.execute(CommandID.OpenUI, UIID.Alert)
        end
    )
end

function LobbyMain:onEnable()
    UpdateBeat:AddListener(self.updateHandler)
end

function LobbyMain:onDisable()
    UpdateBeat:RemoveListener(self.updateHandler)
end

function LobbyMain:update()
    if Input.GetMouseButtonUp(1) then
        CommandManager.execute(CommandID.OpenUI, UIID.Alert)
    end
end

return LobbyMain
