# C#, WPFで画像から文字認識を試す

## 開発環境など

- Microsoft Visual Studio Community 2019
  - Version 16.11.26
- C#
- WPF
- .NET5

## 大まかな処理

画像内の文字を認識したい

- 画像を選ぶ
- 選んだ画像を表示する
- 選んだ画像から文字を認識する
- 認識した文字を表示する
- 認識した文字をクリップボードにコピーする

## 問題

- Nugetで`Microsoft.Windows.SDK.Contracts`をインストールしたら次のエラーが発生した
```
重大度レベル	コード	説明	プロジェクト	ファイル	行	抑制状態
エラー	NETSDK1135	SupportedOSPlatformVersion 10.0.22621.0 を TargetPlatformVersion 7.0 より大きくすることはできません。	TryOCR2	C:\Program Files\dotnet\sdk\5.0.416\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.TargetFrameworkInference.targets	185	
```
- `Microsoft.Windows.SDK.Contracts`は不要
  - .NET 5 から Windows Runtime API を呼ぶのが凄い楽になってる
    - https://qiita.com/okazuki/items/acf95b3ebb21d4d5083b
  - デスクトップ アプリで Windows ランタイム API を呼び出す
    - https://learn.microsoft.com/ja-jp/windows/apps/desktop/modernize/desktop-to-uwp-enhance
  - Windows 10 リリース情報
    - https://learn.microsoft.com/ja-jp/windows/release-health/release-information

## 参考

### メイン

- 【C#】文字認識をWindows10のOCRでやってみた！
  - https://marunaka-blog.com/wpf-ocr-windows10/2260/

### ファイルダイアログ

- コモン ダイアログ ボックスを開く方法 (WPF .NET)
  - https://learn.microsoft.com/ja-jp/dotnet/desktop/wpf/windows/how-to-open-common-system-dialog-box?view=netdesktop-7.0
- 「ファイルを開く」ダイアログボックスを表示する
  - https://dobon.net/vb/dotnet/form/openfiledialog.html
- 特殊ディレクトリのパスを取得する
  - https://dobon.net/vb/dotnet/file/getfolderpath.html

### クリップボード

- Clipboard クラス
  - https://learn.microsoft.com/ja-jp/dotnet/api/system.windows.clipboard?view=windowsdesktop-7.0