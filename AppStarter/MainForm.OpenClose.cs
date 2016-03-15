using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStarter
{
	public partial class MainForm
	{
		private bool isOpen = false;

		private int closedHeight = 49;
		private int openHeight = 150;

		private Image openImage = global::AppStarter.Properties.Resources.ic_keyboard_arrow_up_32px;
		private Image closedImage = global::AppStarter.Properties.Resources.ic_keyboard_arrow_down_32px;

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			if (isOpen)
			{
				Height = closedHeight;
				pictureBox1.Image = closedImage;

				isOpen = false;
			}
			else
			{
				pictureBox1.Image = openImage;
				Height = openHeight;

				isOpen = true;
			}
		}
	}
}
