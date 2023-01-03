using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kogane.Internal
{
    internal sealed class SaveJenkinsInfoToTextFileOnBuildSettingProvider : SettingsProvider
    {
        private const string PATH = "Kogane/Save Jenkins Info To Text File On Build";

        private Editor m_editor;

        private SaveJenkinsInfoToTextFileOnBuildSettingProvider
        (
            string              path,
            SettingsScope       scopes,
            IEnumerable<string> keywords = null
        ) : base( path, scopes, keywords )
        {
        }

        public override void OnActivate( string searchContext, VisualElement rootElement )
        {
            var instance = SaveJenkinsInfoToTextFileOnBuildSetting.instance;

            instance.hideFlags = HideFlags.HideAndDontSave & ~HideFlags.NotEditable;

            Editor.CreateCachedEditor( instance, null, ref m_editor );
        }

        public override void OnGUI( string searchContext )
        {
            using var changeCheckScope = new EditorGUI.ChangeCheckScope();

            m_editor.OnInspectorGUI();

            EditorGUILayout.Space();

            if ( GUILayout.Button( "Reset to Default" ) )
            {
                Undo.RecordObject( SaveJenkinsInfoToTextFileOnBuildSetting.instance, "Reset to Default" );
                SaveJenkinsInfoToTextFileOnBuildSetting.instance.ResetToDefault();
            }

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox( "Template で使用できるタグ", MessageType.Info );

            EditorGUILayout.TextArea
            (
                @"#BUILD_NUMBER#：ビルド番号
#BUILD_ID#：ビルド ID
#BUILD_DISPLAY_NAME#：ビルド表示名
#JOB_NAME#：ジョブ名
#JOB_BASE_NAME#：ジョブベース名
#BUILD_TAG#：ビルドタグ
#BUILD_TIMESTAMP#：ビルド日時"
            );

            if ( !changeCheckScope.changed ) return;

            SaveJenkinsInfoToTextFileOnBuildSetting.instance.Save();
        }

        [SettingsProvider]
        private static SettingsProvider CreateSettingProvider()
        {
            return new SaveJenkinsInfoToTextFileOnBuildSettingProvider
            (
                path: PATH,
                scopes: SettingsScope.Project
            );
        }
    }
}