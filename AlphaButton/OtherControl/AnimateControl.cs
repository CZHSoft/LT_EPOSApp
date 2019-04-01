using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Drawing;


namespace AlphaControls
{
    public partial class AnimateControl : UserControl, IDisposable
    {
        private Bitmap bitmap;
        private int frameCount;
        private int frameWidth;
        private int frameHeight;
        private Graphics graphics;
        private int currentFrame = 0;
        private int loopCount = 0;
        private int loopCounter = 0;

        //private ImageAttributes attrib;

        private System.Windows.Forms.Timer fTimer;

        public Bitmap Bitmap
        {
            get { return bitmap; }
            set
            {
                bitmap = value;
                //attrib = new ImageAttributes(); 
            }

        }

        public AnimateControl()
        {
            InitializeComponent();

            //Cache the Graphics object
            graphics = this.CreateGraphics();
            //Instanciate the Timer
            fTimer = new System.Windows.Forms.Timer();
            //Hook up to the Timer's Tick event
            fTimer.Tick += new System.EventHandler(this.timer1_Tick);
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            if (loopCount == -1) //loop continuosly
            {
                this.DrawFrame();
            }
            else
            {
                if (loopCount == loopCounter) //stop the animation
                    fTimer.Enabled = false;
                else
                    this.DrawFrame();

            }
        }


        public void StartAnimation(int frWidth, int DelayInterval, int LoopCount)
        {

            frameWidth = frWidth;
            //How many times to loop
            loopCount = LoopCount;
            //Reset loop counter
            loopCounter = 0;
            //Calculate the frameCount
            frameCount = bitmap.Width / frameWidth;
            frameHeight = bitmap.Height;
            //Resize the control
            this.Size = new Size(frameWidth, frameHeight);
            //Assign delay interval to the timer
            fTimer.Interval = DelayInterval;
            //Start the timer
            fTimer.Enabled = true;
        }


        public void StopAnimation()
        {

            fTimer.Enabled = false;
        }

        private void DrawFrame()
        {
            if (currentFrame < frameCount - 1)
            {
                currentFrame++;
            }
            else
            {
                //increment the loopCounter
                loopCounter++;
                currentFrame = 0;
            }
            Draw(currentFrame);
        }


        private void Draw(int iframe)
        {
            //Calculate the left location of the drawing frame
            int XLocation = iframe * frameWidth;

            Rectangle rect = new Rectangle(XLocation, 0, frameWidth, frameHeight);

            //Color color = Color.Transparent;             //img.GetPixel(0, 0); 
            //attrib.SetColorKey(color, color); 
            //Draw image
            graphics.DrawImage(bitmap, 0, 0, rect, GraphicsUnit.Pixel);

            //graphics.DrawImage(bitmap, rect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, attrib);

        }


    }
}
