//
//  luautil.c
//  LuaPluginOSX
//
//  Created by Clark Kromenaker on 7/13/15.
//  Copyright (c) 2015 Hidden Variable Studios. All rights reserved.
//

#include "luautil.h"

int luautil_getregistryindex()
{
    return LUA_REGISTRYINDEX;
}

int luautil_getridxglobals()
{
    return LUA_RIDX_GLOBALS;
}