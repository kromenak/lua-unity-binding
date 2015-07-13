/*******************************************
 * LuaNativeBinding.cs
 *
 * Clark Kromenaker
 *
 * A native binding to Lua.
 *******************************************/
using System;
using System.Runtime.InteropServices;

public static class LuaNativeBinding 
{
    /***************************************
     * PUBLIC VARIABLES
     **************************************/
     
    /***************************************
     * PRIVATE VARIABLES
     **************************************/
#if UNITY_EDITOR || UNITY_STANDALONE_OSX
	private const string kLibName = "LuaPluginOSX";
#elif UNITY_IOS
	private const string kLibName = "__Internal";
#endif

    /***************************************
     * PUBLIC METHODS
     **************************************/
	// STANDARD LIBARY (lua.h)
	// State manipulation.
	//TODO: lua_newstate

	[DllImport(kLibName)]
	public static extern void lua_close(IntPtr luaState);

	[DllImport(kLibName)]
	public static extern IntPtr lua_newthread(IntPtr luaState);

	//TODO: lua_atpanic

	//TODO: lua_version

	// Basic stack manipulation.
	[DllImport(kLibName)]
	public static extern int lua_absindex(IntPtr luaState, int index);

	[DllImport(kLibName)]
	public static extern int lua_gettop(IntPtr luaState);

	[DllImport(kLibName)]
	public static extern void lua_settop(IntPtr luaState, int index);

	[DllImport(kLibName)]
	public static extern void lua_pushvalue(IntPtr luaState, int index);

	[DllImport(kLibName)]
	public static extern void lua_remove(IntPtr luaState, int index);

	[DllImport(kLibName)]
	public static extern void lua_insert(IntPtr luaState, int index);

	[DllImport(kLibName)]
	public static extern void lua_replace(IntPtr luaState, int index);

	[DllImport(kLibName)]
	public static extern void lua_copy(IntPtr luaState, int fromIndex, int toIndex);

	[DllImport(kLibName)]
	public static extern int lua_checkstack(IntPtr luaState, int size);

	[DllImport(kLibName)]
	public static extern void lua_xmove(IntPtr luaStateFrom, IntPtr luaStateTo, int count);

	// Access functions (stack -> C)
	[DllImport(kLibName)]
	public static extern int lua_isnumber(IntPtr luaState, int index);

	[DllImport(kLibName)]
	public static extern int lua_isstring(IntPtr luaState, int index);

	[DllImport(kLibName)]
	public static extern int lua_iscfunction(IntPtr luaState, int index);

	[DllImport(kLibName)]
	public static extern int lua_isuserdata(IntPtr luaState, int index);

	[DllImport(kLibName)]
	public static extern int lua_type(IntPtr luaState, int index);

	[DllImport(kLibName)]
	public static extern IntPtr lua_typename(IntPtr luaState, int type);

	[DllImport(kLibName)]
	public static extern double lua_tonumberx(IntPtr luaState, int index, out int isNum);

	[DllImport(kLibName)]
	public static extern int lua_tointegerx(IntPtr luaState, int index, out int isNum);

	[DllImport(kLibName)]
	public static extern uint lua_tounsignedx(IntPtr luaState, int index, out int isNum);

	[DllImport(kLibName)]
	public static extern int lua_toboolean(IntPtr luaState, int index);

	[DllImport(kLibName)]
	public static extern IntPtr lua_tolstring(IntPtr luaState, int index, out uint length);

	[DllImport(kLibName)]
	public static extern uint lua_rawlen(IntPtr luaState, int index);

	[DllImport(kLibName)]
	public static extern IntPtr lua_tocfunction(IntPtr luaState, int index);

	[DllImport(kLibName)]
	public static extern IntPtr lua_touserdata(IntPtr luaState, int index);

	[DllImport(kLibName)]
	public static extern IntPtr lua_tothread(IntPtr luaState, int index);

	[DllImport(kLibName)]
	public static extern IntPtr lua_topointer(IntPtr luaState, int index);

	// Arithmetic and comparison functions.
	[DllImport(kLibName)]
	public static extern void lua_arith(IntPtr luaState, int op);

	[DllImport(kLibName)]
	public static extern int lua_rawequal(IntPtr luaState, int index1, int index2);

	[DllImport(kLibName)]
	public static extern int lua_compare(IntPtr luaState, int index1, int index2, int operation);

	// Push functions (C -> Stack).
	[DllImport(kLibName)]
	public static extern void lua_pushnil(IntPtr luaState);

	[DllImport(kLibName)]
	public static extern void lua_pushnumber(IntPtr luaState, double number);

	[DllImport(kLibName)]
	public static extern void lua_pushinteger(IntPtr luaState, int integer);

	[DllImport(kLibName)]
	public static extern void lua_pushunsigned(IntPtr luaState, uint unsigned);

	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern IntPtr lua_pushlstring(IntPtr luaState, string s, uint size);

	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern IntPtr lua_pushstring(IntPtr luaState, string s);

	//TODO: lua_pushvfstring

	//TODO: lua_pushfstring

	[DllImport(kLibName)]
	public static extern void lua_pushcclosure(IntPtr luaState, IntPtr function, int n);

	[DllImport(kLibName)]
	public static extern void lua_pushboolean(IntPtr luaState, int boolean);

	[DllImport(kLibName)]
	public static extern void lua_pushlightuserdata(IntPtr luaState, IntPtr userData);

	[DllImport(kLibName)]
	public static extern int lua_pushthread(IntPtr luaState);

	// Get functions (Lua -> Stack).
	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern void lua_getglobal(IntPtr luaState, string globalName);

	[DllImport(kLibName)]
	public static extern void lua_gettable(IntPtr luaState, int index);

	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern void lua_getfield(IntPtr luaState, int index, string fieldName);

	[DllImport(kLibName)]
	public static extern void lua_rawget(IntPtr luaState, int index);

	[DllImport(kLibName)]
	public static extern void lua_rawgeti(IntPtr luaState, int index, int n);

	[DllImport(kLibName)]
	public static extern void lua_rawgetp(IntPtr luaState, int index, IntPtr lightUserData);

	[DllImport(kLibName)]
	public static extern void lua_createtable(IntPtr luaState, int narr, int nrec);

	[DllImport(kLibName)]
	public static extern IntPtr lua_newuserdata(IntPtr luaState, uint size);

	[DllImport(kLibName)]
	public static extern int lua_getmetatable(IntPtr luaState, int objIndex);

	[DllImport(kLibName)]
	public static extern void lua_getuservalue(IntPtr luaState, int index);

	// Set functions (Stack -> Lua).
	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern void lua_setglobal(IntPtr luaState, string globalName);

	[DllImport(kLibName)]
	public static extern void lua_settable(IntPtr luaState, int index);

	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern void lua_setfield(IntPtr luaState, int index, string fieldName);

	[DllImport(kLibName)]
	public static extern void lua_rawset(IntPtr luaState, int index);

	[DllImport(kLibName)]
	public static extern void lua_rawseti(IntPtr luaState, int index, int n);

	[DllImport(kLibName)]
	public static extern void lua_rawsetp(IntPtr luaState, int index, IntPtr lightUserData);

	[DllImport(kLibName)]
	public static extern int lua_setmetatable(IntPtr luaState, int objIndex);

	[DllImport(kLibName)]
	public static extern void lua_setuservalue(IntPtr luaState, int index);

	// Load and Call functions (load and run Lua code).
	[DllImport(kLibName)]
	public static extern int lua_callk(IntPtr luaState, int nargs, int nresults, int ctx, IntPtr function);

	[DllImport(kLibName)]
	public static extern int lua_getctx(IntPtr luaState, out int ctx);

	[DllImport(kLibName)]
	public static extern int lua_pcallk(IntPtr luaState, int nargs, int nresults, int errfunc, int ctx, IntPtr function);

	//TODO: lua_load

	//TODO: lua_dump

	// Coroutine functions.
	[DllImport(kLibName)]
	public static extern int lua_yieldk(IntPtr luaState, int nresults, int ctx, IntPtr function);

	[DllImport(kLibName)]
	public static extern int lua_resume(IntPtr luaState, IntPtr from, int narg);

	[DllImport(kLibName)]
	public static extern int lua_status(IntPtr luaState);

	// Garbage collection functions.
	[DllImport(kLibName)]
	public static extern int lua_gc(IntPtr luaState, int what, int data);

	// Misc functions.
	[DllImport(kLibName)]
	public static extern int lua_error(IntPtr luaState);

	[DllImport(kLibName)]
	public static extern int lua_next(IntPtr luaState, int index);

	[DllImport(kLibName)]
	public static extern void lua_concat(IntPtr luaState, int n);

	[DllImport(kLibName)]
	public static extern void lua_len(IntPtr luaState, int index);

	//TODO: lua_getallocf

	//TODO: lua_setallocf

	//TODO: Lua Debug API

	// LUA AUX LIBRARY (lauxlib.h)
	//TODO: luaL_checkversion

	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern int luaL_getmetafield(IntPtr luaState, int index, string fieldName);

	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern int luaL_callmeta(IntPtr luaState, int index, string fieldName);

	[DllImport(kLibName)]
	public static extern IntPtr luaL_tolstring(IntPtr luaState, int index, out uint length);

	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern int luaL_argerror(IntPtr luaState, int numarg, string message);

	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern IntPtr luaL_checklstring(IntPtr luaState, int numarg, out uint length);

	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern IntPtr luaL_optlstring(IntPtr luaState, int numarg, string def, out uint length);

	[DllImport(kLibName)]
	public static extern double luaL_checknumber(IntPtr luaState, int numarg);

	[DllImport(kLibName)]
	public static extern double luaL_optnumber(IntPtr luaState, int numarg, double def);

	[DllImport(kLibName)]
	public static extern int luaL_checkinteger(IntPtr luaState, int numarg);

	[DllImport(kLibName)]
	public static extern int luaL_optinteger(IntPtr luaState, int numarg, int def);

	[DllImport(kLibName)]
	public static extern uint luaL_checkunsigned(IntPtr luaState, int numarg);

	[DllImport(kLibName)]
	public static extern uint luaL_optunsigned(IntPtr luaState, int numarg, uint def);

	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern void luaL_checkstack(IntPtr luaState, int size, string message);

	[DllImport(kLibName)]
	public static extern void luaL_checktype(IntPtr luaState, int narg, int t);

	[DllImport(kLibName)]
	public static extern void luaL_checkany(IntPtr luaState, int narg);

	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern int luaL_newmetatable(IntPtr luaState, string tableName);

	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern void luaL_setmetatable(IntPtr luaState, string tableName);

	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern IntPtr luaL_testudata(IntPtr luaState, int ud, string tableName);

	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern IntPtr luaL_checkudata(IntPtr luaState, int ud, string tableName);

	[DllImport(kLibName)]
	public static extern void luaL_where(IntPtr luaState, int lvl);

	//TODO: luaL_error

	//TODO: luaL_checkoption

	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern int luaL_fileresult(IntPtr luaState, int stat, string fileName);

	[DllImport(kLibName)]
	public static extern int luaL_execresult(IntPtr luaState, int stat);

	[DllImport(kLibName)]
	public static extern int luaL_ref(IntPtr luaState, int t);

	[DllImport(kLibName)]
	public static extern void luaL_unref(IntPtr luaState, int t, int refVal);

	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern int luaL_loadfilex(IntPtr luaState, string fileName, string mode);

	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern int luaL_loadbufferx(IntPtr luaState, string buffer, uint size, string name, string mode);

	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern int luaL_loadstring(IntPtr luaState, string s);
	
	[DllImport(kLibName)]
	public static extern IntPtr luaL_newstate();

	[DllImport(kLibName)]
	public static extern int luaL_len(IntPtr luaState, int index);

	[DllImport(kLibName)]
	public static extern IntPtr luaL_gsub(IntPtr luaState, string s, string p, string r);

	//TODO: luaL_setfuncs

	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern IntPtr luaL_getsubtable(IntPtr luaState, int index, string fName);

	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern void luaL_traceback(IntPtr luaState, IntPtr luaState1, string message, int level);

	[DllImport(kLibName, CharSet = CharSet.Ansi)]
	public static extern void luaL_requiref(IntPtr luaState, string moduleName, IntPtr openFunction, int glb);

	//TODO: Buffer manipulation functions.

	// STANDARD LIBRARIES (lualib.h)
	[DllImport(kLibName)]
	public static extern void luaL_openlibs(IntPtr luaState);

	// HELPER FUNCTIONS (luautil.h - made specifically to assist with C# binding)
	[DllImport(kLibName)]
	public static extern int luautil_getregistryindex();

	[DllImport(kLibName)]
	public static extern int luautil_getridxglobals();

    /***************************************
     * PRIVATE METHODS
     **************************************/
     
}