param($installPath, $toolsPath, $package, $project)

# Link the files from the Nuget package folder
$project.ProjectItems.AddFromFile($installPath + "\freerdp.dll");
$project.ProjectItems.AddFromFile($installPath + "\freerdp-client.dll");
$project.ProjectItems.AddFromFile($installPath + "\winpr.dll");

# set 'Copy To Output Directory' to 'Copy if newer'
$project.ProjectItems.Item("freerdp.dll").Properties.Item("CopyToOutputDirectory").Value = 2;
$project.ProjectItems.Item("freerdp-client.dll").Properties.Item("CopyToOutputDirectory").Value = 2;
$project.ProjectItems.Item("freerdp-client.dll").Properties.Item("CopyToOutputDirectory").Value = 2;
$project.ProjectItems.Item("winpr.dll").Properties.Item("CopyToOutputDirectory").Value = 2;
