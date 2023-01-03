using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    [FilePath( "ProjectSettings/Kogane/SaveJenkinsInfoToTextFileOnBuildSetting.asset", FilePathAttribute.Location.ProjectFolder )]
    internal sealed class SaveJenkinsInfoToTextFileOnBuildSetting : ScriptableSingleton<SaveJenkinsInfoToTextFileOnBuildSetting>
    {
        private const string DEFAULT_FILE_NAME = "jenkins.txt";

        private const string DEFAULT_TEMPLATE = @"ビルド番号：#BUILD_NUMBER#
ビルド ID：#BUILD_ID#
ビルド表示名：#BUILD_DISPLAY_NAME#
ジョブ名：#JOB_NAME#
ジョブベース名：#JOB_BASE_NAME#
ビルドタグ：#BUILD_TAG#
ビルド日時：#BUILD_TIMESTAMP#";

        [SerializeField]                                         private string m_fileName = DEFAULT_FILE_NAME;
        [SerializeField][TextArea( minLines: 10, maxLines: 10 )] private string m_template = DEFAULT_TEMPLATE;

        public string FileName => m_fileName;
        public string Template => m_template;

        public void Save()
        {
            Save( true );
        }

        public void ResetToDefault()
        {
            m_fileName = DEFAULT_FILE_NAME;
            m_template = DEFAULT_TEMPLATE;
        }
    }
}