local BaseUI = require "Core.BaseUI"

local Alert = class("Alert", BaseUI)

function Alert:prefabPath()
    return "Prefabs/Common/Alert"
end

function Alert:isFloat()
    return true
end

function Alert:onAwake()
    self.super:onAwake()
    self.updateHandler = UpdateBeat:CreateListener(self.update, self)
    self.dialog = self.transform:Find("Dialog")
end

function Alert:onEnable()
    self.super:onEnable()
    UpdateBeat:AddListener(self.updateHandler)

    --黑色蒙版动画
    self.transform:DOAlpha(0, 0.5, 0.3, Ease.OutSine, false)

    --小对话框动画
    self.dialog.localScale = Vector3.one * 0.5
    self.dialog:DOScale(Vector3.one, 0.3):SetEase(Ease.OutBack)
end

function Alert:onDisable()
    self.super:onDisable()
    UpdateBeat:RemoveListener(self.updateHandler)
end

function Alert:update()
    if Input.GetMouseButtonUp(0) then
        Destroy(self.gameObject)
    end
end

return Alert
