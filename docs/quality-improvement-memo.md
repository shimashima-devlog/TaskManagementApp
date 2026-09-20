# 品質改善メモ

## 2026年9月7日

## 新規登録・編集の入力チェック

### 確認した問題
- 新規登録画面で必須項目を未入力のまま登録すると、DBへの保存処理まで進み、以下の例外が発生した。

```text
NOT NULL constraint failed: Tasks.TaskName
```

### 原因
- 入力内容に問題があっても、Controllerで`ModelState.IsValid`を確認せず、`SaveChanges()`まで実行していた。

### 修正内容
- `TaskItem`のタスク名・担当者に`[Required]`を追加した。
- 期限は未入力の状態を扱えるよう、`DateTime`から`DateTime?`に変更し、`[Required]`を追加した。
- Create・EditのPOST処理で`ModelState.IsValid`を確認し、入力エラーがある場合はDBを更新せず入力画面を再表示するようにした。
- `Create.cshtml`、`Edit.cshtml`に`asp-validation-for`を追加した。

### 修正後の確認
- 必須項目が未入力の場合はエラーメッセージが表示され、DBへ保存されないことを確認した。
- 正常な値を入力した場合は登録・更新できることを確認した。

---

## 期限表示の改善

### 修正内容
- `DueDate`に`[DataType(DataType.Date)]`を追加した。
- `DueDate`を`DateTime?`に変更したため、一覧画面の日付表示を以下のように変更した。

```csharp
DueDate?.ToString("yyyy/MM/dd")
```

### 修正後の確認
- 一覧画面・入力画面ともに、期限が日付のみで表示されることを確認した。

---

## 初期表示の改善

### 修正内容
- `Program.cs`のデフォルトルートを`TasksController`に変更し、アプリのルートURLからタスク一覧画面を表示できるようにした。

```csharp
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tasks}/{action=Index}/{id?}")
    .WithStaticAssets();
```

### 修正後の確認
- アプリのルートURLを開くと、タスク一覧画面が表示されることを確認した。

---

## 存在しないデータに対するエラー処理

### 実装内容
- Edit・Deleteでは`_context.Tasks.Find(id)`で対象データを取得するようにした。
- 対象データが存在せず`null`の場合は、`NotFound()`を返すようにした。

### 確認結果
- 存在しないIDを指定してEdit・Deleteへアクセスし、HTTPステータスコード`404`が返されることを確認した。

---

## Delete処理の改善

### 修正内容
- 修正前は、POSTで受け取った`TaskItem`をそのまま削除対象としていた。
- 修正後は、送信された`task.Id`をもとにDBから対象タスクを再取得してから削除するように変更した。

### 修正理由
- 削除対象がDBに存在することを確認してから処理するため。
- 対象データが存在しない場合は`NotFound()`を返し、削除処理を中止できるようにするため。

---

## ステータス選択の不具合修正

### 確認した問題
- 新規登録で「対応中」「完了」を選択しても「未着手」として登録される問題があった。
- 編集画面でも、登録済みのステータスが選択状態に反映されない問題があった。

### 原因
- `Create.cshtml`、`Edit.cshtml`の`<select>`に`asp-for="Status"`を指定していなかった。
- そのため、ステータス選択欄と`TaskItem.Status`を正しく関連付けられていなかった。

### 修正内容
- ステータス選択欄に`asp-for="Status"`を追加した。

```html
<select asp-for="Status">
```

### 修正後の確認
- 新規登録時に選択したステータスが正しく保存されることを確認した。
- 編集画面に現在のステータスが表示されることを確認した。
- 編集画面でステータスを変更し、変更内容が保存されることを確認した。