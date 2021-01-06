local BaseUI = require "Core.BaseUI"
local Fight = class("Fight", BaseUI)

function Fight:prefabPath()
    return "Prefabs/Battle/UI/Fight"
end

function Fight:parentName()
    return nil
end

function Fight:onAwake()
    local btnBack = self.transform:Find("Panel/BtnBack")
    btnBack:OnClick(
        function()
            Destroy(self.gameObject)
        end
    )
end

return Fight
