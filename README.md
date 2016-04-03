# lua-unity-binding
A binding between native Lua libraries and Unity. The binding isn't 100% complete, but a lot of the functionality is there.

# Updating Lua Version
If you require a different version of Lua, you can replace the version included (5.2.4) and rebuild the libraries for the various platforms (OSX, iOS, Android). For the most part, the included version of Lua requires very little adjustment to build and run.

## Building for Android
For Android, the NDK build file (jni/Android.mk) must have references to the source files in lua/src. Also, you may need to make the following adjustment to lua/src/llex.c, since Android doesn't define the "decimal_point" field:

```c
#ifndef __ANDROID__

/* Android doesn't have localeconv() */
#if !defined(getlocaledecpoint)
#define getlocaledecpoint()	(localeconv()->decimal_point[0])
#endif

#else

#if !defined(getlocaledecpoint)
#define getlocaledecpoint() ('.')
#endif

#endif
```
