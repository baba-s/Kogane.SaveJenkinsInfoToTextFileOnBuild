# Kogane Save Jenkins Info To Text File On Build

ビルド時に Jenkins の情報を Resources フォルダのテキストファイルに書き込むエディタ拡張

## 使用例

![2022-10-15_162807](https://user-images.githubusercontent.com/6134875/195974868-2c9d8b58-6c22-47d9-8522-c95b61f6819d.png)

Project Settings で Jenkins の情報を書き込むテキストファイルの保存場所や  
書き込むテキストファイルのフォーマットを設定します

```shell
"C:\Program Files\Unity\Hub\Editor\2022.1.11f1\Editor\Unity.exe" ^
    -quit ^
    -batchmode ^
    -executeMethod "BuildScript.Build" ^
    -logFile - ^
    -projectPath "UnityProject" ^
    -buildNumber "%BUILD_NUMBER%" ^
    -buildId "%BUILD_ID%" ^
    -buildDisplayName "%BUILD_DISPLAY_NAME%" ^
    -jobName "%JOB_NAME%" ^
    -jobBaseName "%JOB_BASE_NAME%" ^
    -buildTag "%BUILD_TAG%" ^
    -buildTimestamp "%BUILD_TIMESTAMP%"
```

そして、Jenkins から Unity ビルドする時に  
Jenkins の情報を上記のようにコマンドライン引数で Unity に渡します

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

最後に上記のようなコードを記述することで  
ビルド時における Jenkins のジョブ名やビルド日時を取得できます

## 参考

* 【Jenkins】ビルド日時の環境変数を使用する方法
    * https://baba-s.hatenablog.com/entry/2022/09/23/092305
