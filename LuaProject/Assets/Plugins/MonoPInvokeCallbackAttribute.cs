using System;

public class MonoPInvokeCallbackAttribute : Attribute
{
	private Type type;
	public MonoPInvokeCallbackAttribute( Type t ) { type = t; }

	// Suppress compiler warning "type is not used".
	private void SuppressWarnings()
	{
		if(type == null) { }
	}
}