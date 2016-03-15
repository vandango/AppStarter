using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppStarter
{
	public partial class MainForm
	{
		#region MouseMove

		private bool _mouseIsDown = false;
		private Point _firstPoint;

		private void mainPanel_MouseDown(object sender, MouseEventArgs e)
		{
			this._firstPoint = e.Location;
			this._mouseIsDown = true;
		}

		private void mainPanel_MouseUp(object sender, MouseEventArgs e)
		{
			this._mouseIsDown = false;
		}

		private void mainPanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (this._mouseIsDown)
			{
				// Get the difference between the two points
				int xDiff = this._firstPoint.X - e.Location.X;
				int yDiff = this._firstPoint.Y - e.Location.Y;

				// Set the new point
				int x = this.Location.X - xDiff;
				int y = this.Location.Y - yDiff;
				this.Location = new Point(x, y);
			}
		}

		#endregion

		#region RegisterHotKey

		public sealed class Hotkey : NativeWindow
		{
			/*zum ID-Parameter: Systemweit kann eine TastenKombi nur einmal registriert werden.
			 * Also kann man problemlos die Kombi als ihre eigene ID verwenden*/

			[DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
			private static extern int RegisterHotKey(IntPtr Hwnd, int ID, int Modifiers, int Key);

			[DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
			private static extern int UnregisterHotKey(IntPtr Hwnd, int ID);

			public event EventHandler Pressed = delegate { };

			private int _Value = 0;
			private const int _WinKey = (int)Keys.Alt << 1; //die Window-Taste ist nicht Teil der Keys-Enumeration

			public Hotkey()
			{
				this.CreateHandle(new CreateParams());
			}

			/// <summary>Registrierung löschen durch Zuweisung von Keys.None</summary>
			public Keys Value
			{
				get { return (Keys)_Value; }
				set
				{
					if (_Value == (int)value) return;
					if (_Value != 0 && UnregisterHotKey(this.Handle, _Value) == 0)
						throw new Exception("k.A., was schief läuft");
					_Value = (int)value;
					if (_Value == 0) return;
					var ApiModifier = 0;
					if (Keys.Shift == (value & Keys.Shift)) ApiModifier += 4;
					if (Keys.Control == (value & Keys.Control)) ApiModifier += 2;
					if (Keys.Alt == (value & Keys.Alt)) ApiModifier += 1;
					if (_WinKey == (_Value & _WinKey)) ApiModifier += 8;
					//Für die API-Registrierung die Keys-Modifier-Komponente (oberhalb 0xffff)
					// der Keys-Enumeration wegmaskieren          
					if (RegisterHotKey(this.Handle, _Value, ApiModifier, _Value & 0xffff) == 0)
					{
						//throw new Exception("Ist der Key schon von einer anderen Anwendung registriert?");
					}
				}
			}

			protected override void WndProc(ref Message m)
			{
				const int WM_HOTKEY = 0x312;
				if (m.Msg == WM_HOTKEY) if (Pressed != null) Pressed(this, EventArgs.Empty);
				base.WndProc(ref m);
			}
		}

		#endregion
	}
}
