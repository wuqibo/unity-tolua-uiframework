local BaseUI = require "Base.BaseUI"

local CanvasShop = class("CanvasShop", BaseUI)

function CanvasShop:getPrefabPath()
    return "Prefabs/UI/Shop/CanvasShop"
end

function CanvasShop:onAwake()
    local btnBack = self.transform:Find("Panel/BtnBack")
    UIEventManager.SetButtonClick(
        btnBack,
        function(btn, param)
            Destroy(self.gameObject)
        end
    )
end

return CanvasShop
