/*******************************************
 * LuaState.cs
 *
 * AUTHOR_NAME
 *
 * PURPOSE
 *******************************************/
using System;

public struct LuaState 
{
    /***************************************
     * PUBLIC VARIABLES
     **************************************/
     
    /***************************************
     * PRIVATE VARIABLES
     **************************************/
	private IntPtr state;    

    /***************************************
     * PUBLIC METHODS
     **************************************/
	public LuaState(IntPtr ptrState) : this()
	{
		state = ptrState;
	}
	
	public static implicit operator LuaState(IntPtr ptr)
	{
		return new LuaState(ptr);
	}
	
	
	public static implicit operator IntPtr(LuaState luastate)
	{
		return luastate.state;
	} 

    /***************************************
     * PRIVATE METHODS
     **************************************/
    
}