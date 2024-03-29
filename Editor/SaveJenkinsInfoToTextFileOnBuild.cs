﻿using System;
using System.IO;
using System.Text;
using JetBrains.Annotations;
using Kogane.Internal;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Kogane
{
    public sealed class SaveJenkinsInfoToTextFileOnBuild : CompletableProcessBuildWithReportBase
    {
        private sealed class Options
        {
            [Option( "buildNumber" )]      public string BuildNumber      { get; [UsedImplicitly] set; } = string.Empty;
            [Option( "buildId" )]          public string BuildId          { get; [UsedImplicitly] set; } = string.Empty;
            [Option( "buildDisplayName" )] public string BuildDisplayName { get; [UsedImplicitly] set; } = string.Empty;
            [Option( "jobName" )]          public string JobName          { get; [UsedImplicitly] set; } = string.Empty;
            [Option( "jobBaseName" )]      public string JobBaseName      { get; [UsedImplicitly] set; } = string.Empty;
            [Option( "buildTag" )]         public string BuildTag         { get; [UsedImplicitly] set; } = string.Empty;
            [Option( "buildTimestamp" )]   public string BuildTimestamp   { get; [UsedImplicitly] set; } = string.Empty;
        }

        private static readonly string DIRECTORY_NAME = $"Assets/{nameof( SaveJenkinsInfoToTextFileOnBuild )}/Resources";

        public static Func<bool> OnIsRelease { get; set; }

        private static bool IsRelease => OnIsRelease?.Invoke() ?? false;

        protected override void OnStart( BuildReport report )
        {
            // リリースビルドにテキストファイルが含まれないように
            // ビルド開始時に削除しています
            Refresh();

            if ( IsRelease ) return;
            if ( !Application.isBatchMode ) return;

            var setting = SaveJenkinsInfoToTextFileOnBuildSetting.instance;
            var options = CommandLineParser.ParseFromCommandLineArgs<Options>();

            var result = setting.Template
                    .Replace( "#BUILD_NUMBER#", options.BuildNumber )
                    .Replace( "#BUILD_ID#", options.BuildId )
                    .Replace( "#BUILD_DISPLAY_NAME#", options.BuildDisplayName )
                    .Replace( "#JOB_NAME#", options.JobName )
                    .Replace( "#JOB_BASE_NAME#", options.JobBaseName )
                    .Replace( "#BUILD_TAG#", options.BuildTag )
                    .Replace( "#BUILD_TIMESTAMP#", options.BuildTimestamp )
                ;

            Directory.CreateDirectory( DIRECTORY_NAME );
            var path = $"{DIRECTORY_NAME}/{setting.FileName}";
            File.WriteAllText( path, result, Encoding.UTF8 );
            AssetDatabase.ImportAsset( path );
        }

        protected override void OnComplete()
        {
            if ( IsRelease ) return;
            Refresh();
        }

        private static void Refresh()
        {
            var directoryName = Path.GetDirectoryName( DIRECTORY_NAME );
            if ( !AssetDatabase.IsValidFolder( directoryName ) ) return;
            AssetDatabase.DeleteAsset( directoryName );
        }
    }
}