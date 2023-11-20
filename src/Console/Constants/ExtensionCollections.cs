using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console.Constants;

public static class ExtensionCollections
{
   public static List<string> DotNetExtensions =>
   [
       ".cs",
       ".csproj",
       ".sln",
       ".cshtml",
       ".razor"
   ];

   public static List<string> ConfigExtensions =>
   [
        ".json",
        ".xml",
        ".config",
        ".props",
        ".targets",
        ".toml",
        ".csv",
        ".yaml",
        ".yml",
        ".proto",
        ".hjson",
        ".md"
   ];
}
