local BaseUI = require "Core.BaseUI"
local ResPreload = class("ResPreload", BaseUI)

function ResPreload:prefabPath()
    return "Prefabs/Common/ResPreload"
end

function ResPreload:onAwake()
    self.slider = self.transform:Find("Panel/Slider"):GetComponent("Slider")
    self.slider.value = 10
end

function ResPreload:onCallback(param)
    print("================================= onCallback: " .. param)
end

function ResPreload:onStart()
    local paths = {
        "Prefabs/LobbyUI"
    }
    ResManager:PreloadLocalAssetBundles(
        paths,
        function(progress)
            self.slider.value = progress
            if progress == 1 then
                Destroy(self.gameObject)
                CommandManager.execute(CommandID.OpenUI, UIID.Login)
            end
        end
    )
end

return ResPreload
