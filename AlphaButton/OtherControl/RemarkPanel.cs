using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace AlphaControls
{
    public partial class RemarkPanel : Control, IDisposable
    {
        private bool _IsDisposed = false;

        ~RemarkPanel()
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

            }
            // Free any unmanaged resources in this section 

            _IsDisposed = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (var backdrop = new Bitmap(Width, Height))
            {
                using (var gxOff = Graphics.FromImage(backdrop))
                {
                    var rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);

                    gxOff.FillRectangle(new SolidBrush(Color.White), rect);

                    using (var border = new Pen(Color.White))
                        gxOff.DrawRectangle(border, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);

                    //测试
                    int localY = 5;
                    if (!string.IsNullOrEmpty(this.Text))
                    {
                        var size = gxOff.MeasureString(this.Text, Font);

                        using (var textBrush = new SolidBrush(Color.Black))
                        {
                            if (this.Text.Length > 12)
                            {
                                for (int i = 0; i <= this.Text.Length / 12; i++)
                                {
                                    if (i == 0)
                                    {
                                        string str = this.Text.Substring(0, 12) + "\n";
                                        gxOff.DrawString(str, Font, textBrush, 5, localY);
                                        localY += (int)size.Height;
                                    }
                                    else if (i != this.Text.Length / 12)
                                    {
                                        string str = this.Text.Substring(12 * i , 12) + "\n";
                                        gxOff.DrawString(str, Font, textBrush, 5, localY);
                                        localY += (int)size.Height;
                                    }
                                    else
                                    {
                                        string str = this.Text.Substring(12 * i, this.Text.Length-12*i) + "\n";
                                        gxOff.DrawString(str, Font, textBrush, 5, localY);
                                    }
                                }
                            }

                        }
                    }
                }

                e.Graphics.DrawImage(backdrop, 0, 0);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }
    }
}