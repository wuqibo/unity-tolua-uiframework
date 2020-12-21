_Tween = LuaFramework.Tween
_EaseType = LuaFramework.Tween.EaseType

EaseType = {}

EaseType.Linear = _EaseType.Linear
EaseType.ExpoIn = _EaseType.ExpoIn
EaseType.ExpoOut = _EaseType.ExpoOut
EaseType.ExpoInOut = _EaseType.ExpoInOut
EaseType.SineIn = _EaseType.SineIn
EaseType.SineOut = _EaseType.SineOut
EaseType.SineInOut = _EaseType.SineInOut
EaseType.ElasticIn = _EaseType.ElasticIn
EaseType.ElasticOut = _EaseType.ElasticOut
EaseType.ElasticInOut = _EaseType.ElasticInOut
EaseType.BackIn = _EaseType.BackIn
EaseType.BackOut = _EaseType.BackOut
EaseType.BackInOut = _EaseType.BackInOut
EaseType.BounceIn = _EaseType.BounceIn
EaseType.BounceOut = _EaseType.BounceOut
EaseType.BounceInOut = _EaseType.BounceInOut

Tween = {}

--设置初始位置(无动画)-----------------------------------------------------------------------
function Tween.SetPosition(trans, position, worldSpace)
    _Tween.SetPosition(trans, position, worldSpace)
end

--开始位置动画
function Tween.StartMove(delay, trans, toPos, duration, easeType, onFinish, worldSpace)
    _Tween.StartMove(delay, trans, toPos, duration, easeType, nil, onFinish, worldSpace, true, true, true)
end

--终止位置动画
function Tween.StopMove(trans)
    _Tween.StopMove(trans)
end

--开始模拟抛物线动画（height:距离初始点的高度  duration：0-1的分段数）-----------------------------------------------------------------------------
function Tween.StartParabola(delay, trans, toPos, height, delta, elasticity, onCollision, onStop, worldSpace)
    _Tween.StartParabola(delay, trans, toPos, height, delta, worldSpace, elasticity, nil, onCollision, nil, onStop)
end

--终止抛物线动画
function Tween.StopParabola(trans)
    _Tween.StopParabola(trans)
end

--设置初始缩放(无动画)-----------------------------------------------------------------------
function Tween.SetScale(trans, scale)
    _Tween.SetScale(trans, scale)
end

--开始缩放动画
function Tween.StartScale(delay, trans, toScale, duration, easeType, onFinish)
    _Tween.StartScale(delay, trans, toScale, duration, easeType, nil, onFinish)
end

--终止缩放动画
function Tween.StopScale(trans)
    _Tween.StopScale(trans)
end

--设置初始RectTransform.sizeDelta(无动画)-----------------------------------------------------------------------
function Tween.SetUISize(trans, scale)
    _Tween.SetUISize(trans, scale)
end

--开始RectTransform.sizeDelta动画
function Tween.StartUISize(delay, trans, toSize, duration, easeType, onFinish)
    _Tween.StartUISize(delay, trans, toSize, duration, easeType, nil, onFinish)
end

--终止RectTransform.sizeDelta动画
function Tween.StopUISize(trans)
    _Tween.StopUISize(trans)
end

--设置初始透明度(无动画)-----------------------------------------------------------------------
function Tween.SetAlpha(trans, toAlpha, excludeChildren)
    _Tween.SetAlpha(trans, toAlpha, excludeChildren)
end

--开始透明度动画
function Tween.StartAlpha(delay, trans, toAlpha, duration, easeType, onFinish, excludeChildren)
    _Tween.StartAlpha(delay, trans, toAlpha, duration, easeType, nil, onFinish, excludeChildren)
end

--终止透明度动画
function Tween.StopAlpha(trans)
    _Tween.StopAlpha(trans)
end

--开始对单个数值进行Tween运算(onUpdate:返回实时数值 onFinish：不返回值)------------------------------------------------------------------------------
function Tween.Value(delay, startValue, toValue, duration, easeType, onUpdate, onFinish)
    _Tween.Value(delay, startValue, toValue, duration, easeType, onUpdate, onFinish)
end
