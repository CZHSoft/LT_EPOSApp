using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace AlphaControls
{
    public partial class PicShiftPanel : UserControl, IDisposable
    {
        private Image SourceImage;
        /// <summary>
        /// Full source image, read from SliderPuzzle.jpg (or null if file
        /// is not found).
        /// </summary>
        private String SourceImageFileNameValue = @"Content\SliderPuzzle.jpg";
        /// <summary>
        /// The file name of the source image to be diced up into the puzzle mosaic.
        /// </summary>
        public String SourceImageFileName
        {
            get { return SourceImageFileNameValue; }
            set
            {
                if (SourceImageFileNameValue != value)
                {
                    Image si = null;

                    // Load the new source image from the file system:读取图片地址
                    try
                    { si = new Bitmap(value); }
                    catch (ArgumentException)
                    { return; }
                    catch (System.IO.IOException)
                    { return; }

                    SourceImageFileNameValue = value;
                    SourceImage = si;
                }
            }
        }

        //private ImageList showImageList;

        //public ImageList ShowImageList
        //{
        //    get { return showImageList; }
        //    set 
        //    { 
        //        showImageList = value;
        //        this.Invalidate();
        //    }
        //}

        //private List<Image> foodImageList=new List<Image>();

        //public List<Image> FoodImageList
        //{
        //    get 
        //    {
        //        if (foodImageList != null)
        //        {
        //            return foodImageList;
        //        }
        //        return null;
        //    }
        //    set 
        //    { 
        //        foodImageList = value;
        //        this.Invalidate();
        //    }
        //}

        private List<FoodModel> foodModelList = new List<FoodModel>();

        public List<FoodModel> FoodModelList
        {
            get 
            {
                if (foodModelList != null)
                {
                    return foodModelList;
                }
                else
                {
                    return null;
                }
            }
            set { foodModelList = value; }
        }



        //索引
        private int signNO;

        public int SignNO
        {
            get { return signNO; }
            set 
            { 
                signNO = value;
                lbID.Text = "ID:" + foodModelList[signNO - 1].FoodID;
                lbName.Text = foodModelList[signNO - 1].FoodName;
                lbPrice.Text = "Price:"+foodModelList[signNO - 1].FoodPrice;
            }
        }

        public void SetbtnNextText(string text)
        {
            btnNext.Text = text;
        }

        public void SetbtnLastText(string text)
        {
            btnLast.Text = text;
        }

        public void SetbtnBuyText(string text)
        {
            btnBuy.Text = text;
        }

        public PicShiftPanel()
        {
            InitializeComponent();

            PictureShiftPanelEvent.OnPictureSelectChange += new PictureShiftPanelEvent.PicturnTurnDelegate(PictureTurnEvent_OnPictureSelectChange);
        }


        /// <summary>
        /// 事件处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="evt"></param>
        private void PictureTurnEvent_OnPictureSelectChange(object sender, EventArgs e)
        {
            PicTurnRight((int)sender);
        }

        /// <summary>
        /// Move tiles so that their positions on form match the board model
        /// </summary>
        private void SyncBoardView(PicShiftControl.Animate animate, Point newP)
        {
            // Clean up the screen before we start sliding tiles around:
            if (animate != PicShiftControl.Animate.None)
                Update();

            picShiftControl.MoveTo(newP, animate);
        }

        public void PicTurnLeft(int no)
        {

            if (foodModelList!=null && SignNO > 1)
            {
                //图片左移
                SyncBoardView(PicShiftControl.Animate.Slow, new Point(-picShiftControl.ImageValue.Width
                   , picShiftControl.Location.Y));
                //改变控件坐标
                picShiftControl.Location = new Point(panel.Width, picShiftControl.Location.Y);
                //
                SignNO = no;

                if (SignNO <= 1)
                {
                    btnLast.Enabled = false;
                }
                if (btnNext.Enabled == false)
                {
                    btnNext.Enabled = true;
                }
                
                //切换图片
                //picShiftControl.ImageValue = foodImageList[SignNO - 1];
                picShiftControl.ImageValue = foodModelList[SignNO - 1].FoodImage;

                lbCount.Text = SignNO.ToString() + "/" + foodModelList.Count.ToString();

                //图片右移
                SyncBoardView(PicShiftControl.Animate.Slow, new Point((panel.Width - picShiftControl.Width) / 2
                    , picShiftControl.Location.Y));
            }

        }

        public void PicTurnRight(int no)
        {
            if (foodModelList.Count > no)
            {
                //图片右移
                SyncBoardView(PicShiftControl.Animate.Slow, new Point(panel.Width
                    , picShiftControl.Location.Y));
                //改变控件坐标
                picShiftControl.Location = new Point(-picShiftControl.Width, picShiftControl.Location.Y);
                //
                SignNO=no+1;

                if (SignNO >= foodModelList.Count)
                {
                    btnNext.Enabled = false;
                }
                if (btnLast.Enabled == false)
                {
                    btnLast.Enabled = true;
                }
                if (SignNO <= 1)
                {
                    btnLast.Enabled = false;
                }
                if (btnNext.Enabled == false)
                {
                    btnNext.Enabled = true;
                }
                //切换图片
                //picShiftControl.ImageValue = foodImageList[SignNO - 1];
                picShiftControl.ImageValue = foodModelList[SignNO - 1].FoodImage;

                lbCount.Text = SignNO.ToString() + "/" + foodModelList.Count.ToString();

                //图片左移
                SyncBoardView(PicShiftControl.Animate.Slow, new Point((panel.Width - picShiftControl.Width) / 2
                    , picShiftControl.Location.Y));
            }

        }

        public void PicInit()
        {
            if (foodModelList != null)
            {
                if (foodModelList.Count > 0)
                {
                    SignNO = 1;
                    picShiftControl.ImageValue = foodModelList[0].FoodImage;

                    //picShiftControl.ImageValue = foodImageList[0];
                    //改变控件坐标
                    picShiftControl.Location = new Point(panel.Width, picShiftControl.Location.Y);
                    //图片右移
                    SyncBoardView(PicShiftControl.Animate.Slow, new Point((panel.Width - picShiftControl.Width) / 2
                        , picShiftControl.Location.Y));
                }
                btnLast.Enabled = false;
                lbCount.Text = SignNO.ToString() + "/" + foodModelList.Count.ToString();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (foodModelList != null && foodModelList.Count > SignNO)
            {
                //图片右移
                SyncBoardView(PicShiftControl.Animate.Slow, new Point(panel.Width
                    , picShiftControl.Location.Y));
                //改变控件坐标
                picShiftControl.Location = new Point(-picShiftControl.Width, picShiftControl.Location.Y);
                //
                SignNO++;

                if (SignNO >= foodModelList.Count)
                {
                    btnNext.Enabled = false;

                }
                if (btnLast.Enabled == false)
                {
                    btnLast.Enabled = true;
                }
                //切换图片
                //picShiftControl.ImageValue = foodImageList[SignNO - 1];
                picShiftControl.ImageValue = foodModelList[SignNO - 1].FoodImage;

                lbCount.Text = SignNO.ToString() + "/" + foodModelList.Count.ToString();

                //图片左移
                SyncBoardView(PicShiftControl.Animate.Slow, new Point((panel.Width - picShiftControl.Width) / 2
                    , picShiftControl.Location.Y));
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (foodModelList != null && SignNO > 1)
            {
            //图片左移
            SyncBoardView(PicShiftControl.Animate.Slow, new Point(-picShiftControl.ImageValue.Width
               , picShiftControl.Location.Y));
            //改变控件坐标
            picShiftControl.Location = new Point(panel.Width, picShiftControl.Location.Y);
            //
            SignNO--;

            if (SignNO <= 1)
            {
                btnLast.Enabled = false;
            }
            if (btnNext.Enabled == false)
            {
                btnNext.Enabled = true;
            }

            //切换图片
            //picShiftControl.ImageValue = foodImageList[SignNO - 1];
            picShiftControl.ImageValue = foodModelList[SignNO - 1].FoodImage;

            lbCount.Text = SignNO.ToString() + "/" + foodModelList.Count.ToString();

            //图片右移
            SyncBoardView(PicShiftControl.Animate.Slow, new Point((panel.Width - picShiftControl.Width) / 2
                , picShiftControl.Location.Y));
            }
        }

        private void picShiftControl_DoubleClick(object sender, EventArgs e)
        {
            sender = SignNO;

            PictureShiftPanelEvent.DoOnPicturnSelect(sender);
        }

        private void gestureRecognizer_Scroll(object sender, Microsoft.WindowsMobile.Gestures.GestureScrollEventArgs e)
        {
            if (e.ScrollDirection == Microsoft.WindowsMobile.Gestures.GestureScrollDirection.Left)
            {
                object sender1 = new object();
                EventArgs e1 = new EventArgs();
                btnNext_Click(sender1, e1);
            }
            else if (e.ScrollDirection == Microsoft.WindowsMobile.Gestures.GestureScrollDirection.Right)
            {
                object sender1 = new object();
                EventArgs e1 = new EventArgs();
                btnLast_Click(sender1, e1);
            }
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            string[] foodInfo = new string[3] { foodModelList[signNO-1].FoodID, 
                foodModelList[signNO-1].FoodName, foodModelList[signNO-1].FoodPrice };
            PictureShiftPanelEvent.DoOnButtonBuyClick(foodInfo);
        }
        
    }

    public class FoodModel
    {
        public FoodModel(string id,string name,string price,Image image)
        {
            this.FoodID = id;
            this.FoodName = name;
            this.FoodPrice = price;
            this.FoodImage = image;
        }

        private Image foodImage;

        public Image FoodImage
        {
            get { return foodImage; }
            set { foodImage = value; }
        }

        private string foodID;

        public string FoodID
        {
            get { return foodID; }
            set { foodID = value; }
        }

        private string foodName;

        public string FoodName
        {
            get { return foodName; }
            set { foodName = value; }
        }

        private string foodPrice;

        public string FoodPrice
        {
            get { return foodPrice; }
            set { foodPrice = value; }
        }
    }

}
