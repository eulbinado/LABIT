param($installPath, $toolsPath, $package, $project)

# Remove linked files
$project.ProjectItems.Item("freerdp.dll").Remove();
$project.ProjectItems.Item("freerdp-client.dll").Remove();
$project.ProjectItems.Item("winpr.dll").Remove();
