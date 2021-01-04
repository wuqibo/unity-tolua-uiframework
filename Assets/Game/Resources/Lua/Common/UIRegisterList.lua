--需要通过发送消息开启的UI
local uilist = {
    [UIID.ResPreload] = require "ResPreload.ResPreload",
    [UIID.Login] = require "LobbyUI.Login.Login",
    [UIID.LobbyMain] = require "LobbyUI.Lobby.LobbyMain",
    [UIID.PlayerInfo] = require "LobbyUI.PlayerInfo.PlayerInfo",
    [UIID.Shop] = require "LobbyUI.Shop.Shop",
    [UIID.RoomSelect] = require "LobbyUI.RoomSelect.RoomSelect",
    [UIID.Alert] = require "Common.Alert",
    [UIID.Battle] = require "Battle.UI.Fight",
}

return uilist
