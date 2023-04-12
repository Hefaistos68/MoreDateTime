using Microsoft.VisualStudio.TestTools.UnitTesting;

using Nager.Date;

namespace MoreDateTime.Tests
{
	/// <summary>
	/// The assembly initialization for tests
	/// </summary>
	internal class AssemblyInitialize
	{
		/// <summary>
		/// Mies the test initialize.
		/// </summary>
		/// <param name="testContext">The test context.</param>
		[AssemblyInitialize]
		public static void MyTestInitialize(TestContext testContext)
		{
			DateSystem.LicenseKey = "Thank you for supporting open source projects";
		}
	}
}
