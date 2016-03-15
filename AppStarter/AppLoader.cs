using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppStarter
{
	/// <summary>
	///     Class AppLoader
	/// </summary>
	public static class AppLoader
	{
		/// <summary>
		///     Load a application in a new thread.
		/// </summary>
		/// <param name="name">The filename or the name (if it is in the PATH environment variable) of the application to start.</param>
		public static void LoadAppInThread(string name)
		{
			var loader = new AppProcessLoader(name);
			var lcwThread = new Thread(loader.Start);
			lcwThread.Start();
		}

		/// <summary>
		///     Load a application in a new thread.
		/// </summary>
		/// <param name="param">The parameters.</param>
		/// <param name="name">The filename or the name (if it is in the PATH environment variable) of the application to start.</param>
		public static void LoadAppInThread(string name, string param)
		{
			var loader = new AppProcessLoader(name, param);
			var lcwThread = new Thread(loader.Start);
			lcwThread.Start();
		}

		/// <summary>
		///     Load a application in the same thread.
		/// </summary>
		/// <param name="name">The filename or the name (if it is in the PATH environment variable) of the application to start.</param>
		public static void LoadApp(string name)
		{
			var loader = new AppProcessLoader(name);
			loader.Start();
		}

		/// <summary>
		///     Load a application in the same thread.
		/// </summary>
		/// <param name="param">The parameters.</param>
		/// <param name="name">The filename or the name (if it is in the PATH environment variable) of the application to start.</param>
		public static void LoadApp(string name, string param)
		{
			var loader = new AppProcessLoader(name, param);
			loader.Start();
		}

		/// <summary>
		///     Load a application in the same thread.
		/// </summary>
		/// <param name="name">The filename or the name (if it is in the PATH environment variable) of the application to start.</param>
		/// <param name="waitForExit">A value that indicates if the opener should wait as long as the app is not finished.</param>
		public static void LoadApp(string name, bool waitForExit)
		{
			var loader = new AppProcessLoader(name);
			loader.Start(waitForExit);
		}

		/// <summary>
		///     Load a application in the same thread.
		/// </summary>
		/// <param name="name">The filename or the name (if it is in the PATH environment variable) of the application to start.</param>
		/// <param name="waitForExit">A value that indicates if the opener should wait as long as the app is not finished.</param>
		/// <param name="timeInMilliseconds">Time to wait in milliseconds, write a value smaller as 0 to wait an unlimited time.</param>
		public static void LoadApp(string name, bool waitForExit, int timeInMilliseconds)
		{
			var loader = new AppProcessLoader(name);
			loader.Start(waitForExit, timeInMilliseconds);
		}

		/// <summary>
		///     Load a application in the same thread.
		/// </summary>
		/// <param name="name">The filename or the name (if it is in the PATH environment variable) of the application to start.</param>
		/// <param name="param">The parameters.</param>
		/// <param name="waitForExit">A value that indicates if the opener should wait as long as the app is not finished.</param>
		public static void LoadApp(string name, string param, bool waitForExit)
		{
			var loader = new AppProcessLoader(name, param);
			loader.Start(waitForExit);
		}

		/// <summary>
		///     Load a application in the same thread.
		/// </summary>
		/// <param name="name">The filename or the name (if it is in the PATH environment variable) of the application to start.</param>
		/// <param name="param">The parameters.</param>
		/// <param name="waitForExit">A value that indicates if the opener should wait as long as the app is not finished.</param>
		/// <param name="timeInMilliseconds">Time to wait in milliseconds, write a value smaller as 0 to wait an unlimited time.</param>
		public static void LoadApp(string name, string param, bool waitForExit, int timeInMilliseconds)
		{
			var loader = new AppProcessLoader(name);
			loader.Start(param, waitForExit, timeInMilliseconds);
		}

		/// <summary>
		///     Private class AppProcessLoader
		/// </summary>
		private class AppProcessLoader
		{
			private readonly Process _app;

			/// <summary>
			///     Default Ctor
			/// </summary>
			/// <param name="name">The name of the application</param>
			public AppProcessLoader(string name)
			{
				_app = new Process { StartInfo = { FileName = name } };
			}

			/// <summary>
			///     Default Ctor
			/// </summary>
			/// <param name="name">The name of the application</param>
			/// <param name="param">The param.</param>
			public AppProcessLoader(string name, string param)
			{
				_app = new Process { StartInfo = { FileName = name, Arguments = param } };
			}

			/// <summary>
			///     Start now
			/// </summary>
			public void Start()
			{
				try
				{
					_app.Start();
				}
				catch
				{
					// ignored
				}
			}

			/// <summary>
			///     Start now
			/// </summary>
			/// <param name="waitForExit">A value that indicates if the opener should wait as long as the app is not finished.</param>
			public void Start(bool waitForExit)
			{
				_app.Start();

				if (waitForExit)
				{
					_app.WaitForExit();
				}
			}

			/// <summary>
			///     Start now
			/// </summary>
			/// <param name="waitForExit">A value that indicates if the opener should wait as long as the app is not finished.</param>
			/// <param name="timeInMilliseconds">Time to wait in milliseconds, write a value smaller as 0 to wait an unlimited time.</param>
			public void Start(bool waitForExit, int timeInMilliseconds)
			{
				_app.Start();

				if (waitForExit)
				{
					if (timeInMilliseconds > 0)
					{
						_app.WaitForExit();
					}
					else
					{
						_app.WaitForExit(timeInMilliseconds);
					}
				}
			}

			/// <summary>
			///     Start now
			/// </summary>
			/// <param name="param">The parameters.</param>
			/// <param name="waitForExit">A value that indicates if the opener should wait as long as the app is not finished.</param>
			/// <param name="timeInMilliseconds">Time to wait in milliseconds, write a value smaller as 0 to wait an unlimited time.</param>
			public void Start(string param, bool waitForExit, int timeInMilliseconds)
			{
				_app.StartInfo.Arguments = param;
				_app.Start();

				if (waitForExit)
				{
					if (timeInMilliseconds > 0)
					{
						_app.WaitForExit();
					}
					else
					{
						_app.WaitForExit(timeInMilliseconds);
					}
				}
			}
		}
	}
}
