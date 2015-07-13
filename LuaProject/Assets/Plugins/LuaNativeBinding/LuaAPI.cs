/*******************************************
 * LuaAPI.cs
 *
 * Clark Kromenaker
 *
 * C# API for interacting with Lua.
 *******************************************/
using System;
using System.Runtime.InteropServices;

public static class LuaAPI 
{
    /***************************************
     * PUBLIC VARIABLES
     **************************************/
	public const int LUA_MULTRET = -1;

	// The form for all Lua C functions ever. It provides the state, you return the number of return values.
	public delegate int LuaCFunction(LuaState luaState);

	// Valid lua types.
	public enum LuaType
	{
		LUA_TNONE = -1,
		LUA_TNIL = 0,
		LUA_TBOOLEAN = 1,
		LUA_TLIGHTUSERDATA = 2,
		LUA_TNUMBER = 3,
		LUA_TSTRING = 4,
		LUA_TTABLE = 5,
		LUA_TFUNCTION = 6,
		LUA_TUSERDATA = 7,
		LUA_TTHREAD = 8,
		LUA_NUMTAGS = 9
	}

	// Valid operations for the lua_arith function.
	public enum LuaOpArith
	{
		LUA_OPADD = 0,
		LUA_OPSUB = 1,
		LUA_OPMUL = 2,
		LUA_OPDIV = 3,
		LUA_OPMOD = 4,
		LUA_OPPOW = 5,
		LUA_OPUNM = 6
	}

	// Valid operations for comparison functions.
	public enum LuaOpCompare
	{
		LUA_OPEQ = 0,
		LUA_OPLT = 1,
		LUA_OPLE = 2
	}

	// Valid actions for the lua_gc function.
	public enum LuaGCAction
	{
		LUA_GCSTOP = 0,
		LUA_GCRESTART = 1,
		LUA_GCCOLLECT = 2,
		LUA_GCCOUNT = 3,
		LUA_GCCOUNTB = 4,
		LUA_GCSTEP = 5,
		LUA_GCSETPAUSE = 6,
		LUA_GCSETSTEPMUL = 7,
		LUA_GCSETMAJORINC = 8,
		LUA_GCISRUNNING = 9,
		LUA_GCGEN = 10,
		LUA_GCINC = 11
	}

    /***************************************
     * PRIVATE VARIABLES
     **************************************/

    /***************************************
     * PUBLIC METHODS
     **************************************/
	// LUA STANDARD LIBRARY (lua.h)
	// State Manipulation
	public static void lua_close(LuaState state)
	{
		LuaNativeBinding.lua_close(state);
	}

	public static LuaState lua_newthread(LuaState state)
	{
		return LuaNativeBinding.lua_newthread(state);
	}

	// Basic Stack Manipulation
	public static int lua_absindex(LuaState state, int index)
	{
		return LuaNativeBinding.lua_absindex(state, index);
	}

	public static int lua_gettop(LuaState state)
	{
		return LuaNativeBinding.lua_gettop(state);
	}

	public static void lua_settop(LuaState state, int index)
	{
		LuaNativeBinding.lua_settop(state, index);
	}

	public static void lua_pushvalue(LuaState state, int index)
	{
		LuaNativeBinding.lua_pushvalue(state, index);
	}

	public static void lua_remove(LuaState state, int index)
	{
		LuaNativeBinding.lua_remove(state, index);
	}

	public static void lua_insert(LuaState state, int index)
	{
		LuaNativeBinding.lua_insert(state, index);
	}

	public static void lua_copy(LuaState state, int fromIndex, int toIndex)
	{
		LuaNativeBinding.lua_copy(state, fromIndex, toIndex);
	}

	public static bool lua_checkstack(LuaState state, int size)
	{
		return LuaNativeBinding.lua_checkstack(state, size) == 0 ? false : true;
	}

	public static void lua_xmove(LuaState from, LuaState to, int count)
	{
		LuaNativeBinding.lua_xmove(from, to, count);
	}

	// Access Functions (Stack -> C)
	public static bool lua_isnumber(LuaState state, int index)
	{
		return LuaNativeBinding.lua_isnumber(state, index) == 0 ? false : true;
	}

	public static bool lua_isstring(LuaState state, int index)
	{
		return LuaNativeBinding.lua_isstring(state, index) == 0 ? false : true;
	}

	public static bool lua_iscfunction(LuaState state, int index)
	{
		return LuaNativeBinding.lua_iscfunction(state, index) == 0 ? false : true;
	}

	public static bool lua_isuserdata(LuaState state, int index)
	{
		return LuaNativeBinding.lua_isuserdata(state, index) == 0 ? false : true;
	}

	public static LuaType lua_type(LuaState state, int index)
	{
		return (LuaType)LuaNativeBinding.lua_type(state, index);
	}

	public static string lua_typename(LuaState state, LuaType type)
	{
		IntPtr namePtr = LuaNativeBinding.lua_typename(state, (int)type);
		return Marshal.PtrToStringAnsi(namePtr);
	}

	public static double lua_tonumberx(LuaState state, int index, out int isNum)
	{
		return LuaNativeBinding.lua_tonumberx(state, index, out isNum);
	}

	public static int lua_tointegerx(LuaState state, int index, out int isNum)
	{
		return LuaNativeBinding.lua_tointegerx(state, index, out isNum);
	}

	public static uint lua_tounsignedx(LuaState state, int index, out int isNum)
	{
		return LuaNativeBinding.lua_tounsignedx(state, index, out isNum);
	}

	public static bool lua_toboolean(LuaState state, int index)
	{
		return LuaNativeBinding.lua_toboolean(state, index) == 0 ? false : true;
	}

	public static string lua_tolstring(LuaState state, int index, out uint length)
	{
		IntPtr stringPtr = LuaNativeBinding.lua_tolstring(state, index, out length);
		return Marshal.PtrToStringAnsi(stringPtr);
	}

	public static uint lua_rawlen(LuaState state, int index)
	{
		return LuaNativeBinding.lua_rawlen(state, index);
	}

	public static LuaCFunction lua_tocfunction(LuaState state, int index)
	{
		IntPtr cFuncPtr = LuaNativeBinding.lua_tocfunction(state, index);
		return (LuaCFunction)Marshal.GetDelegateForFunctionPointer(cFuncPtr, typeof(LuaCFunction));
	}

	public static IntPtr lua_touserdata(LuaState state, int index)
	{
		//TODO: Perhaps use GCHandle to convert IntPtr to C# instance?
		return LuaNativeBinding.lua_touserdata(state, index);
	}

	public static LuaState lua_tothread(LuaState state, int index)
	{
		return LuaNativeBinding.lua_tothread(state, index);
	}

	public static IntPtr lua_topointer(LuaState state, int index)
	{
		//TODO: Perhaps use GCHandle to convert IntPtr to C# instance?
		return LuaNativeBinding.lua_topointer(state, index);
	}

	// Arithmetic and Comparison Functions
	public static void lua_arith(LuaState state, LuaOpArith op)
	{
		LuaNativeBinding.lua_arith(state, (int)op);
	}

	public static bool lua_rawequal(LuaState state, int index1, int index2)
	{
		return LuaNativeBinding.lua_rawequal(state, index1, index2) == 0 ? false : true;
	}

	public static bool lua_compare(LuaState state, int index1, int index2, LuaOpCompare op)
	{
		return LuaNativeBinding.lua_compare(state, index1, index2, (int)op) == 0 ? false : true;
	}

	// Push Functions (C -> Stack)
	public static void lua_pushnil(LuaState state)
	{
		LuaNativeBinding.lua_pushnil(state);
	}

	public static void lua_pushnumber(LuaState state, double number)
	{
		LuaNativeBinding.lua_pushnumber(state, number);
	}

	public static void lua_pushinteger(LuaState state, int integer)
	{
		LuaNativeBinding.lua_pushinteger(state, integer);
	}

	public static void lua_pushunsigned(LuaState state, uint unsigned)
	{
		LuaNativeBinding.lua_pushunsigned(state, unsigned);
	}

	public static string lua_pushlstring(LuaState state, string s)
	{
		// This version can't handle a null string.
		if(s == null) { return null; }
		IntPtr stringPtr = LuaNativeBinding.lua_pushlstring(state, s, (uint)s.Length);
		return Marshal.PtrToStringAnsi(stringPtr);
	}

	public static string lua_pushstring(LuaState state, string s)
	{
		// This version is designed to handle null strings correctly.
		IntPtr stringPtr = LuaNativeBinding.lua_pushstring(state, s);
		return Marshal.PtrToStringAnsi(stringPtr);
	}

	public static void lua_pushcclosure(LuaState state, LuaCFunction function, int functionArgCount)
	{
		IntPtr funcPtr = Marshal.GetFunctionPointerForDelegate(function);
		LuaNativeBinding.lua_pushcclosure(state, funcPtr, functionArgCount);
	}

	public static void lua_pushboolean(LuaState state, bool boolVal)
	{
		LuaNativeBinding.lua_pushboolean(state, boolVal ? 1 : 0);
	}

	// TODO: Can potentially take in the C# instance itself and use GCHandle to create pointer?
	public static void lua_pushlightuserdata(LuaState state, IntPtr userDataPointer)
	{
		LuaNativeBinding.lua_pushlightuserdata(state, userDataPointer);
	}

	public static int lua_pushthread(LuaState state)
	{
		return LuaNativeBinding.lua_pushthread(state);
	}

	// Get Functions (Lua -> Stack)
	public static void lua_getglobal(LuaState state, string globalName)
	{
		LuaNativeBinding.lua_getglobal(state, globalName);
	}

	public static void lua_gettable(LuaState state, int index)
	{
		LuaNativeBinding.lua_gettable(state, index);
	}

	public static void lua_getfield(LuaState state, int index, string fieldName)
	{
		LuaNativeBinding.lua_getfield(state, index, fieldName);
	}

	public static void lua_rawget(LuaState state, int index)
	{
		LuaNativeBinding.lua_rawget(state, index);
	}

	public static void lua_rawgeti(LuaState state, int index, int tableKey)
	{
		LuaNativeBinding.lua_rawgeti(state, index, tableKey);
	}

	// TODO: Can potentially take in the C# instance itself and use GCHandle to create pointer?
	public static void lua_rawgetp(LuaState state, int index, IntPtr userDataPointer)
	{
		LuaNativeBinding.lua_rawgetp(state, index, userDataPointer);
	}

	public static void lua_createtable(LuaState state, int sequenceSizeHint, int otherSizeHint)
	{
		LuaNativeBinding.lua_createtable(state, sequenceSizeHint, otherSizeHint);
	}

	// TODO: IntPtr points to some raw memory. May be able to convert to a byte array or something for immediate use?
	public static IntPtr lua_newuserdata(LuaState state, uint size)
	{
		return LuaNativeBinding.lua_newuserdata(state, size);
	}

	public static int lua_getmetatable(LuaState state, int objIndex)
	{
		return LuaNativeBinding.lua_getmetatable(state, objIndex);
	}

	public static void lua_getuservalue(LuaState state, int index)
	{
		LuaNativeBinding.lua_getuservalue(state, index);
	}

	// Set Functions (Stack -> Lua)
	public static void lua_setglobal(LuaState state, string globalName)
	{
		LuaNativeBinding.lua_setglobal(state, globalName);
	}

	public static void lua_settable(LuaState state, int index)
	{
		LuaNativeBinding.lua_settable(state, index);
	}

	public static void lua_setfield(LuaState state, int index, string fieldName)
	{
		LuaNativeBinding.lua_setfield(state, index, fieldName);
	}

	public static void lua_rawset(LuaState state, int index)
	{
		LuaNativeBinding.lua_rawset(state, index);
	}

	public static void lua_rawseti(LuaState state, int index, int tableKey)
	{
		LuaNativeBinding.lua_rawseti(state, index, tableKey);
	}

	// TODO: Can potentially take in the C# instance itself and use GCHandle to create pointer?
	public static void lua_rawsetp(LuaState state, int index, IntPtr userDataPointer)
	{
		LuaNativeBinding.lua_rawsetp(state, index, userDataPointer);
	}

	public static int lua_setmetatable(LuaState state, int objIndex)
	{
		return LuaNativeBinding.lua_setmetatable(state, objIndex);
	}

	public static void lua_setuservalue(LuaState state, int index)
	{
		LuaNativeBinding.lua_setuservalue(state, index);
	}

	// Load and Call Functions (Load and Run Lua Code)
	public static void lua_callk(LuaState state, int argCount, int resultCount, int ctx, LuaCFunction function)
	{
		IntPtr funcPtr = Marshal.GetFunctionPointerForDelegate(function);
		LuaNativeBinding.lua_callk(state, argCount, resultCount, ctx, funcPtr);
	}

	public static void lua_call(LuaState state, int argCount, int resultCount)
	{
		LuaNativeBinding.lua_callk(state, argCount, resultCount, 0, IntPtr.Zero);
	}

	public static int lua_getctx(LuaState state, out int ctx)
	{
		return LuaNativeBinding.lua_getctx(state, out ctx);
	}

	public static void lua_pcallk(LuaState state, int argCount, int resultCount, int errFunc, int ctx, LuaCFunction function)
	{
		IntPtr funcPtr = Marshal.GetFunctionPointerForDelegate(function);
		LuaNativeBinding.lua_pcallk(state, argCount, resultCount, errFunc, ctx, funcPtr);
	}

	public static void lua_pcall(LuaState luaState, int argCount, int resultCount, int errFunc)
	{
		LuaNativeBinding.lua_pcallk(luaState, argCount, resultCount, errFunc, 0, IntPtr.Zero);
	}

	//TODO: lua_load

	//TODO: lua_dump

	// Coroutine Functions
	public static int lua_yieldk(LuaState state, int resultCount, int ctx, LuaCFunction function)
	{
		IntPtr funcPtr = Marshal.GetFunctionPointerForDelegate(function);
		return LuaNativeBinding.lua_yieldk(state, resultCount, ctx, funcPtr);
	}

	public static int lua_yield(LuaState state, int resultCount)
	{
		return LuaNativeBinding.lua_yieldk(state, resultCount, 0, IntPtr.Zero);
	}

	public static int lua_resume(LuaState state, LuaState from, int argCount)
	{
		return LuaNativeBinding.lua_resume(state, from, argCount);
	}

	public static int lua_status(LuaState state)
	{
		return LuaNativeBinding.lua_status(state);
	}

	// Garbage Collection Functions
	public static int lua_gc(LuaState state, LuaGCAction what, int data)
	{
		return LuaNativeBinding.lua_gc(state, (int)what, data);
	}

	// Misc Functions
	public static int lua_error(LuaState state)
	{
		return LuaNativeBinding.lua_error(state);
	}

	public static int lua_next(LuaState state, int index)
	{
		return LuaNativeBinding.lua_next(state, index);
	}

	public static void lua_concat(LuaState state, int count)
	{
		LuaNativeBinding.lua_concat(state, count);
	}

	public static void lua_len(LuaState state, int index)
	{
		LuaNativeBinding.lua_len(state, index);
	}

	//TODO: lua_getallocf
	//TODO: lua_setallocf

	// Additional Functions (in C, Macros from other Functionality)
	public static double lua_tonumber(LuaState state, int index)
	{
		int isNum;
		return lua_tonumberx(state, index, out isNum);
	}

	public static int lua_tointeger(LuaState state, int index)
	{
		int isNum;
		return lua_tointegerx(state, index, out isNum);
	}

	public static uint lua_tounsigned(LuaState state, int index)
	{
		int isNum;
		return lua_tounsignedx(state, index, out isNum);
	}

	public static void lua_pop(LuaState state, int count)
	{
		lua_settop(state, -(count) - 1);
	}

	public static void lua_newtable(LuaState state)
	{
		lua_createtable(state, 0, 0);
	}

	public static void lua_register(LuaState state, string name, LuaCFunction func)
	{
		lua_pushcclosure(state, func, 0);
		lua_setglobal(state, name);
	}

	public static void lua_pushcfunction(LuaState state, LuaCFunction func)
	{
		lua_pushcclosure(state, func, 0);
	}

	public static bool lua_isfunction(LuaState state, int index)
	{
		return lua_type(state, index) == LuaType.LUA_TFUNCTION;
	}

	public static bool lua_istable(LuaState state, int index)
	{
		return lua_type(state, index) == LuaType.LUA_TTABLE;
	}

	public static bool lua_islightuserdata(LuaState state, int index)
	{
		return lua_type(state, index) == LuaType.LUA_TLIGHTUSERDATA;
	}

	public static bool lua_isnil(LuaState state, int index)
	{
		return lua_type(state, index) == LuaType.LUA_TNIL;
	}

	public static bool lua_isboolean(LuaState state, int index)
	{
		return lua_type(state, index) == LuaType.LUA_TBOOLEAN;
	}

	public static bool lua_isthread(LuaState state, int index)
	{
		return lua_type(state, index) == LuaType.LUA_TTHREAD;
	}

	public static bool lua_isnone(LuaState state, int index)
	{
		return lua_type(state, index) == LuaType.LUA_TNONE;
	}

	public static bool lua_isnoneornil(LuaState state, int index)
	{
		LuaType type = lua_type(state, index);
		return type == LuaType.LUA_TNONE || type == LuaType.LUA_TNIL;
	}

	//TODO: lua_pushliteral

	public static void lua_pushglobaltable(LuaState state)
	{
		lua_rawgeti(state, LuaNativeBinding.luautil_getregistryindex(), LuaNativeBinding.luautil_getridxglobals());
	}

	public static string lua_tostring(LuaState luaState, int index)
	{
		uint len;
		IntPtr ptr = LuaNativeBinding.lua_tolstring(luaState, index, out len);
		return Marshal.PtrToStringAnsi(ptr);
	}

	//TODO: Debug API

	// LUA AUX LIBRARY (lauxlib.h)
	public static int luaL_getmetafield(LuaState state, int index, string fieldName)
	{
		return LuaNativeBinding.luaL_getmetafield(state, index, fieldName);
	}

	public static int luaL_callmeta(LuaState state, int index, string fieldName)
	{
		return LuaNativeBinding.luaL_callmeta(state, index, fieldName);
	}

	public static string luaL_tolstring(LuaState state, int index, out uint length)
	{
		IntPtr stringPtr = LuaNativeBinding.luaL_tolstring(state, index, out length);
		return Marshal.PtrToStringAnsi(stringPtr);
	}

	public static int luaL_argerror(LuaState state, int argNum, string message)
	{
		return LuaNativeBinding.luaL_argerror(state, argNum, message);
	}

	public static string luaL_checklstring(LuaState state, int argNum, out uint length)
	{
		IntPtr stringPtr = LuaNativeBinding.luaL_checklstring(state, argNum, out length);
		return Marshal.PtrToStringAnsi(stringPtr);
	}

	public static string luaL_optlstring(LuaState state, int argNum, string def, out uint length)
	{
		IntPtr stringPtr = LuaNativeBinding.luaL_optlstring(state, argNum, def, out length);
		return Marshal.PtrToStringAnsi(stringPtr);
	}

	public static double luaL_checknumber(LuaState state, int argNum)
	{
		return LuaNativeBinding.luaL_checknumber(state, argNum);
	}

	public static double luaL_optnumber(LuaState state, int argNum, double def)
	{
		return LuaNativeBinding.luaL_optnumber(state, argNum, def);
	}

	public static int luaL_checkinteger(LuaState state, int argNum)
	{
		return LuaNativeBinding.luaL_checkinteger(state, argNum);
	}

	public static int luaL_optinteger(LuaState state, int argNum, int def)
	{
		return LuaNativeBinding.luaL_optinteger(state, argNum, def);
	}

	public static uint luaL_checkunsigned(LuaState state, int argNum)
	{
		return LuaNativeBinding.luaL_checkunsigned(state, argNum);
	}

	public static uint luaL_optunsigned(LuaState state, int argNum, uint def)
	{
		return LuaNativeBinding.luaL_optunsigned(LuaState, argNum, def);
	}

	public static void luaL_checkstack(LuaState state, int size, string message)
	{
		LuaNativeBinding.luaL_checkstack(state, size, message);
	}

	public static void luaL_checktype(LuaState state, int argNum, LuaType type)
	{
		LuaNativeBinding.luaL_checktype(state, argNum, (int)type);
	}

	public static void luaL_checkany(LuaState state, int argNum)
	{
		LuaNativeBinding.luaL_checkany(state, argNum);
	}

	public static int luaL_newmetatable(LuaState state, string tableName)
	{
		return LuaNativeBinding.luaL_newmetatable(state, tableName);
	}

	public static void luaL_setmetatable(LuaState state, string tableName)
	{
		LuaNativeBinding.luaL_setmetatable(state, tableName);
	}

	// TODO: Could maybe do something with GCHandle to pass back a C# instance instead of IntPtr.
	public static IntPtr luaL_testudata(LuaState state, int ud, string tableName)
	{
		return LuaNativeBinding.luaL_testudata(state, ud, tableName);
	}

	// TODO: Could maybe do something with GCHandle to pass back a C# instance instead of IntPtr.
	public static IntPtr luaL_checkudata(LuaState state, int ud, string tableName)
	{
		return LuaNativeBinding.luaL_checkudata(state, ud, tableName);
	}

	public static void luaL_where(LuaState state, int lvl)
	{
		LuaNativeBinding.luaL_where(state, lvl);
	}

	//TODO: luaL_error

	//TODO: luaL_checkoption

	public static int luaL_fileresult(LuaState state, int stat, string fileName)
	{
		return LuaNativeBinding.luaL_fileresult(state, stat, fileName);
	}

	public static int luaL_execresult(LuaState state, int stat)
	{
		return LuaNativeBinding.luaL_execresult(state, stat);
	}

	public static int luaL_ref(LuaState state, int index)
	{
		return LuaNativeBinding.luaL_ref(state, index);
	}

	public static void luaL_unref(LuaState state, int index, int refVal)
	{
		LuaNativeBinding.luaL_unref(state, index, refVal);
	}

	public static int luaL_loadfilex(LuaState state, string fileName, string mode)
	{
		return LuaNativeBinding.luaL_loadfilex(state, fileName, mode);
	}

	public static int luaL_loadfile(LuaState state, string fileName)
	{
		return luaL_loadfilex(state, fileName, null);
	}

	public static int luaL_loadbufferx(LuaState state, string buffer, string name, string mode) 
	{
		return LuaNativeBinding.luaL_loadbufferx(state, buffer, (uint)buffer.Length, name, mode);
	}

	public static void luaL_loadstring(LuaState luaState, string s)
	{
		LuaNativeBinding.luaL_loadstring(luaState, s);
	}

    public static LuaState luaL_newstate() 
	{
		return LuaNativeBinding.luaL_newstate();
	}

	public static int luaL_len(LuaState state, int index)
	{
		return LuaNativeBinding.luaL_len(state, index);
	}

	public static string luaL_gsub(LuaState state, string s, string p, string r)
	{
		IntPtr stringPtr = LuaNativeBinding.luaL_gsub(state, s, p, r);
		return Marshal.PtrToStringAnsi(stringPtr);
	}

	//TODO: luaL_setfuncs

	public static int luaL_getsubtable(LuaState state, int index, string fName)
	{
		return LuaNativeBinding.luaL_getsubtable(state, index, fName);
	}

	public static void luaL_traceback(LuaState L, LuaState L1, string message, int level)
	{
		LuaNativeBinding.luaL_traceback(L, L1, message, level);
	}

	public static void luaL_requiref(LuaState state, string moduleName, LuaCFunction openFunc, int glb) 
	{
		IntPtr funcPtr = Marshal.GetFunctionPointerForDelegate(openFunc);
		LuaNativeBinding.luaL_requiref(state, moduleName, funcPtr, glb);
	}

	//TODO: luaL_newlibtable

	//TODO: luaL_newlib

	//TODO: luaL_argcheck

	public static string luaL_checkstring(LuaState state, int argNum)
	{
		uint length;
		return luaL_checklstring(state, argNum, out length);
	}

	public static string luaL_optstring(LuaState state, int argNum, string def)
	{
		uint length;
		return luaL_optlstring(state, argNum, def, out length);
	}

	//TODO: luaL_checkint
	//TODO: luaL_optint
	//TODO: luaL_checklong
	//TODO: luaL_optlong

	public static string luaL_typename(LuaState state, int index)
	{
		return lua_typename(state, lua_type(state, index));
	}

	public static void luaL_dofile(LuaState state, string fileName)
	{
		luaL_loadfile(state, fileName);
		lua_pcall(state, 0, LUA_MULTRET, 0);
	}

	public static void luaL_dostring(LuaState state, string s)
	{
		luaL_loadstring(state, s);
		lua_pcall(state, 0, LUA_MULTRET, 0);
	}

	public static void luaL_getmetatable(LuaState state, string fieldName)
	{
		lua_getfield(state, LuaNativeBinding.luautil_getregistryindex(), fieldName);
	}

	//TODO: luaL_opt

	public static int luaL_loadbuffer(LuaState state, string buffer, string name)
	{
		return luaL_loadbufferx(state, buffer, name, null);
	}

	//TODO: buffer manipulation


	// LUA STANDARD LIBRARIES (lualib.h)
	public static void luaL_openlibs(LuaState luaState)
	{
		LuaNativeBinding.luaL_openlibs(luaState);
	}

    /***************************************
     * PRIVATE METHODS
     **************************************/ 
}