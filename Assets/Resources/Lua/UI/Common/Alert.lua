local BaseUI = require "Base.BaseUI"

local Alert = class("Alert", BaseUI)

function Alert:getPrefabPath()
    return "Prefabs/UI/Common/Alert"
end

function Alert:isFloatUI()
    return true
end

function Alert:onAwake()
    self.updateHandler = UpdateBeat:CreateListener(self.update, self)
    self.dialog = self.transform:Find("Dialog")
end

function Alert:onEnable()
    UpdateBeat:AddListener(self.updateHandler)

    --黑色蒙版动画
    Tween.SetAlpha(self.transform, 0)
    Tween.StartAlpha(0, self.transform, 0.5, 0.3, EaseType.SineOut);

    --小对话框动画
    Tween.SetScale(self.dialog, Vector3.one * 0.5);
    Tween.StartScale(0, self.dialog, Vector3.one, 0.3, EaseType.BackOut);
end

function Alert:onDisable()
    UpdateBeat:RemoveListener(self.updateHandler)
end

function Alert:update()
    if Input.GetMouseButtonUp(0) then
        Destroy(self.gameObject)
    end
end

return Alert
