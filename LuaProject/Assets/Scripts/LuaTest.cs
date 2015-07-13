/*******************************************
 * LuaTest.cs
 *
 * AUTHOR_NAME
 *
 * PURPOSE
 *******************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LuaTest : MonoBehaviour 
{
    /***************************************
     * INSPECTOR VARIABLES
     **************************************/
     
    /***************************************
     * PRIVATE VARIABLES
     **************************************/

    /***************************************
     * UNITY METHODS
     **************************************/
    private void Start()
    {
		LuaState state = LuaAPI.luaL_newstate();
		LuaAPI.luaL_openlibs(state);

		LuaAPI.lua_register(state, "print", lua_print);

		LuaAPI.luaL_loadstring(state, "print(\"Hello World\")");
		LuaAPI.lua_pcall(state, 0, 0, 0);
    }
        
    /***************************************
     * PUBLIC METHODS
     **************************************/
     
    /***************************************
     * PRIVATE METHODS
     **************************************/
	[MonoPInvokeCallback(typeof(LuaAPI.LuaCFunction))]
    private static int lua_print(LuaState L)
	{
		string s = LuaAPI.lua_tostring(L, 1);
		Debug.Log(s);
		return 0;
	}
}