using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TiemQuangCao.Tests
{
	[TestClass()]
	public class HelpersTests
	{
		[TestMethod()]
		public void GithubTagToVersionTest()
		{
			Assert.AreEqual(Helpers.GithubTagToVersion("v1.2.3.4"), new Version(1, 2, 3, 4));
			Assert.AreEqual(Helpers.GithubTagToVersion("v1.0.0"), new Version(1, 0, 0));
			Assert.AreEqual(Helpers.GithubTagToVersion("v0.0.1"), new Version(0, 0, 1));

			Assert.IsTrue(Helpers.GithubTagToVersion("") == null);
			Assert.IsTrue(Helpers.GithubTagToVersion("v") == null);

			Assert.IsTrue(Helpers.GithubTagToVersion("v1") == null);
			Assert.IsTrue(Helpers.GithubTagToVersion("v1.") == null);
			Assert.IsTrue(Helpers.GithubTagToVersion("v1.0") == null);
			Assert.IsTrue(Helpers.GithubTagToVersion("v1.0.") == null);

			Assert.IsTrue(Helpers.GithubTagToVersion("va.b.c") == null);
		}

		[TestMethod()]
		public void GetRuntimeIdentifierTest()
		{
			if (OperatingSystem.IsWindows())
			{
				if (RuntimeInformation.OSArchitecture == Architecture.X64)
				{
					Assert.AreEqual(Helpers.GetRuntimeIdentifier(), "win-x64");
				}
				else if (RuntimeInformation.OSArchitecture == Architecture.X86)
				{
					Assert.AreEqual(Helpers.GetRuntimeIdentifier(), "win-x86");
				}
			}
		}
	}
}
