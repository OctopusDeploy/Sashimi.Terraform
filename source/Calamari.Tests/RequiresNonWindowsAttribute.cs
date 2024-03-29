using System;
using System.Runtime.InteropServices;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OSPlatform = System.Runtime.InteropServices.OSPlatform;

//TODO: When migrating to Calamari Monorepo remove this
namespace Calamari.Terraform.Tests
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class NonWindowsTestAttribute : NUnitAttribute, IApplyToTest
	{
		readonly string message;

		public NonWindowsTestAttribute() : this("This test does not run on windows")
		{
		}

		public NonWindowsTestAttribute(string message)
		{
			this.message = message;
		}
        
		static bool IsRunningOnWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

		public void ApplyToTest(Test test)
		{
			if (test.RunState == RunState.NotRunnable || test.RunState == RunState.Ignored)
				return;

			if (IsRunningOnWindows)
			{
				test.RunState = RunState.Skipped;
				test.Properties.Add(PropertyNames.SkipReason, message);
			}
		}
	}
}