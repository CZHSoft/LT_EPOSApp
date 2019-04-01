//------------------------------------------------------------------------------
/// <copyright from='1997' to='2005' company='Microsoft Corporation'>
///    Copyright (c) Microsoft Corporation. All Rights Reserved.
///
///    This source code is intended only as a supplement to Microsoft
///    Development Tools and/or on-line documentation.  See these other
///    materials for detailed information regarding Microsoft code samples.
///
/// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

#if !NETCFDESIGNTIME	// RUNTIME only - CE only!
using System.Runtime.InteropServices;
using Microsoft.WindowsCE.Forms;
#else// These bits are not supported or needed at run time
	using System.ComponentModel.Design;
	using System.Windows.Forms.Design;

[assembly: System.CF.Design.RuntimeAssemblyAttribute("TooltipCF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
#endif

namespace Microsoft.Samples.CF.ToolTipCF 
{

#if NETCFDESIGNTIME	// These bits are not supported or needed at run time
	[ProvideProperty("TooltipText",typeof(Control))]
	[ToolboxItemFilter("System.CF.Windows.Forms", ToolboxItemFilterType.Require)]
	public class TooltipCF : System.ComponentModel.Component
		, System.ComponentModel.IExtenderProvider 
	{
#else// Used at runtime only
	public class TooltipCF :  System.ComponentModel.Component
	{

		class ControlInfo
		{
			public string ToolTipText;
			public IntPtr WindowsHandle;
			public Form parentForm;
			public Size TextExtent;
		};

		private ArrayList components;
		private Hashtable controlFromWindowHandle;
		private System.Windows.Forms.Control activeControl;
		private System.Threading.Timer stateTimer;
		private stateInfo CurrentState;
		private System.Windows.Forms.Label TipWindow;

		private enum stateInfo
		{
			none,
			InitialDelay,
			DisplayDelay
		};

		private TooltipMessageWindow hlpWnd;
		// Pocket PC specific helper DLL for mouse movement
		[DllImport("tooltiphelper.dll")]
		private static extern int TrackSingle(IntPtr MessageWindow,IntPtr ControlWindow, int Enabled);

		[DllImport("coredll.dll", EntryPoint="WindowFromPoint")]
		private extern static IntPtr WindowFromPointYX(int y, int x);
#endif
		/// <summary>
		///    Required designer variable.
		/// </summary>
		/// 
		private Hashtable TooltipTexts;
		private int autoPopDelay;
		private int initialDelay;
		private Color backColour;
		private Color foreColour;
		private Font font;

		public TooltipCF() 
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			TooltipTexts = new Hashtable();
#if !NETCFDESIGNTIME	// Used at runtime only
			controlFromWindowHandle = new Hashtable();

			// create a background timer thread
			CurrentState = new stateInfo();
			CurrentState = stateInfo.none;
			stateTimer = new System.Threading.Timer(new TimerCallback(ChangeTextEvent), CurrentState, Timeout.Infinite, Timeout.Infinite);
#endif
		}

		/// <summary>
		///    Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing) 
		{
			base.Dispose(disposing);
		}

		private void InitializeComponent() 
		{
			autoPopDelay = 5000;
			initialDelay = 500;
			backColour = Color.Yellow;
			foreColour = Color.Black;
			font = new Font("Nina", 10F, System.Drawing.FontStyle.Regular);

			// Setup a capture for the mouse move
#if !NETCFDESIGNTIME	// Pocket PC specific
			this.components = new ArrayList();
			hlpWnd = new TooltipMessageWindow();
			hlpWnd.MouseEventList+= new TooltipMessageWindow.MouseEvent(MouseEventHandler);
#endif
		}

#if NETCFDESIGNTIME	// These bits are not supported or needed at run time
   // Show this property in the property grid.
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("The background colour used to display text in the control.")]
#endif
		public Color BackColor
		{
			get 
			{
				return backColour;
			}
			set 
			{
				backColour = value;
			}
		}

#if NETCFDESIGNTIME	// These bits are not supported or needed at run time
   // Show this property in the property grid.
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("The foreground colour used to display text in the control.")]
#endif
		public Color ForeColor
		{
			get 
			{
				return foreColour;
			}
			set 
			{
				foreColour = value;
			}
		}

#if NETCFDESIGNTIME	// These bits are not supported or needed at run time
   // Show this property in the property grid.
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("The font used to display text in the control.")]
#endif
		public Font Font
		{
			get 
			{
				return font;
			}
			set 
			{
				font = value;
			}
		}

#if NETCFDESIGNTIME	// These bits are not supported or needed at run time
   // Show this property in the property grid.
        [System.ComponentModel.Category("Behavior")]
        [System.ComponentModel.Description("Determines the length of time the pointer must remain stationary within a tooltip region before the tool tip window appears.")]
#endif
		public int InitialDelay
		{
			get 
			{
				return initialDelay;
			}
			set 
			{
				initialDelay = value;
			}
		}

#if NETCFDESIGNTIME	// These bits are not supported or needed at run time
   // Show this property in the property grid.
        [System.ComponentModel.Category("Behavior")]
        [System.ComponentModel.Description("Determines the length of time the tool tip window remains visible if the pointer is stationary inside a tool tip region.")]
#endif
		public int AutoPopDelay
		{
			get 
			{
				return autoPopDelay;
			}
			set 
			{
				autoPopDelay = value;
			}
		}

		//
		// <doc>
		// <desc>
		//      This is the extended property for the TooltipText property.  Extended
		//      properties are actual methods because they take an additional parameter
		//      that is the object or control to provide the property for.
		// </desc>
		// </doc>
		//
		[DefaultValue("")]
		public string GetTooltipText(Control control) 
		{
			string text = string.Empty;
#if !NETCFDESIGNTIME	// Runtime specific
			try
			{
				text = (TooltipTexts[control] as ControlInfo).ToolTipText;
				if (text == null) 
				{
					text = string.Empty;
				}
			}
			catch (Exception )
			{
			}
#else
			text = (string)TooltipTexts[control];
			if (text == null) 
			{
				text = string.Empty;
			}
#endif
			return text;
		}

		
		public void SetTooltipText(Control control, string value) 
		{
			if (value == null) 
			{
				value = string.Empty;
			}

#if !NETCFDESIGNTIME	// Runtime specific
			if (value.Length == 0) 
			{
				try
				{
					ControlInfo ci = TooltipTexts[control] as ControlInfo;
					if (ci.WindowsHandle != IntPtr.Zero)
						TrackSingle(hlpWnd.Hwnd,ci.WindowsHandle,0);
					TooltipTexts.Remove(control);
					components.Remove(control);
					controlFromWindowHandle.Remove(ci.WindowsHandle);
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				control.MouseMove -= new MouseEventHandler(OnControlMouseMove);
				control.ParentChanged -= new EventHandler(OnParentChange);
			}
			else 
			{
				components.Add(control);
				ControlInfo ci = new ControlInfo();
				ci.ToolTipText = value;
				ci.WindowsHandle = IntPtr.Zero;
				TooltipTexts.Add(control,ci);

				control.MouseMove += new MouseEventHandler(OnControlMouseMove);
				control.ParentChanged += new EventHandler(OnParentChange);
			}
#else
			if (value.Length == 0) 
			{
				TooltipTexts.Remove(control);
			}
			else 
			{
				TooltipTexts[control] = value;
			}
#endif

		}

#if NETCFDESIGNTIME	// These bits are not supported or needed at run time
		///      This implements the IExtenderProvider.CanExtend method.  The
		///      help label provides an extender property, and the design time
		///      framework will call this method once for each component to determine
		///      if we are interested in providing our extended properties for the
		///      component.  We return true here if the object is a control and is
		///      not a HelpLabel (since it would be silly to add this property to
		///      ourselves).
		bool IExtenderProvider.CanExtend(object target) 
		{
			if (target is Control &&
				!(target is TooltipCF)) 
			{

				return true;
			}
			return false;
		}

#else
		///      This method is called by the timer when a time critical event has ocured, like the tool 
		///		text needs to be displayed
		public void ChangeTextEvent(object state)
		{
			stateTimer.Change(Timeout.Infinite, Timeout.Infinite);
			// check the event type
			switch(CurrentState)
			{
				case stateInfo.InitialDelay:
					// CHange the state object to reflect the timer
					CurrentState = stateInfo.DisplayDelay;
					// then reset the timer to the display delay
					stateTimer.Change(autoPopDelay, Timeout.Infinite);
					TipWindow.Visible = true;
					break;
				case stateInfo.DisplayDelay:
					// CHange the state object to reflect the timer
					CurrentState = stateInfo.none;
					TipWindow.Visible = false;
					// then reset the timer to the display delay
					stateTimer.Change(Timeout.Infinite, Timeout.Infinite);
					break;
			}
		}

		private void MouseEventHandler(TooltipMessageWindow.MouseEventType et, int x, int y, IntPtr WindowsHandle)
		{
			// Got a mouse move!
			// Lookup the windows handle and call the real workings
			try
			{
				RealMouseMove(et,x,y,controlFromWindowHandle[WindowsHandle] as Control);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void RealMouseMove(TooltipMessageWindow.MouseEventType et, int x, int y, Control c)
		{
			ControlInfo ci = TooltipTexts[c] as ControlInfo;
			switch (et)
			{
				case TooltipMessageWindow.MouseEventType.Up:
					activeControl = null;
					// Reset the timer when the mouse moves
					CurrentState = stateInfo.none;
					stateTimer.Change(Timeout.Infinite, Timeout.Infinite);
					break;
				case TooltipMessageWindow.MouseEventType.Down:
					// Start the timer thread ready to display the text
					activeControl = c;
					TipWindow.Text = ci.ToolTipText;
					TipWindow.Size = ci.TextExtent;

					// Reset the timer when the mouse moves
					CurrentState = stateInfo.InitialDelay;
					stateTimer.Change(initialDelay, Timeout.Infinite);
					break;
				case TooltipMessageWindow.MouseEventType.Move:
					// Check to see if the mouse position is inside the control space
					if (x<0 || x > c.Width ||y < 0 || y> c.Height)
					{
						// OUTSIDE OUR CONTROL - KILL all timers and reset state
						activeControl = null;

						// Reset the timer when the mouse moves
						CurrentState = stateInfo.none;
						stateTimer.Change(Timeout.Infinite, Timeout.Infinite);
					}
					else
					{
						// Re-Start the timer thread ready to display the text
						activeControl = c;
						TipWindow.Text = ci.ToolTipText;
						TipWindow.Size = ci.TextExtent;

						// Reset the timer when the mouse moves
						CurrentState = stateInfo.InitialDelay;
						stateTimer.Change(initialDelay, Timeout.Infinite);
					}
					break;
			}
			TipWindow.Visible = false;
			// Calculate tip location
			x += c.Location.X;	// Move it to the start of the field
			if (x+TipWindow.Width > Screen.PrimaryScreen.Bounds.Width)	// If the tip goes off the screen...
			{
				if (TipWindow.Width > Screen.PrimaryScreen.Bounds.Width) // if the tip is bigger than the screen...
				{
					x = -((TipWindow.Width - Screen.PrimaryScreen.Bounds.Width)/2); // put it in the middle so you will loose front and end chars
				}
				else
					x=Screen.PrimaryScreen.Bounds.Width - TipWindow.Width; // move it left to fit on the screen
			}

			y +=c.Location.Y;	// Move to the top of the field
			y-=TipWindow.Height;		// Move it above the mouse

			TipWindow.Location = new Point(x,y);
		}

		//      This is an event handler that responds to the MouseMove
		//      event for a specific control.  We attach this to each control we are providing tooltip
		//      text for so that the mouse movement can be detected.
		private void OnControlMouseMove(object sender, MouseEventArgs e) 
		{
			RealMouseMove(TooltipMessageWindow.MouseEventType.Move,e.X,e.Y,sender as Control);
		}

		private void FormLoad(object sender, System.EventArgs e)
		{
			Form parentForm = sender as Form;
			parentForm.Load -= new System.EventHandler(this.FormLoad);
			foreach (Control c in components)
			{
				ControlInfo ci = TooltipTexts[c] as ControlInfo;
				ci.parentForm = parentForm;
				// Now subclass the window
				int x = c.Top +  parentForm.Top;
				int y = c.Left +  parentForm.Left;
				IntPtr handle = WindowFromPointYX(y,x); 
				controlFromWindowHandle[handle] = c;
				ci.WindowsHandle = handle;
				Graphics g = Graphics.FromImage(new Bitmap(240,40));	// Dont know what else to do here!
				SizeF floatSize = g.MeasureString(ci.ToolTipText,this.Font);
				ci.TextExtent = new Size((int)floatSize.Width +5,(int)floatSize.Height);
				TrackSingle(hlpWnd.Hwnd,handle,1);
			}
			TipWindow = new Label();
			parentForm.Controls.Add(TipWindow);
			parentForm.Controls.SetChildIndex(TipWindow,0);
			TipWindow.BackColor = BackColor;
			TipWindow.ForeColor = ForeColor;
			TipWindow.Font = this.Font;
			TipWindow.Visible = false;
		}

		//      This is an event handler that responds to the Parent Changed
		//      event.  
		private void OnParentChange(object sender, EventArgs e) 
		{
			Control theControl = sender as Control;
			try
			{
				Form parentForm = theControl.Parent as Form;
				parentForm.Load -= new System.EventHandler(this.FormLoad);	// remove any previous version of this form load
				parentForm.Load += new System.EventHandler(this.FormLoad);
			}
			catch(Exception ex)
			{
				// Just ignore the exception - its not for us
				MessageBox.Show(ex.Message);
			}
		}

		// POCKET PC SPECIFIC
		public class TooltipMessageWindow: MessageWindow
		{
			const int WM_USER	= 0x0400;
			const int UM_SHOULDWATCH = WM_USER+100;
			const int UM_MOUSEMOVE	 = WM_USER+101;
			const int UM_MOUSEDOWN	 = WM_USER+102;
			const int UM_MOUSEUP	 = WM_USER+103;

			public enum MouseEventType
			{
				None,
				Move,
				Up,
				Down
			};

			public delegate void MouseEvent(MouseEventType et,int x, int y, IntPtr WindowsHandle);
			public event MouseEvent MouseEventList;

			// SDK functions
			[DllImport("coredll.dll")]
			private static extern IntPtr FindWindow(string MessageClass,string WindowsTitle);
			[DllImport("coredll.dll")]
			private static extern int SetWindowText(IntPtr MessageWindow,string Title);

			protected override void WndProc(ref Message msg)
			{
				MouseEventType et = MouseEventType.None;
				switch(msg.Msg)
				{
					case UM_MOUSEMOVE:
						et = MouseEventType.Move;
						break;
					case UM_MOUSEUP:
						et = MouseEventType.Up;
						break;
					case UM_MOUSEDOWN:
						et = MouseEventType.Down;
						break;
				}

				// Do something useful, like reset the counter for the specific control
				if (MouseEventList!=null && et != MouseEventType.None)
					MouseEventList(et,((int)msg.LParam)&0xFFFF,((int)msg.LParam>>16),msg.WParam);

				// call the base class WndProc for default message handling
				base.WndProc(ref msg);
			}

			public TooltipMessageWindow( )
			{
			}

		}
#endif

	}
}


