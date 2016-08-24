#tool "nuget:?package=NUnit.ConsoleRunner"

var target = Argument("target", "Default");

Task("Build")
  .Does(() =>
{
  NuGetRestore("Avengers.sln");
  MSBuild("Avengers.sln");
});

Task("Test")
  .Does(() =>
{
  NUnit3("./**/bin/**/*.Tests.dll", new NUnit3Settings {
    ResultFormat = "AppVeyor"
    });
});

Task("Default")
  .IsDependentOn("Build")
  .IsDependentOn("Test");

RunTarget(target);