using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace AlphaControls
{
    public partial class SmoothListboxHorizontal : UserControl
    {
        //三个鼠标事件
        private MouseEventHandler mouseDown;
        private MouseEventHandler mouseUp;
        private MouseEventHandler mouseMove;

        private bool mouseIsDown = false;//鼠标被按下了吗
        private Point mouseDownPoint = Point.Empty;//鼠标按下位置
        private bool renderLockFlag = false;//鼠标在拖动吗

        //private int oldX=0;
        private Point previousPoint = Point.Empty;//旧移动位置
        private int draggedDistance=0;//移动距离

        private float dragDistanceFactor = 50.0f;//距离因子
        private float deacceFactor = 0.9f;//减速因子
        private float velocity = 0.0f;//速度
        private float maxVelocity = 500.0f;//最大速度
        private float snapBackFactor = 0.2f;//返回的速度因子


        private int itemWidth;//每项的宽度
        private int itemHeight;//每项的高度
        private bool itemWidthFlag=false;//在AddItem时记录一次
        public string photoName;//图片名
    
        /// <summary>
        /// 当松开手时定时执行此函数
        /// </summary>
        private void DoAutomaticMotion()
        {
            if (!mouseIsDown)
            {
                velocity *= deacceFactor;
                float elapsedTime = timer1.Interval / 1000.0f;
                float deltaDistance = elapsedTime * velocity;  //距离=时间*速度
                if (Math.Abs(deltaDistance) >= 1.0f)  //拖动距离大于等于1
                {
                    ScrollItems((int)deltaDistance);
                }
                else　　　　　　　　　　　　　　　　　//拖动距离小于1
                {
                    int left = itemsPanel.Left % itemWidth;//itemsPanel.Left随着左移越来越小,变为负数
                    int width = -itemWidth / 2;
                    if (left != 0 || itemsPanel.Left > 0|| itemsPanel.Left + itemsPanel.Width < ClientSize.Width)
                    {
                        if (itemsPanel.Left > 0)//拖动左端空了
                        {
                            ScrollItems(-Math.Max(1, (int)(snapBackFactor * (float)itemsPanel.Left)));
                            //lblInfo.Text = "a" + left + "|" + itemsPanel.Left;
                        }
                        else if (itemsPanel.Width > ClientSize.Width)
                        {
                            int bottomPosition = itemsPanel.Left + itemsPanel.Width;
                            if (bottomPosition < ClientSize.Width)//拖动的右端空了
                            {
                                ScrollItems(Math.Max(1, (int)(snapBackFactor * (float)(ClientSize.Width - bottomPosition))));
                            //    lblInfo.Text = "c" +left+ "|" + itemsPanel.Left;
                            }
                            else if (left > width) //需要向右移
                            {
                                ScrollItems(Math.Max(1, (int)(snapBackFactor * (-left))));
                            //    lblInfo.Text = "b" + left + "|" + itemsPanel.Left;
                            }
                            else if (bottomPosition > ClientSize.Width)　//需要向左移
                            {
                                ScrollItems(-Math.Max(1, (int)(snapBackFactor * (itemWidth + left))));
                             //   lblInfo.Text = "d" + left + "|" + itemsPanel.Left;
                            }
                        }
                        else//如果列表的长度比窗口还小
                        {
                            if (itemsPanel.Left != 0)//不加会跟"拖动左端空了"冲突
                            {
                                ScrollItems(Math.Max(1, -((int)(snapBackFactor * (float)itemsPanel.Left))));
                            //    lblInfo.Text = "e" + left + "|" + itemsPanel.Left;
                            }
                        }
                    }
                }
            }
        }

        public SmoothListboxHorizontal()
        {
            InitializeComponent();
            itemsPanel.Left = 0;//似乎默认为-13
            itemWidth = itemsPanel.Width;//在PDA上用时,似值没有跟着增大,所以在AddItem时再记录一次
            itemHeight = itemsPanel.Height;
            mouseDown = new MouseEventHandler(MouseDownEvent);
            mouseUp = new MouseEventHandler(MouseUpEvent);
            mouseMove = new MouseEventHandler(MouseMoveEvent);
            Utils.SetHandlers(itemsPanel,mouseDown,mouseUp,mouseMove);
        }

        /// <summary>
        /// 根据点击事件找到所点击的控件
        /// </summary>
        private Control GetListItemFromEvent(Control sender)
        {
            if (sender == this || sender == itemsPanel)
                return null;
            else
            {
                while (sender.Parent != itemsPanel)
                    sender = sender.Parent;
                return sender;
            }
        }

        //鼠标按下时
        private void MouseDownEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseIsDown = true;
                mouseDownPoint = Utils.GetAbsolute(new Point(e.X, e.Y), sender as Control, this);
                previousPoint = mouseDownPoint;
            }
        }

        //松开鼠标时
        private void MouseUpEvent(object sender, MouseEventArgs e)
        {
            if (renderLockFlag) //如果是被拖动时
            {
                //-500~500之间
                velocity = Math.Min(Math.Max(dragDistanceFactor*draggedDistance,-maxVelocity), maxVelocity);
                draggedDistance = 0;
            }
            if (e.Button == MouseButtons.Left)
            {
                //如果只点击不拖动,就在此处理,避免拖动即触发点击
                if (Utils.GetAbsolute(new Point(e.X, e.Y), sender as Control, this).Equals(mouseDownPoint))
                {
                    Control item = GetListItemFromEvent(sender as Control);
                    if (item != null)
                    {
                        //称除选中效果
                        foreach (Control listItem in itemsPanel.Controls)
                        {
                            if (listItem != item)
                                if (listItem is IExtendedListItem)
                                    (listItem as IExtendedListItem).SelectedChanged(false);
                        }
                        if (item is IExtendedListItem)
                        {
                            (item as IExtendedListItem).SelectedChanged(true);//选中效果
                            this.photoName = (item as IExtendedListItem).GetPhoto();
                        }
                    }
                    //让主窗体执行动作
                    if(this.Parent is Interfa)
                        (this.Parent as Interfa).ClickOn();
                }
            }
            mouseIsDown = false;
        }

        //鼠标移动时
        private void MouseMoveEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!renderLockFlag)    //限制拖动次数
                {
                    //在一点点移动时,发现e.x的值不会变
                    renderLockFlag = true;
                    Point absolutePoint = Utils.GetAbsolute(new Point(e.X, e.Y), sender as Control, this);
                    int delta = absolutePoint.X - previousPoint.X;
                    draggedDistance = delta;
                    ScrollItems(delta);
                    previousPoint = absolutePoint;
                }
            }
        }

        /// <summary>
        /// 控件移动,正数向右,负数向左
        /// </summary>
        private void ScrollItems(int offset)
        {
            if (0 == offset)
            {
                return;
            }
            SuspendLayout();
            itemsPanel.Left += offset;
            ResumeLayout(true);
        }

        /// <summary>
        /// 定时处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            renderLockFlag = false;
            DoAutomaticMotion();
        }


        /// <summary>
        /// 布局重置
        /// </summary>
        public void LayoutItems()
        {
            int left = 0;
            for (int i = 0; i < itemsPanel.Controls.Count; i++)
            {
                if (i % 2 == 0)
                {
                    itemsPanel.Controls[i].Location = new Point(left, 0);
                    //itemsPanel.Controls[i].Height = itemsPanel.ClientSize.Height;
                    left += itemsPanel.Controls[i].Width;
                }
                else
                {
                    itemsPanel.Controls[i].Location = new Point(left - itemWidth, itemHeight+5);
                }
            }
            itemsPanel.Width = left;
            itemsPanel.Height = itemHeight * 2+5;

            //int top = 0;
            //foreach (Control itemControl in itemsPanel.Controls)
            //{
            //    itemControl.Location = new Point(0, top);
            //    itemControl.Width = itemsPanel.ClientSize.Width;//无作用
            //    top += itemControl.Height;
            //}

            //itemsPanel.Height = top;
        }

        /// <summary>
        /// 增加新项
        /// </summary>
        /// <param name="control"></param>
        public void AddItem(Control control)
        {
            if (!itemWidthFlag)
            {
                itemWidth = itemsPanel.Width;
                itemHeight = itemsPanel.Height;
                itemWidthFlag = true;
            }
            if (!itemsPanel.Controls.Contains(control))
            {
                itemsPanel.Controls.Add(control);
                LayoutItems();
                if (control is IExtendedListItem)
                    ((IExtendedListItem)control).PositionChanged(itemsPanel.Controls.Count);
                Utils.SetHandlers(this, mouseDown, mouseUp, mouseMove);
            }
            else
            {
                throw new 
                    ArgumentException("Each item in SmoothListbox must be a unique Control", "control");
            }
        }


        /// <summary>
        /// 移除旧项
        /// </summary>
        /// <param name="control"></param>
        public void RemoveItem(Control control)
        {
            itemsPanel.Controls.Remove(control);
            Utils.RemoveHandlers(control, mouseDown, mouseUp, mouseMove);

            for (int i = 0; i < itemsPanel.Controls.Count; ++i)
            {
                Control itemControl = itemsPanel.Controls[i];
                if (itemControl is IExtendedListItem)
                {
                    (itemControl as IExtendedListItem).PositionChanged(i);
                }
            }
            LayoutItems();
        }

        //重置
        public void Reset()
        {
            velocity = 0;
            itemsPanel.Left = 0;
            LayoutItems();
        }
    }
}