#tool "nuget:?package=NUnit.ConsoleRunner"

var target = Argument("target", "Default");

Task("Build")
  .Does(() =>
{
  MSBuild("Avengers.sln");
});

Task("Test")
  .Does(() =>
{
  NUnit3("./**/bin/**/*.Tests.dll");
});

Task("Default")
  .IsDependentOn("Build")
  .IsDependentOn("Test");

RunTarget(target);