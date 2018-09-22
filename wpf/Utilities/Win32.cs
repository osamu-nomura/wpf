using System;
using System.Runtime.InteropServices;
using System.Text;

namespace wpf.Utilities
{
    #region 【Class : Win32】
    /// <summary>
    /// Win32 API定義 クラス
    /// </summary>
    public static class Win32
    {
        #region ■ DLL Import Methods

        #region - AllowSetForegroundWindow
        /// <summary>
        /// 他プロセスからのSetForegroundWindowを許可する
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool AllowSetForegroundWindow(int dwProcessId);
        #endregion

        #region - SetForegroundWindow 
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        #endregion

        #region - ShowWindowCommands
        public enum ShowWindowCommands
        {
            Hide = 0,
            Normal = 1,
            ShowMinimized = 2,
            Maximize = 3, // is this the right value?
            ShowMaximized = 3,
            ShowNoActivate = 4,
            Show = 5,
            Minimize = 6,
            ShowMinNoActive = 7,
            ShowNA = 8,
            Restore = 9,
            ShowDefault = 10,
            ForceMinimize = 11
        }
        #endregion

        #region - ShowWindow
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);
        #endregion

        #region - GetWindowThreadProcessId
        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref uint procId);
        #endregion

        #region - GetClassName
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        #endregion

        #region - EnumWindowsProc
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        #endregion

        #region - EnumWindows
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
        #endregion

        #region - SendMessage
        [DllImport("user32.dll")]//, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint Msg, IntPtr wParam, IntPtr lParam);
        #endregion

        #endregion
    }
    #endregion
}
