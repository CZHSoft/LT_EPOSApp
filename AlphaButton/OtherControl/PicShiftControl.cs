using System;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace AlphaControls
{
    public class PicShiftControl : Control, IDisposable
    {
        //ÒÆ¶¯×´Ì¬±ê¼Ç
        public enum Animate
        {
            None = 0,
            Slow = 1,
            Fast = 2
        }

        //±ß¿ò»­±Ê
        static private System.Drawing.Pen s_ShadowPen =
            new System.Drawing.Pen(Color.DarkGray);

        private Image imageValue;

        public Image ImageValue
        {
            get { return imageValue; }
            set 
            { 
                imageValue = value;

                if (Parent != null)
                Invalidate();
            }
        }


        // Used by OnPaint to source's area in the image:

        private Rectangle ImageSrcRectValue;


        public PicShiftControl()
        {
            imageValue = null;
            ImageSrcRectValue = new Rectangle(0, 0, 0, 0);
        }

        private bool _IsDisposed = false;

        ~PicShiftControl() 
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
                ImageValue = null;
                s_ShadowPen.Dispose();
            } 
            // Free any unmanaged resources in this section 

            _IsDisposed = true;
        } 


        ///// <summary>
        ///// The point of origin of this tile in the full untiled image
        ///// </summary>
        //public Point TileOrigin
        //{
        //    get { return ImageSrcRectValue.Location; }
        //    set
        //    {
        //        ImageSrcRectValue.Location = value;

        //        if ( Parent != null )
        //            Invalidate();
        //    }
        //}

        /// <summary>
        /// Animate and move this tile from its current screen location
	    /// to the newLocation
        /// </summary>
        /// <param name="newLocation">Parent coordinates of destination</param>
        /// <param name="speed">Speed of animation (enum)</param>

        public void MoveTo( System.Drawing.Point newLocation, Animate speed )
        {
            BringToFront();
	    
            if ( speed != Animate.None )
            {
                Point startLoc = Location;
                Point newLoc   = Location;

                int dX = newLocation.X - startLoc.X;
                int dY = newLocation.Y - startLoc.Y;

                for ( int step = 1; step < 10; ++step )
                {
                    int factor = step * step;
                    Thread.Sleep(speed == Animate.Fast ? 1 : 30);
                    //Thread.Sleep(5);
                    newLoc.X = startLoc.X + (dX * factor)/100;
                    newLoc.Y = startLoc.Y + (dY * factor)/100;
                    Location = newLoc;
                    
                    Parent.Update();
                }
            }

            Location = newLocation;
            
            if ( speed != PicShiftControl.Animate.None )
                Parent.Update();
        }

        protected override void OnPaint( PaintEventArgs e )
        {
            base.OnPaint(e);

            if ( ImageValue != null )
	        {
                e.Graphics.DrawImage( ImageValue,
				      ClientRectangle,
				      ImageSrcRectValue,
				      GraphicsUnit.Pixel );
	        }

            e.Graphics.DrawRectangle(s_ShadowPen, ClientRectangle);

        }

        protected override void OnPaintBackground( PaintEventArgs e )
        {
            if ( ImageValue == null )
                base.OnPaintBackground(e);
        }
        
        protected override void OnResize( EventArgs e )
        {
            base.OnResize(e);
            ImageSrcRectValue.Size = ClientRectangle.Size;
        }
   }
}
