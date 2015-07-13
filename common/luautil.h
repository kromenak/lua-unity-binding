//
//  luautil.h
//  LuaPluginiOS
//
//  Created by Clark Kromenaker on 7/11/15.
//
#include "Lua.h"

#ifndef LuaPluginiOS_luautil_h
#define LuaPluginiOS_luautil_h

int luautil_getregistryindex()
{
    return LUA_REGISTRYINDEX;
}

int luautil_getridxglobals()
{
    return LUA_RIDX_GLOBALS;
}

#endif
