﻿using GACore.NLog;
using NLog;
using NUnit.Framework;
using System;
using System.IO;

namespace GACore.Test.NLog
{
	[TestFixture]
	public class TNLogManager
	{
		[Test]
		public void GetFileTargetLogger()
		{
			NLogManager manager = NLogManager.Instance;

			manager.LogDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

			Logger logger = manager.GetFileTargetLogger("TNLogManager_GetFileTargetLogger");

			logger.WriteValidateLoglevels();
			Assert.IsTrue(File.Exists(logger.GetFilePath()));
		}
	}
}