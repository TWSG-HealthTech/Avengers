#tool "nuget:?package=NUnit.ConsoleRunner"
#tool "nuget:?package=Machine.Specifications.Runner.Console"

using System.Diagnostics;
using IO = System.IO;
using System.Linq;

var target = Argument("target", "Default");
var buildConfiguration = "Release";

Task("Build")
  .Does(() =>
{
  NuGetRestore("Avengers.sln");
  MSBuild("Avengers.sln", new MSBuildSettings {
    Configuration = buildConfiguration
  });
});

Task("Test")
  .Does(() =>
{
  var binFolders = IO.Directory.GetDirectories(IO.Directory.GetCurrentDirectory(), "*.Tests").SelectMany(d => IO.Directory.GetDirectories(d, "bin"));
  var testDlls = binFolders.SelectMany(f => IO.Directory.GetFiles(IO.Path.Combine(f, buildConfiguration), "*.Tests.dll", SearchOption.AllDirectories));

  var startInfo = new ProcessStartInfo(IO.Path.Combine(IO.Directory.GetCurrentDirectory(), "tools/Machine.Specifications.Runner.Console/tools/mspec-clr4.exe"))
  {
    UseShellExecute = false,
    RedirectStandardOutput = true,
    CreateNoWindow = true,
    Arguments = "--xml=\"./TestResult.xml\" " + string.Join(" ", testDlls)
  };

  var mspecProcess = Process.Start(startInfo);
  while (!mspecProcess.StandardOutput.EndOfStream) {
      string line = mspecProcess.StandardOutput.ReadLine();
      Console.WriteLine(line);
  }
});

Task("Default")
  .IsDependentOn("Build")
  .IsDependentOn("Test");

RunTarget(target);