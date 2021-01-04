function class(classname, super)
    local superType = type(super)
    local cls

    if superType ~= "function" and superType ~= "table" then
        superType = nil
        super = nil
    end

    if super then
        cls = {}
        setmetatable(cls, {__index = super})
        cls.super = super
    else
        cls = {}
    end

    cls.__cname = classname
    cls.__index = cls

    function cls:new(...)
        local instance = setmetatable({}, cls)
        instance.class = cls
        instance:ctor(...)
        return instance
    end

    return cls
end

function clear_class(class)
    for i, v in pairs(class) do
        if type(v) ~= "function" then
            class[i] = nil
        end
    end
end

function Destroy(obj)
    if obj then
        GameObject.Destroy(obj)
    end
end