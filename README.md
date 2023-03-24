# UnityToolKit-ToggleGroup-for-VisualElement
![2023-03-24-23-19-25](https://user-images.githubusercontent.com/28706700/227608589-2f3b4664-691b-4f55-966b-edcde9b15eec.png)

## これは何？
Unity UI Builderにボタン風ToggleGroupを追加する拡張です

## 使い方
UI Builderを開いてProjectタブからMyUILayout内にあるToggleGroupをHierarchy上にD＆Dしてください。

ToggleGroup内に必要な数のVisutalElementを追加して、Saveをすると自動で整形されます。

ToggleGroupのInspectorからボタンの色の変更やラベルの変更ができます。

ボタンにテキストが欲しい場合は追加したVisualElementの子にLabelを追加してください。

![2023-03-25-01-32-19 (3)](https://user-images.githubusercontent.com/28706700/227612594-87b6d781-21bd-4e0c-9934-2a57ffe2e702.png)

## クラス名の説明
* toggle-group:一番親のVisualElementを指します。
* toggle-group__top-label:ToggleGroupの上に表示されるラベルを指します。デフォルトでは未記入なので表示されていません。
* toggle-group__container:ToggleGroupに追加するVisualElementを格納するVisualElementを指します。Containerという名称で検索することもできます。
* toggle-group__contents:ToggleGroupに追加したVisualElementを指します。
