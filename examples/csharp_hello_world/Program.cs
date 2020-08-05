using System;
using System.Runtime.InteropServices;
using GLFW;
using Bgfx;

// https://github.com/ForeverZer0/glfw-net
// https://github.com/MikePopoloski/SharpBgfx/tree/master/examples/00-HelloWorld

// https://github.com/bkaradzic/bgfx/issues/1851
// https://github.com/flibitijibibo/SDL2-CS

namespace csharp_hello_world
{
    class Program
    {
        private const string TITLE = "Simple Window";
        private const int WIDTH = 1024;
        private const int HEIGHT = 800;

        private const int GL_COLOR_BUFFER_BIT = 0x00004000;


        private delegate void glClearColorHandler(float r, float g, float b, float a);
        private delegate void glClearHandler(int mask);

        private static glClearColorHandler glClearColor;
        private static glClearHandler glClear;

        private static Random rand;

        static void Main(string[] args)
        {
            // Set some common hints for the OpenGL profile creation
            Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.Doublebuffer, true);
            Glfw.WindowHint(Hint.Decorated, true);
            Glfw.WindowHint(Hint.OpenglForwardCompatible, true);

            rand = new Random();

#if OBJECTORIENTED
            // // The object oriented approach
            // using (var window = new NativeWindow(WIDTH, HEIGHT, title))
            // {
            //     window.CenterOnScreen();
            //     window.KeyPress += WindowOnKeyPress;
            //     while (!window.IsClosing)
            //     {
            //         Glfw.PollEvents();
            //         window.SwapBuffers();
            //     }
            // }

#else

            // Create window
            var window = Glfw.CreateWindow(WIDTH, HEIGHT, TITLE, Monitor.None, Window.None);
            Glfw.MakeContextCurrent(window);

            // Effectively enables VSYNC by setting to 1.
            Glfw.SwapInterval(1);

            // Find center position based on window and monitor sizes
            var screenSize = Glfw.PrimaryMonitor.WorkArea;
            var x = (screenSize.Width - WIDTH) / 2;
            var y = (screenSize.Height - HEIGHT) / 2;
            Glfw.SetWindowPosition(window, x, y);

            // Set a key callback
            //Glfw.SetKeyCallback(window, KeyCallback);


            glClearColor = Marshal.GetDelegateForFunctionPointer<glClearColorHandler>(Glfw.GetProcAddress("glClearColor"));
            glClear = Marshal.GetDelegateForFunctionPointer<glClearHandler>(Glfw.GetProcAddress("glClear"));


            var tick = 0L;
            ChangeRandomColor();

            while (!Glfw.WindowShouldClose(window))
            {
                // Poll for OS events and swap front/back buffers
                Glfw.PollEvents();
                Glfw.SwapBuffers(window);

                // Change background color to something random every 60 draws
                if (tick++ % 60 == 0)
                    ChangeRandomColor();

                // Clear the buffer to the set color
                glClear(GL_COLOR_BUFFER_BIT);
            }
#endif
        }

        private static void ChangeRandomColor()
        {
            var r = (float)rand.NextDouble();
            var g = (float)rand.NextDouble();
            var b = (float)rand.NextDouble();
            glClearColor(r, g, b, 1.0f);
        }

#if OBJECTORIENTED

        private static void WindowOnKeyPress(object? sender, KeyEventArgs e)
        {
            var window = (NativeWindow) sender;
            switch (e.Key)
            {
                case Keys.Escape:
                    window.Close();
                    break;
                // ...and so on....
            }
        }
#else

        private static void KeyCallback(Window window, Keys key, int scancode, InputState state, ModifierKeys mods)
        {
            switch (key)
            {
                case Keys.Escape:
                    Glfw.SetWindowShouldClose(window, true);
                    break;
            }
        }
#endif
    }

    //public class DllFunctionException : Exception
    //{
    //    public DllFunctionException(string message)
    //        : base(message)
    //    {
    //    }
    //}

    //public class WindowLifeCycleException : Exception
    //{
    //    public WindowLifeCycleException()
    //        : base()
    //    {
    //    }
    //}

    //public static class Window
    //{
    //    private static IntPtr sMainWindow;

    //    static Window()
    //    {
    //        sMainWindow = IntPtr.Zero;
    //    }

    //    public unsafe static void Init()
    //    {
    //        if (sMainWindow != IntPtr.Zero)
    //        {
    //            throw new WindowLifeCycleException();
    //        }

    //        {
    //            SDL.SDL_SetMainReady();
    //        }

    //        {
    //            int result;

    //            result = SDL.SDL_Init(SDL.SDL_INIT_VIDEO | SDL.SDL_INIT_EVENTS);
    //            if (result != 0)
    //            {
    //                string functionName = "SDL_Init";
    //                int functionReturn = result;
    //                string functionError = SDL.SDL_GetError();

    //                throw new DllFunctionException("SDL2-> function: " + functionName + "; return: " + functionReturn + "; error: " + functionError + ".");
    //            }
    //        }

    //        {
    //            IntPtr result;

    //            string title = "my game title";
    //            int x = SDL.SDL_WINDOWPOS_CENTERED;
    //            int y = SDL.SDL_WINDOWPOS_CENTERED;
    //            int width = 600;
    //            int height = 400;
    //            SDL.SDL_WindowFlags flags = SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE;

    //            result = SDL.SDL_CreateWindow(title, x, y, width, height, flags);
    //            if (result == IntPtr.Zero)
    //            {
    //                SDL.SDL_Quit();

    //                string functionName = "SDL_CreateWindow";
    //                IntPtr functionReturn = result;
    //                string functionError = SDL.SDL_GetError();

    //                throw new DllFunctionException("SDL2-> function: " + functionName + "; return: " + functionReturn + "; error: " + functionError + ".");
    //            }

    //            sMainWindow = result;
    //        }

    //        {
    //            SDL.SDL_SysWMinfo info = new SDL.SDL_SysWMinfo();

    //            SDL.SDL_bool result = SDL.SDL_GetWindowWMInfo(sMainWindow, ref info);
    //            if (result != SDL.SDL_bool.SDL_TRUE)
    //            {
    //                SDL.SDL_DestroyWindow(sMainWindow);
    //                SDL.SDL_Quit();

    //                string functionName = "SDL_GetWindowWMInfo";
    //                SDL.SDL_bool functionReturn = result;
    //                string functionError = SDL.SDL_GetError();

    //                throw new DllFunctionException("SDL2-> function: " + functionName + "; return: " + functionReturn + "; error: " + functionError + ".");
    //            }

    //            bgfx.Init init;
    //            bgfx.init_ctor(&init);

    //            init.platformData.ndt = null;
    //            init.platformData.nwh = info.info.win.window.ToPointer();

    //            init.resolution.width = 600;
    //            init.resolution.height = 400;
    //            init.resolution.reset = (uint)bgfx.ResetFlags.Vsync;

    //            if (bgfx.init(&init) == false)
    //            {
    //                SDL.SDL_DestroyWindow(sMainWindow);
    //                SDL.SDL_Quit();

    //                string functionName = "bgfx_init";
    //                bool functionReturn = false;
    //                string functionError = "no error message";

    //                throw new DllFunctionException("bgfx-> function: " + functionName + "; return: " + functionReturn + "; error: " + functionError + ".");
    //            }

    //            bgfx.set_debug((uint)bgfx.DebugFlags.Text);
    //            bgfx.set_view_clear(0, (ushort)(bgfx.ClearFlags.Color | bgfx.ClearFlags.Depth), 0x303030ff, 1.0f, 0);
    //        }
    //    }
    //    public unsafe static void Quit()
    //    {
    //        if (sMainWindow == IntPtr.Zero)
    //        {
    //            throw new WindowLifeCycleException();
    //        }

    //        bgfx.shutdown();

    //        SDL.SDL_DestroyWindow(sMainWindow);
    //        SDL.SDL_Quit();

    //        sMainWindow = IntPtr.Zero;
    //    }
    //    public unsafe static bool Run()
    //    {
    //        SDL.SDL_Event event_;

    //        while (SDL.SDL_PollEvent(out event_) != 0)
    //        {
    //            if (event_.type == SDL.SDL_EventType.SDL_QUIT)
    //            {
    //                return false;
    //            }
    //        }

    //        int width;
    //        int height;
    //        SDL.SDL_GetWindowSize(sMainWindow, out width, out height);

    //        bgfx.set_view_rect(0, 0, 0, Convert.ToUInt16(width), Convert.ToUInt16(height));

    //        bgfx.touch(0);

    //        bgfx.dbg_text_clear(0, false);
    //        bgfx.dbg_text_printf(0, 1, 0x0f, "What??", null);

    //        bgfx.frame(false);

    //        return true;
    //    }
    //}

    //public static class Application
    //{
    //    static void Test()
    //    {
    //        Window.Init();

    //        while (Window.Run())
    //        {
    //            Thread.Sleep(100);
    //        }

    //        Window.Quit();
    //    }

    //    static bool IsContinue()
    //    {
    //        while (true)
    //        {
    //            Console.Clear();
    //            Console.WriteLine("1. continue");
    //            Console.WriteLine("0. exit");

    //            ConsoleKeyInfo keyInfo = Console.ReadKey();

    //            switch (keyInfo.Key)
    //            {
    //                case ConsoleKey.NumPad0:
    //                    Console.Clear();
    //                    return false;

    //                case ConsoleKey.NumPad1:
    //                    Console.Clear();
    //                    return true;

    //                default:
    //                    Console.WriteLine();

    //                    Console.WriteLine("Unknown command.");
    //                    Console.WriteLine("pasue...");

    //                    Console.ReadKey();
    //                    break;
    //            }
    //        }
    //    }

    //    public static void Main(string[] args)
    //    {
    //        while (IsContinue())
    //        {
    //            Test();
    //        }
    //    }
    //}
}
