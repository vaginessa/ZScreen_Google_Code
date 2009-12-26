﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;

namespace ZScreenLib.Forms
{
    public class LayeredForm : Form
    {
        public LayeredForm()
        {
            FormBorderStyle = FormBorderStyle.None;
        }

        public void DrawBitmap(Bitmap bmp)
        {
            DrawBitmap(bmp, 255);
        }

        public void DrawBitmap(Bitmap bmp, byte opacity)
        {
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;
            IntPtr screenDc = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr memDc = GDI.CreateCompatibleDC(screenDc);

            try
            {
                hBitmap = bmp.GetHbitmap(Color.FromArgb(0));
                oldBitmap = GDI.SelectObject(memDc, hBitmap);
                SIZE size = new SIZE(bmp.Width, bmp.Height);
                POINT pointSource = new POINT(this.Left, this.Top);
                POINT topPos = new POINT(0, 0);
                NativeMethods.BLENDFUNCTION blend;
                blend.BlendOp = 0;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = byte.MaxValue;
                blend.AlphaFormat = 1;
                NativeMethods.UpdateLayeredWindow(this.Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                NativeMethods.ReleaseDC(IntPtr.Zero, screenDc);

                if (hBitmap != IntPtr.Zero)
                {
                    GDI.SelectObject(memDc, oldBitmap);
                    GDI.DeleteObject(hBitmap);
                }

                GDI.DeleteDC(memDc);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00080000;
                return cp;
            }
        }
    }
}