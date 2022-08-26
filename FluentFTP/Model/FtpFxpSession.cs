﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentFTP.Client.BaseClient;

namespace FluentFTP {

	/// <summary>
	/// Object that keeps track of an active FXP Connection between 2 FTP servers.
	/// </summary>
	public class FtpFxpSession : IDisposable {
		/// <summary>
		/// A connection to the FTP server where the file or folder is currently stored
		/// </summary>
		public BaseFtpClient SourceServer;

		/// <summary>
		/// A connection to the destination FTP server where you want to create the file or folder
		/// </summary>
		public BaseFtpClient TargetServer;

		/// <summary>
		/// A connection to the destination FTP server used to track progress while transfer is going on.
		/// </summary>
		public BaseFtpClient ProgressServer;

		/// <summary>
		/// Gets a value indicating if this object has already been disposed.
		/// </summary>
		public bool IsDisposed { get; private set; }

		/// <summary>
		/// Closes an FXP connection by disconnecting and disposing off the FTP clients that are
		/// cloned for this FXP connection. Manually created FTP clients are untouched.
		/// </summary>
		public void Dispose() {
			if (IsDisposed) {
				return;
			}
			
			if (SourceServer != null) {
				SourceServer.AutoDispose();
				SourceServer = null;
			}
			if (TargetServer != null) {
				TargetServer.AutoDispose();
				TargetServer = null;
			}
			if (ProgressServer != null) {
				ProgressServer.AutoDispose();
				ProgressServer = null;
			}

			IsDisposed = true;
			GC.SuppressFinalize(this);

		}
	}
}