﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace GroupProjectWebFormSMOLPP
{
    public partial class Form1 : Form
    {

        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        public Form1()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);
            //borderless form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
        private struct RGBColors //Setting variables for my determined RGBs
        {
            public static Color colorA = Color.FromArgb(172, 126, 241);
            public static Color colorB = Color.FromArgb(249, 118, 176);
            public static Color colorC = Color.FromArgb(253, 138, 114);
            public static Color colorD = Color.FromArgb(95, 77, 221);
            public static Color colorE = Color.FromArgb(249, 88, 155);
            public static Color colorF = Color.FromArgb(24, 161, 251);
            public static Color colorG = Color.FromArgb(26, 250, 197);
        }

        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(32, 34, 37);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
                iconCurrentChildForm.IconChar = currentBtn.IconChar;
                iconCurrentChildForm.IconColor = color;
            }
        }
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(43, 45, 49);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;

            }
        }
        private void btnPath_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.colorB);
            OpenChildForm(new UserPortal());
            lblTitleChild.Text = "My Account";

        }

       private void label4_Click(object sender, EventArgs e)
        {
          
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlDsk.Controls.Add(childForm);
            pnlDsk.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitleChild.Text = childForm.Text;
        }

        private void logoBtn_Click_1(object sender, EventArgs e)
        {
            if (currentChildForm != null)
            { currentChildForm.Close(); }
            Reset();
        }


        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.Home;
            iconCurrentChildForm.IconColor = Color.MediumPurple;
            lblTitleChild.Text = "Home";
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        //imports for draggable panel
        //due to my removal of the title bar,
        //need a work-around
        //that is why in part this is so difficult
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void iconExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconExpand_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void iconMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;

        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.colorC);
            OpenChildForm(new TrackingForm());
            lblTitleChild.Text = "Your Account > Your Orders > Order Summary > Delivery Tracking";
        }
    }
}
