using System.Reflection;
using MelonLoader;

[assembly: AssemblyTitle(PartsList.BuildInfo.Description)]
[assembly: AssemblyDescription(PartsList.BuildInfo.Description)]
[assembly: AssemblyCompany(PartsList.BuildInfo.Company)]
[assembly: AssemblyProduct(PartsList.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + PartsList.BuildInfo.Author)]
[assembly: AssemblyTrademark(PartsList.BuildInfo.Company)]
[assembly: AssemblyVersion(PartsList.BuildInfo.Version)]
[assembly: AssemblyFileVersion(PartsList.BuildInfo.Version)]
[assembly: MelonInfo(typeof(PartsList.PartsList), PartsList.BuildInfo.Name, PartsList.BuildInfo.Version, PartsList.BuildInfo.Author, PartsList.BuildInfo.DownloadLink)]
[assembly: MelonColor()]

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame(null, null)]