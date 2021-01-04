local LuaBehaviour = require "Core.LuaBehaviour"
local Ball = class("Ball", LuaBehaviour)

function Ball:prefabPath()
    return "Prefabs/Battle/Actors/Ball"
end

function Ball:onAwake()
    print("00000000000")
    
end

return Ball
