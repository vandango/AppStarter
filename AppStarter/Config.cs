using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStarter
{
	internal class Config : IDisposable
	{
		private string ConfigFile { get; set; }
		public string IndexFile { get; set; }

		public bool RecursiveDirectoryScan { get; set; }

		public List<string> ApplicationIndex { get; set; }

		public List<string> ApplicationPaths { get; private set; }
		public List<string> ExcludePaths { get; private set; }
		public List<string> FileTypes { get; private set; }

		public Config()
		{
			string folder = Path.GetFullPath(
				Environment.GetFolderPath(
					Environment.SpecialFolder.CommonApplicationData
					) + "\\AppStarter\\"
				);

			if (!Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}

			this.ConfigFile = Path.GetFullPath(folder + "\\config.ascfg");
			this.IndexFile = Path.GetFullPath(folder + "\\index.asidx");

			this.Read();
		}

		public void Read()
		{
			this.ApplicationPaths = new List<string>();
			this.ExcludePaths = new List<string>();
			this.FileTypes = new List<string>();

			if (File.Exists(this.ConfigFile))
			{
				List<string> buffer = new List<string>();

				using (var reader = new StreamReader(this.ConfigFile))
				{
					while (!reader.EndOfStream)
					{
						buffer.Add(reader.ReadLine());
					}
				}

				foreach (string line in buffer)
				{
					// ApplicationPaths
					if (line.StartsWith("APP_PATH="))
					{
						this.ApplicationPaths.Add(line.Replace("APP_PATH=", ""));
					}

					// ExcludePaths
					if (line.StartsWith("EXCLUDE_PATH="))
					{
						this.ExcludePaths.Add(line.Replace("EXCLUDE_PATH=", ""));
					}

					// FileTypes
					if (line.StartsWith("FILETYPES="))
					{
						this.FileTypes.Add(line.Replace("FILETYPES=", ""));
					}

					// RecursiveDirectoryScan
					if (line.StartsWith("RECURSIVEDIRECTORYSCAN="))
					{
						this.RecursiveDirectoryScan = (line.Replace("RECURSIVEDIRECTORYSCAN=", "") == "1" ? true : false);
					}
				}
			}

			if (File.Exists(this.IndexFile))
			{
				this.ApplicationIndex = new List<string>(
					File.ReadAllLines(this.IndexFile)
					);
			}
		}

		public void Save()
		{
			using (var writer = new StreamWriter(this.ConfigFile))
			{
				// ApplicationPaths
				foreach (var item in this.ApplicationPaths)
				{
					writer.Write("APP_PATH=");
					writer.WriteLine(item);
				}

				// ExcludePaths
				foreach (var item in this.ExcludePaths)
				{
					writer.Write("EXCLUDE_PATH=");
					writer.WriteLine(item);
				}

				// FileTypes
				foreach (var item in this.FileTypes)
				{
					writer.Write("FILETYPES=");
					writer.WriteLine(item);
				}

				// RecursiveDirectoryScan
				writer.Write("RECURSIVEDIRECTORYSCAN=");
				writer.WriteLine(RecursiveDirectoryScan ? "1" : "0");
			}

			if (this.ApplicationIndex != null
			    && this.ApplicationIndex.Count > 0)
			{
				File.WriteAllLines(this.IndexFile, this.ApplicationIndex.ToArray());
			}
		}

		public Tuple<string, bool> IsRecursive(string path)
		{
			if (path.EndsWith(" -r") || path.EndsWith(" -R"))
			{
				return new Tuple<string, bool>(
					path.Replace(" -r", "").Replace(" -R", ""),
					true
					);
			}

			return new Tuple<string, bool>(
				path,
				false
				);
		}

		public void Dispose()
		{
			this.ApplicationIndex = null;
            this.ApplicationPaths = null;
			this.ExcludePaths = null;
			this.FileTypes = null;
		}
	}
}
