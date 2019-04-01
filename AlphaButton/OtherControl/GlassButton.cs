
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace AlphaControls
{
    /// <summary>
    /// Implements a trnsparent button control.
    /// Note: on its surface, in the transparent areas, the button displays the pixels
    /// of a parent image.
    /// </summary>
    public partial class GlassButton : UserControl
    {
        #region Member variables and Properties
        //------------------------------------------------------------------------
        // Member variables and Properties
        //------------------------------------------------------------------------

        /// <summary>
        /// The image, whose corresponding pixels are displayed in the transparent areas.
        /// </summary>
        public Image ParentImage;

        /// <summary>
        /// The image that is displayed on the button surface.
        /// The parts of the image that should be transparent, must be set to the TransparentColor.
        /// </summary>
        public Image ForegroundImage;

        /// <summary>
        /// Specifies the color of the ForegroundImage pixels,that are to be made transparent.
        /// </summary>
        public Color TransparentColor;

        #endregion Member variables and Properties



        #region Lifetime members
        //------------------------------------------------------------------------
        // Lifetime members
        //------------------------------------------------------------------------

        /// <summary>
        /// Constructor.
        /// </summary>
        public GlassButton()
        {
            InitializeComponent();
        }

        #endregion Lifetime members



        #region Event handlers
        //------------------------------------------------------------------------
        // Event handlers
        //------------------------------------------------------------------------

        private void GlassButtonControl_Paint(object sender, PaintEventArgs e)
        {
            // Create a temporary bitmap, where we assemble the current button image.
            Bitmap MergedImage = new Bitmap(this.Width, this.Height);
            Graphics MergedImageGraphics = Graphics.FromImage(MergedImage);

            if (ParentImage != null)
            {
                // Fill the current merged image from the corresponding part of the parent.
                MergedImageGraphics.DrawImage(
                    ParentImage,
                    new Rectangle(0, 0, MergedImage.Width, MergedImage.Height),
                    new Rectangle(this.Left, this.Top, this.Width, this.Height),
                    GraphicsUnit.Pixel
                );
            }

            if (ForegroundImage != null)
            {
                // Draw the foreground image.

                ImageAttributes ia = new ImageAttributes();
                ia.SetColorKey(this.TransparentColor, this.TransparentColor);

                MergedImageGraphics.DrawImage(
                    this.ForegroundImage,
                    new Rectangle(0, 0, MergedImage.Width, MergedImage.Height),
                    0, 0, ForegroundImage.Width, ForegroundImage.Height,
                    GraphicsUnit.Pixel, ia);

                // Draw the merged button image.
                e.Graphics.DrawImage(MergedImage, 0, 0);

                // Clean up.
                ia.ClearColorKey();
                MergedImageGraphics.Dispose();
                MergedImage.Dispose();
            }
        }


        private void GlassButton_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        #endregion Event handlers
    }
}
