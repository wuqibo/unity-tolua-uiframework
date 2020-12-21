UIEventManager = {}
UIEventManager.this = UIEventManager

local function _setButtonPress(btn, event, param, triggerMethod)
    local button = btn:GetComponent(typeof(Button))
    if not button then
        button = btn.gameObject:AddComponent(typeof(Button))
    end
    button.triggerMethod = triggerMethod
    if param then
        button.param = param
    end
    button.onClick = event

    --点击反馈效果
    local buttonChange = btn:GetComponent(typeof(ButtonChange))
    if not buttonChange then
        buttonChange = btn.gameObject:AddComponent(typeof(ButtonChange))
    end
end

--按钮放开时触发
function UIEventManager.SetButtonClick(btn, event, param)
    _setButtonPress(btn, event, param, ButtonTriggerMethod.Up)
end

--按钮按下时触发
function UIEventManager.SetButtonPress(btn, event, param)
    _setButtonPress(btn, event, param, ButtonTriggerMethod.Down)
end

--按钮双击时触发
function UIEventManager.SetButtonDoubleClick(btn, event, param)
    _setButtonPress(btn, event, param, ButtonTriggerMethod.Double)
end


return UIEventManager
