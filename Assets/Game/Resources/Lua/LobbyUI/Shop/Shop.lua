local BaseUI = require "Core.BaseUI"
local Shop = class("Shop", BaseUI)

function Shop:prefabPath()
    return "Prefabs/LobbyUI/Shop/Shop"
end

function Shop:onAwake()
    local btnBack = self.transform:Find("Panel/BtnBack")
    btnBack:OnClick(
        function()
            Destroy(self.gameObject)
        end
    )
end

return Shop
