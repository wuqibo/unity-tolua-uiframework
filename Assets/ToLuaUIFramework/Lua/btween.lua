BEaseType = {}

BEaseType.Linear = ToLuaUIFramework.BTween.BEaseType.Linear
BEaseType.ExpoIn = ToLuaUIFramework.BTween.BEaseType.ExpoIn
BEaseType.ExpoOut = ToLuaUIFramework.BTween.BEaseType.ExpoOut
BEaseType.ExpoInOut = ToLuaUIFramework.BTween.BEaseType.ExpoInOut
BEaseType.SineIn = ToLuaUIFramework.BTween.BEaseType.SineIn
BEaseType.SineOut = ToLuaUIFramework.BTween.BEaseType.SineOut
BEaseType.SineInOut = ToLuaUIFramework.BTween.BEaseType.SineInOut
BEaseType.ElasticIn = ToLuaUIFramework.BTween.BEaseType.ElasticIn
BEaseType.ElasticOut = ToLuaUIFramework.BTween.BEaseType.ElasticOut
BEaseType.ElasticInOut = ToLuaUIFramework.BTween.BEaseType.ElasticInOut
BEaseType.BackIn = ToLuaUIFramework.BTween.BEaseType.BackIn
BEaseType.BackOut = ToLuaUIFramework.BTween.BEaseType.BackOut
BEaseType.BackInOut = ToLuaUIFramework.BTween.BEaseType.BackInOut
BEaseType.BounceIn = ToLuaUIFramework.BTween.BEaseType.BounceIn
BEaseType.BounceOut = ToLuaUIFramework.BTween.BEaseType.BounceOut
BEaseType.BounceInOut = ToLuaUIFramework.BTween.BEaseType.BounceInOut

BTween = {}

--开始模拟抛物线动画（height:距离初始点的高度  duration：0-1的分段数）-----------------------------------------------------------------------------
function BTween.StartParabola(delay, trans, toPos, height, delta, elasticity, onCollision, onStop, worldSpace)
    ToLuaUIFramework.BTween.StartParabola(delay, trans, toPos, height, delta, worldSpace, elasticity, nil, onCollision, nil, onStop)
end

--终止抛物线动画
function BTween.StopParabola(trans)
    ToLuaUIFramework.BTween.StopParabola(trans)
end

--开始对单个数值进行Tween运算(onUpdate:返回实时数值 onFinish：不返回值)------------------------------------------------------------------------------
function BTween.Value(delay, startValue, toValue, duration, easeType, onUpdate, onFinish)
    ToLuaUIFramework.BTween.Value(delay, startValue, toValue, duration, easeType, onUpdate, onFinish)
end
