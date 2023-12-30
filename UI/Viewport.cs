using UI.Classes.DrawList;
using UI.Internal.Sections.ViewportP;
using System.Numerics;

// ported from src/viewport.kt

namespace UI
{
    //-----------------------------------------------------------------------------
    // [SECTION] Viewports
    //-----------------------------------------------------------------------------

    public enum ViewportFlags
    {
        IsPlatformWindow,
        IsPlatformMonitor,
        OwnedByApp,
    }

    // - Currently represents the Platform Window created by the application which is hosting our Dear ImGui windows.
    // - In 'docking' branch with multi-viewport enabled, we extend this concept to have multiple active viewports.
    // - In the future we will extend this concept further to also represent Platform Monitor and support a "no main platform window" operation mode.
    // - About Main Area vs Work Area:
    //   - Main Area = entire viewport.
    //   - Work Area = entire viewport minus sections used by main menu bars (for platform windows), or by task bar (for platform monitor).
    //   - Windows are generally trying to stay within the Work Area of their host viewport.
    public class Viewport
    {
        /** See ImGuiViewportFlags_ */
        public ViewportFlags flags = null;

        /** Main Area: Position of the viewport (Dear ImGui coordinates are the same as OS desktop/native coordinates) */
        public Vector2 Pos = new Vector2();

        /** Main Area: Size of the viewport. */
        public Vector2 Size = new Vector2();

        /** Work Area: Position of the viewport minus task bars, menus bars, status bars (>= Pos) */
        public Vector2 WorkPos = new Vector2();

        /** Work Area: Size of the viewport minus task bars, menu bars, status bars (<= Size) */
        public Vector2 WorkSize = new Vector2();

        /** Platform/Backend Dependent Data
        *
        *  void* to hold lower-level, platform-native window handle (under Win32 this is expected to be a HWND, unused for other platforms) */
        public object platformHandleRaw = null;

        // Helpers
        public Vector2 Center
        {
            get { return new Vector2(Pos.X + Size.X * 0.5f, Pos.Y + Size.Y * 0.5f); }
        }



        public Vector2 WorkCenter
        {
            get { return new Vector2(WorkPos.X + WorkSize.X * 0.5f, WorkPos.Y + WorkSize.Y * 0.5f)}
        }

        /** !GetBackgroundDrawList(ImGuiViewport* viewport)
        *  get background draw list for the given viewport. this draw list will be the first rendering one. Useful to quickly draw shapes/text behind dear imgui contents.
        */
        public DrawList BackgroundDrawList
        {
            get
            {
                return this(ViewportP).GetDrawList(0, "##Background");
            }
        }

        /** get foreground draw list for the given viewport. this draw list will be the last rendered one. Useful to quickly draw shapes/text over dear imgui contents. */
        public DrawList GoregroundDrawList
        {
            get
            {
                return this(ViewportP).GetDrawList(1, "##Foreground");
            }
        }
    }

    //-----------------------------------------------------------------------------
    // [SECTION] Platform Dependent Interfaces
    //-----------------------------------------------------------------------------

    /** (Optional) Support for IME (Input Method Editor) via the io.SetPlatformImeDataFn() function. */
    public struct PlatformImeData
    {
        /** A widget wants the IME to be visible */
        public bool WantVisible = false;
        /** Position of the input cursor */
        public Vector2 InputPos = new Vector2();
        /** Line height */
        public float InputLineHeight = 0f;

        public PlatformImeData(PlatformImeData data)
        {
            this.WantVisible = data.WantVisible;
            this.InputPos = data.InputPos;
            this.InputLineHeight = data.InputLineHeight;
        }
    }
}