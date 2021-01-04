CommandManager = {}

--两个队列交替使用，以防止执行过程中有消息变动
local msgListeners = {}
local queue = {}
local queue2 = {}

local function getCmdIndex(id, listener)
    if msgListeners[id] == nil then
        return -1
    end
    for i = 1, #msgListeners[id] do
        if msgListeners[id][i].listener == listener then
            return i
        end
    end
    return -1
end

local function mainThreadUpdate()
    while queue[1] do
        queue, queue2 = queue2, queue
        for i = 1, #queue2 do
            local command = queue2[i]
            for key, value in pairs(msgListeners[command.id]) do
                if value.self then
                    if command.param1 and command.param2 and command.param3 and command.param4 then
                        value.listener(value.self, command.param1, command.param2, command.param3, command.param4)
                    elseif command.param1 and command.param2 and command.param3 then
                        value.listener(value.self, command.param1, command.param2, command.param3)
                    elseif command.param1 and command.param2 then
                        value.listener(value.self, command.param1, command.param2)
                    elseif command.param1 then
                        value.listener(value.self, command.param1)
                    else
                        value.listener(value.self)
                    end
                else
                    if command.param1 and command.param2 and command.param3 and command.param4 then
                        value.listener(command.param1, command.param2, command.param3, command.param4)
                    elseif command.param1 and command.param2 and command.param3 then
                        value.listener(command.param1, command.param2, command.param3)
                    elseif command.param1 and command.param2 then
                        value.listener(command.param1, command.param2)
                    elseif command.param1 then
                        value.listener(command.param1)
                    else
                        value.listener()
                    end
                end
            end
            queue2[i] = nil
        end
    end
end

UpdateBeat:AddListener(UpdateBeat:CreateListener(mainThreadUpdate))

function CommandManager.add(id, listener, self)
    local index = 1
    if msgListeners[id] == nil then
        msgListeners[id] = {}
    else
        local existIndex = getCmdIndex(id, listener)
        if existIndex ~= -1 then
            return
        end
        index = #msgListeners[id] + 1
    end
    if self then
        msgListeners[id][index] = {self = self, listener = listener}
    else
        msgListeners[id][index] = {listener = listener}
    end
end

function CommandManager.remove(id, listener)
    if msgListeners[id] == nil then
        return
    end
    local existIndex = getCmdIndex(id, listener)
    if existIndex == -1 then
        return
    end
    table.remove(msgListeners[id], existIndex)
end

--执行命令(一般用于系统逻辑命令传递，若是用户触发事件，请使用事件管理器 EventManager)
function CommandManager.execute(id, param1, param2, param3, param4)
    if msgListeners[id] == nil then
        return
    end
    local command = {id = id}
    if param1 then
        command["param1"] = param1
    end
    if param2 then
        command["param2"] = param2
    end
    if param3 then
        command["param3"] = param3
    end
    if param4 then
        command["param4"] = param4
    end
    table.insert(queue, command)
end

return CommandManager
