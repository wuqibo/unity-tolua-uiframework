EventManager = {}

local m_listeners = {}

local function getEventIndex(id, listener)
    if m_listeners[id] == nil then
        return -1
    end
    for i = 1, #m_listeners[id] do
        if m_listeners[id][i].listener == listener then
            return i
        end
    end
    return -1
end

function EventManager.add(self, id, listener)
    local index = 1
    if m_listeners[id] == nil then
        m_listeners[id] = {}
    else
        local existIndex = getEventIndex(id, listener)
        if existIndex ~= -1 then
            return
        end
        index = #m_listeners[id] + 1
    end
    m_listeners[id][index] = {self = self, listener = listener}
end

function EventManager.remove(id, listener)
    if m_listeners[id] == nil then
        return
    end
    local existIndex = getEventIndex(id, listener)
    if existIndex == -1 then
        return
    end
    table.remove(m_listeners[id], existIndex)
end

--事件触发（推荐只能用于用户触发，若是系统逻辑触发，请使用消息管理器 CommandManager）
function EventManager.emit(id, ...)
    if m_listeners[id] == nil then
        return
    end
    for key, value in pairs(m_listeners[id]) do
        value.listener(value.self, ...)
    end
end

return EventManager
