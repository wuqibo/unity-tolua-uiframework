﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class ToLuaUIFramework_LuaManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(ToLuaUIFramework.LuaManager), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("ExeCommand", ExeCommand);
		L.RegFunction("DoFile", DoFile);
		L.RegFunction("GetFunction", GetFunction);
		L.RegFunction("LuaGC", LuaGC);
		L.RegFunction("Close", Close);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("instance", get_instance, set_instance);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ExeCommand(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ToLuaUIFramework.LuaManager obj = (ToLuaUIFramework.LuaManager)ToLua.CheckObject<ToLuaUIFramework.LuaManager>(L, 1);
			ToLuaUIFramework.CommandEnum arg0 = (ToLuaUIFramework.CommandEnum)ToLua.CheckObject(L, 2, typeof(ToLuaUIFramework.CommandEnum));
			bool o = obj.ExeCommand(arg0);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DoFile(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ToLuaUIFramework.LuaManager obj = (ToLuaUIFramework.LuaManager)ToLua.CheckObject<ToLuaUIFramework.LuaManager>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			obj.DoFile(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFunction(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ToLuaUIFramework.LuaManager obj = (ToLuaUIFramework.LuaManager)ToLua.CheckObject<ToLuaUIFramework.LuaManager>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			LuaInterface.LuaFunction o = obj.GetFunction(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LuaGC(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ToLuaUIFramework.LuaManager obj = (ToLuaUIFramework.LuaManager)ToLua.CheckObject<ToLuaUIFramework.LuaManager>(L, 1);
			obj.LuaGC();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Close(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ToLuaUIFramework.LuaManager obj = (ToLuaUIFramework.LuaManager)ToLua.CheckObject<ToLuaUIFramework.LuaManager>(L, 1);
			obj.Close();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_instance(IntPtr L)
	{
		try
		{
			ToLua.Push(L, ToLuaUIFramework.LuaManager.instance);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_instance(IntPtr L)
	{
		try
		{
			ToLuaUIFramework.LuaManager arg0 = (ToLuaUIFramework.LuaManager)ToLua.CheckObject<ToLuaUIFramework.LuaManager>(L, 2);
			ToLuaUIFramework.LuaManager.instance = arg0;
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}
