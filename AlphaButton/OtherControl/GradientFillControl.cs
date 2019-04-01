using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Drawing;

namespace AlphaControls
{
    /// <summary>
    /// 背景渐变填充
    /// </summary>
    public sealed class GradientFill
    {
        // This method wraps the PInvoke to GradientFill.
        // Parmeters:
        //  gr - The Graphics object we are filling
        //  rc - The rectangle to fill
        //  startColor - The starting color for the fill
        //  endColor - The ending color for the fill
        //  fillDir - The direction to fill
        //
        // Returns true if the call to GradientFill succeeded; false
        // otherwise.
        public static bool Fill(
            Graphics gr,
            Rectangle rc,
            Color startColor, Color endColor,
            FillDirection fillDir)
        {

            // Initialize the data to be used in the call to GradientFill.
            Microsoft.Drawing.TRIVERTEX[] tva = new Microsoft.Drawing.TRIVERTEX[2];
            tva[0] = new Microsoft.Drawing.TRIVERTEX(rc.X, rc.Y, startColor);
            tva[1] = new Microsoft.Drawing.TRIVERTEX(rc.Right, rc.Bottom, endColor);
            Microsoft.Drawing.GRADIENT_RECT[] gra = new Microsoft.Drawing.GRADIENT_RECT[] {
    new Microsoft.Drawing.GRADIENT_RECT(0, 1)};

            // Get the hDC from the Graphics object.
            IntPtr hdc = gr.GetHdc();

            // PInvoke to GradientFill.
            bool b;

            b = Win32Helper.GradientFill(
                    hdc,
                    tva,
                    (uint)tva.Length,
                    gra,
                    (uint)gra.Length,
                    (uint)fillDir);
            System.Diagnostics.Debug.Assert(b, string.Format(
                "GradientFill failed: {0}",
                System.Runtime.InteropServices.Marshal.GetLastWin32Error()));

            // Release the hDC from the Graphics object.
            gr.ReleaseHdc(hdc);

            return b;
        }

        // The direction to the GradientFill will follow
        public enum FillDirection
        {
            //
            // The fill goes horizontally
            //
            LeftToRight = Win32Helper.GRADIENT_FILL_RECT_H,
            //
            // The fill goes vertically
            //
            TopToBottom = Win32Helper.GRADIENT_FILL_RECT_V
        }
    }

    // Extends the standard button control and performs
    // custom drawing with a GradientFill background.

    /// <summary>
    /// 背景渐变填充的按钮
    /// </summary>
    public class GradientFilledButton : Control, IDisposable
    {
        private System.ComponentModel.IContainer components = null;

        public GradientFilledButton()
        {
            components = new System.ComponentModel.Container();
            this.Font = new Font(this.Font.Name, this.Font.Size, FontStyle.Bold);
        }

        private bool fontState = false;

        /// <summary>
        /// false 为button ,ture 为其他个性形式
        /// </summary>
        public bool FontState
        {
            get { return fontState; }
            set 
            { 
                fontState = value;
                this.Invalidate();
            }
        }

        // Controls the direction in which the button is filled.
        public GradientFill.FillDirection FillDirection
        {
            get
            {
                return fillDirectionValue;
            }
            set
            {
                fillDirectionValue = value;
                Invalidate();
            }
        }
        private GradientFill.FillDirection fillDirectionValue;

        // The start color for the GradientFill. This is the color
        // at the left or top of the control depeneding on the value
        // of the FillDirection property.
        public Color StartColor
        {
            get { return startColorValue; }
            set
            {
                startColorValue = value;
                Invalidate();
            }
        }
        private Color startColorValue = Color.Red;

        // The end color for the GradientFill. This is the color
        // at the right or bottom of the control depending on the value
        // of the FillDirection property
        public Color EndColor
        {
            get { return endColorValue; }
            set
            {
                endColorValue = value;
                Invalidate();
            }
        }
        private Color endColorValue = Color.Blue;

        // This is the offset from the left or top edge
        //  of the button to start the gradient fill.
        public int StartOffset
        {
            get { return startOffsetValue; }
            set
            {
                startOffsetValue = value;
                Invalidate();
            }
        }
        private int startOffsetValue;

        // This is the offset from the right or bottom edge
        //  of the button to end the gradient fill.
        public int EndOffset
        {
            get { return endOffsetValue; }
            set
            {
                endOffsetValue = value;
                Invalidate();
            }
        }
        private int endOffsetValue;

        // Used to double-buffer our drawing to avoid flicker
        // between painting the background, border, focus-rect
        // and the text of the control.
        private Bitmap DoubleBufferImage
        {
            get
            {
                if (bmDoubleBuffer == null)
                    bmDoubleBuffer = new Bitmap(
                        this.ClientSize.Width,
                        this.ClientSize.Height);
                return bmDoubleBuffer;
            }
            set
            {
                if (bmDoubleBuffer != null)
                    bmDoubleBuffer.Dispose();
                bmDoubleBuffer = value;
            }
        }
        private Bitmap bmDoubleBuffer;

        // Called when the control is resized. When that happens,
        // recreate the bitmap used for double-buffering.
        protected override void OnResize(EventArgs e)
        {
            DoubleBufferImage = new Bitmap(
                this.ClientSize.Width,
                this.ClientSize.Height);
            base.OnResize(e);
        }

        // Called when the control gets focus. Need to repaint
        // the control to ensure the focus rectangle is drawn correctly.
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.Invalidate();
        }
        //
        // Called when the control loses focus. Need to repaint
        // the control to ensure the focus rectangle is removed.
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.Capture)
            {
                Point coord = new Point(e.X, e.Y);
                if (this.ClientRectangle.Contains(coord) !=
                    this.ClientRectangle.Contains(lastCursorCoordinates))
                {
                    DrawButton(this.ClientRectangle.Contains(coord));
                }
                lastCursorCoordinates = coord;
            }
            base.OnMouseMove(e);
        }

        // The coordinates of the cursor the last time
        // there was a MouseUp or MouseDown message.
        Point lastCursorCoordinates;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Start capturing the mouse input
                this.Capture = true;
                // Get the focus because button is clicked.
                this.Focus();

                // draw the button
                DrawButton(true);
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.Capture = false;

            DrawButton(false);

            base.OnMouseUp(e);
        }

        bool bGotKeyDown = false;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            bGotKeyDown = true;
            switch (e.KeyCode)
            {
                case Keys.Space:
                case Keys.Enter:
                    DrawButton(true);
                    break;
                case Keys.Up:
                case Keys.Left:
                    this.Parent.SelectNextControl(this, false, false, true, true);
                    break;
                case Keys.Down:
                case Keys.Right:
                    this.Parent.SelectNextControl(this, true, false, true, true);
                    break;
                default:
                    bGotKeyDown = false;
                    base.OnKeyDown(e);
                    break;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                case Keys.Enter:
                    if (bGotKeyDown)
                    {
                        DrawButton(false);
                        OnClick(EventArgs.Empty);
                        bGotKeyDown = false;
                    }
                    break;
                default:
                    base.OnKeyUp(e);
                    break;
            }
        }

        // Override this method with no code to avoid flicker.
        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            DrawButton(e.Graphics, this.Capture &&
                (this.ClientRectangle.Contains(lastCursorCoordinates)));
        }

        //
        // Gets a Graphics object for the provided window handle
        //  and then calls DrawButton(Graphics, bool).
        //
        // If pressed is true, the button is drawn
        // in the depressed state.
        void DrawButton(bool pressed)
        {
            Graphics gr = this.CreateGraphics();
            DrawButton(gr, pressed);
            gr.Dispose();
        }

        // Draws the button on the specified Grapics
        // in the specified state.
        //
        // Parameters:
        //  gr - The Graphics object on which to draw the button.
        //  pressed - If true, the button is drawn in the depressed state.
        void DrawButton(Graphics gr, bool pressed)
        {
            // Get a Graphics object from the background image.
            Graphics gr2 = Graphics.FromImage(DoubleBufferImage);

            // Fill solid up until where the gradient fill starts.
            if (startOffsetValue > 0)
            {
                if (fillDirectionValue ==
                    GradientFill.FillDirection.LeftToRight)
                {
                    gr2.FillRectangle(
                        new SolidBrush(pressed ? EndColor : StartColor),
                        0, 0, startOffsetValue, Height);
                }
                else
                {
                    gr2.FillRectangle(
                        new SolidBrush(pressed ? EndColor : StartColor),
                        0, 0, Width, startOffsetValue);
                }
            }

            // Draw the gradient fill.
            Rectangle rc = this.ClientRectangle;
            if (fillDirectionValue == GradientFill.FillDirection.LeftToRight)
            {
                rc.X = startOffsetValue;
                rc.Width = rc.Width - startOffsetValue - endOffsetValue;
            }
            else
            {
                rc.Y = startOffsetValue;
                rc.Height = rc.Height - startOffsetValue - endOffsetValue;
            }
            GradientFill.Fill(
                gr2,
                rc,
                pressed ? endColorValue : startColorValue,
                pressed ? startColorValue : endColorValue,
                fillDirectionValue);

            // Fill solid from the end of the gradient fill
            // to the edge of the button.
            if (endOffsetValue > 0)
            {
                if (fillDirectionValue ==
                    GradientFill.FillDirection.LeftToRight)
                {
                    gr2.FillRectangle(
                        new SolidBrush(pressed ? StartColor : EndColor),
                        rc.X + rc.Width, 0, endOffsetValue, Height);
                }
                else
                {
                    gr2.FillRectangle(
                        new SolidBrush(pressed ? StartColor : EndColor),
                        0, rc.Y + rc.Height, Width, endOffsetValue);
                }
            }

            // Draw the text.
            if (FontState)
            {
                if (!string.IsNullOrEmpty(Text))
                {
                    
                    for (int txtLength = 0; txtLength < Text.Length; txtLength++)
                    {
                        var size = gr2.MeasureString(Text.Substring(txtLength, 1), Font);
                        using (var txtBrush = new SolidBrush(Color.Black))
                            gr2.DrawString(Text.Substring(txtLength, 1), Font, txtBrush,
                                (ClientSize.Width - size.Width) / 2, txtLength * size.Height);
                    }
                }
            }
            else
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                gr2.DrawString(this.Text, this.Font,
                    new SolidBrush(this.ForeColor),
                    this.ClientRectangle, sf);
            }

            // Draw the border.
            // Need to shrink the width and height by 1 otherwise
            // there will be no border on the right or bottom.
            rc = this.ClientRectangle;
            rc.Width--;
            rc.Height--;
            Pen pen = new Pen(SystemColors.WindowFrame);

            gr2.DrawRectangle(pen, rc);

            // Draw from the background image onto the screen.
            gr.DrawImage(DoubleBufferImage, 0, 0);
            gr2.Dispose();
        }

        //protected override void Dispose(bool disposing)
        //{
        //    this.bmDoubleBuffer = null;

        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool _IsDisposed = false;

        ~GradientFilledButton() 
        { 
            Dispose(false); 
        }
        public void Dispose() 
        { 
            Dispose(true);
            // Tell the garbage collector not to call the finalizer 
            // since all the cleanup will already be done.
            GC.SuppressFinalize(true); 
        } 
        protected override void Dispose(bool IsDisposing) 
        { 
            if (_IsDisposed) return; 

            if (IsDisposing) 
            { 
                // Free any managed resources in this section 
                bmDoubleBuffer = null;
              

            } 
            // Free any unmanaged resources in this section 

            _IsDisposed = true;
        } 
    }

}
