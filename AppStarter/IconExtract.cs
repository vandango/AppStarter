using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace AppStarter
{
	public class IconExtract
	{
		public static Icon GetIcon(string filePath, bool small)
		{
			return ExtractIcon.GetIcon(filePath, small);
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SHFILEINFO
		{
			public IntPtr hIcon;
			public IntPtr iIcon;
			public uint dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] public string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)] public string szTypeName;
		}

		private static class ExtractIcon
		{
			/// <summary>
			/// Methode zum extrahieren von einem Icon aus einer Datei.
			/// </summary>
			/// <param name="FilePath">Hier übergeben Sie den Pfad der Datei von dem das Icon extrahiert werden soll.</param>
			/// <param name="Small">Bei übergabe von true wird ein kleines und bei false ein großes Icon zurück gegeben.</param>
			public static Icon GetIcon(string FilePath, bool Small)
			{
				try
				{
					IntPtr hImgSmall;
					IntPtr hImgLarge;
					IconExtract.SHFILEINFO shinfo = new IconExtract.SHFILEINFO();
					if (Small)
					{
						hImgSmall = Win32.SHGetFileInfo(FilePath, 0,
							ref shinfo, (uint) Marshal.SizeOf(shinfo),
							Win32.SHGFI_ICON | Win32.SHGFI_SMALLICON);
					}
					else
					{
						hImgLarge = Win32.SHGetFileInfo(FilePath, 0,
							ref shinfo, (uint) Marshal.SizeOf(shinfo),
							Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON);
					}
					return (System.Drawing.Icon.FromHandle(shinfo.hIcon));
				}
				catch
				{
				}

				return null;
			}
		}

		/// <summary>
		/// DLL Definition für IconExtract.
		/// </summary>
		private class Win32
		{
			public const uint SHGFI_ICON = 0x100;
			public const uint SHGFI_LARGEICON = 0x0;
			public const uint SHGFI_SMALLICON = 0x1;

			[DllImport("shell32.dll")]
			public static extern IntPtr SHGetFileInfo(string pszPath,
				uint dwFileAttributes,
				ref IconExtract.SHFILEINFO psfi,
				uint cbSizeFileInfo,
				uint uFlags);
		}

		//// IntPtr ist ein Handle des Icons
		//// GetIcons()[0] gibt das große Symbol zurück und GetIcons()[1] das kleine
		//// Z.B. so: Icon tmp = Icon.FromHandle(GetIcons("txt", "C:\\Test\\", TestItem)[0]);
		//// Sonst bei fragen bitte melden. Ich hab das jetzt nämlich nicht alles umgeschrieben...

		//private IntPtr[] GetIcons(string endung, string pfad, ListViewItem item)
		//{
		//	RegistryKey key;

		//	IntPtr[] Icos = new IntPtr[2];

		//	IntPtr[] Symbolgross = new IntPtr[1];
		//	IntPtr[] Symbolklein = new IntPtr[1];

		//	/* Wenn es keinen Eintrag in der Registry für die Endung gibt, kommt catch ins Spiel (z.b.: bei .EXE und .LNK Dateien)*/
		//	try
		//	{
		//		key = Registry.ClassesRoot.OpenSubKey(endung, true);

		//		string str = key.GetValue(null).ToString();
		//		key = Registry.ClassesRoot.OpenSubKey(str + "\\DefaultIcon", true);
		//		str = key.GetValue(null).ToString();
		//		string temp = str;
		//		int index2 = 1;

		//		while (true)
		//		{
		//			temp = temp.Remove(0, temp.Length - index2);
		//			temp = temp.Remove(1, index2 - 1);
		//			if (temp == ",")
		//			{
		//				break;
		//				// Ende if
		//			}
		//			temp = str;
		//			index2++;
		//			// Ende while
		//		}
		//		temp = str.Remove(0, str.Length - index2 + 1);

		//		ExtractIconEx(str.Remove(str.Length - index2, index2), Int32.Parse(str.Remove(0, str.Length + 1 - index2)), Symbolgross, Symbolklein, 1);
		//		Icos[0] = Symbolgross[0];
		//		Icos[1] = Symbolklein[0];
		//		// Ende try
		//	}
		//	catch (Exception)
		//	{
		//		if ((endung.ToUpper() == ".EXE") || (endung.ToUpper() == ".LNK"))
		//		{
		//			if (ExtractIconEx(pfad + "\\" + item.Text + endung, 0, Symbolgross, Symbolklein, 1) == 0)
		//			{
		//				ExtractIconEx(Environment.SystemDirectory + "\\System32\\Shell32.dll", 0, Symbolgross, Symbolklein, 1);
		//				// Ende if
		//			}
		//			// Ende if
		//		}
		//		else
		//		{
		//			/* Wenn es sich um eine unbekannte Endung handelt, dann wähle ein Standarticon*/
		//			ExtractIconEx(Environment.SystemDirectory + "\\System32\\Shell32.dll", 0, Symbolgross, Symbolklein, 1);
		//			// Ende else
		//		}
		//		Icos[0] = Symbolgross[0];
		//		Icos[1] = Symbolklein[0];
		//		// Ende catch
		//	}
		//	return Icos;
		//	// Ende GetIcons()
		//}
	}
}
