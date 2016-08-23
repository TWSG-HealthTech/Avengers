#tool "nuget:?package=NUnit.ConsoleRunner"

var target = Argument("target", "Default");

Task("Default")
  .Does(() =>
{
  MSBuild("Avengers.sln");
});

Task("Test")
  .IsDependentOn("Default")
  .Does(() =>
{
  NUnit3("./**/bin/**/*.Tests.dll");
});

RunTarget(target);