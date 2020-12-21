--主入口函数。从这里开始lua逻辑

require "define"
require "global"
require "utils"
require "tween"


function Main()
    local CanvasLogin = require "UI.Login.CanvasLogin"
    CanvasLogin:new()
end

--场景切换通知
function OnLevelWasLoaded(level)
    collectgarbage("collect")
    Time.timeSinceLevelLoad = 0
end

function OnApplicationQuit()
end
