using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Build.Profile;

namespace AppBuilder.Editor {
   [UsedImplicitly]
   public static class AppBuilder {
      // usage: 

      [UsedImplicitly]
      public static void BuildProject() {
         var args = ParseCommandLineArgs();

         if (!args.TryGetValue("BuildProfile", out var buildProfileName)) {
            throw new ArgumentException("Missing argument 'BuildProfile'");
         }

         if (!args.TryGetValue("OutputPath", out var outputPath)) {
            throw new ArgumentException("Missing argument 'OutputPath'");
         }

         var buildProfile = AssetDatabase.LoadAssetAtPath<BuildProfile>($"Assets/Settings/Build Profiles/{buildProfileName}.asset");

         if (!buildProfile) {
            throw new ArgumentException($"{nameof(BuildProfile)} {buildProfileName} not found.");
         }

         var buildPlayerOptions = new BuildPlayerWithProfileOptions { buildProfile = buildProfile, locationPathName = outputPath };

         BuildPipeline.BuildPlayer(buildPlayerOptions);
      }

      private static Dictionary<string, string> ParseCommandLineArgs() {
         var result = new Dictionary<string, string>();
         var commandLineArgs = Environment.GetCommandLineArgs();
         for (var i = 0; i < commandLineArgs.Length; i++) {
            var argKey = commandLineArgs[i][1..];

            if (argKey.StartsWith("-") && i < commandLineArgs.Length - 1) {
               result.Add(commandLineArgs[i][1..], commandLineArgs[i + 1]);
            }
         }

         return result;
      }
   }
}