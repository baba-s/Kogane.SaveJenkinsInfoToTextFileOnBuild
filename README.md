# Kogane Save Jenkins Info To Text File On Build

ビルド時に Jenkins の情報を Resources フォルダのテキストファイルに書き込むエディタ拡張

## 使用例

```shell
"C:\Program Files\Unity\Hub\Editor\2022.1.11f1\Editor\Unity.exe" ^
    -quit ^
    -batchmode ^
    -executeMethod "BuildScript.Build" ^
    -logFile - ^
    -projectPath "C:\Users\baba-s\GitHub\unity-package-sandbox\UnityProject" ^
    -buildNumber "%BUILD_NUMBER%" ^
    -buildId "%BUILD_ID%" ^
    -buildDisplayName "%BUILD_DISPLAY_NAME%" ^
    -jobName "%JOB_NAME%" ^
    -jobBaseName "%JOB_BASE_NAME%" ^
    -buildTag "%BUILD_TAG%" ^
    -buildTimestamp "%BUILD_TIMESTAMP%"
```

```csharp
using UnityEngine;
using UnityEngine.UI;

public sealed class Example : MonoBehaviour
{
    public Text m_text;

    private void Awake()
    {
        var textAsset = Resources.Load<TextAsset>( "jenkins" );
        m_text.text = textAsset.text;
    }
}
```

## 備考

* 【Jenkins】ビルド日時の環境変数を使用する方法
    * https://baba-s.hatenablog.com/entry/2022/09/23/092305