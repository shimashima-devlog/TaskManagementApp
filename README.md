# TaskManagementApp

## アプリケーションの概要
- TaskManagementAppは、タスクを管理するためのWebアプリケーション。
- 主に、タスクの一覧表示と、新規登録、編集、削除が可能。

## 作成目的
- ASP.NET Core MVCを使用し、MVCパターンの基本的な構成を学ぶ。
- HTTPリクエストからController、DB操作、画面表示までの処理とデータの流れを理解する。

## 使用した技術
- C#
- .NET SDK 10.0.400
- ASP.NET Core MVC
- Entity Framework Core 10.0.11
- SQLite

## 実装した機能
- タスクの一覧表示
- タスクの新規登録
- タスクの編集
- タスクの削除
- 必須項目の入力チェック
- SQLiteを使用したデータの保存・読み込み

## 起動方法
1. .NET SDKがインストールされている環境で、プロジェクトフォルダを開く。

2. ターミナルで、必要なパッケージを取得する。
   `dotnet restore`

3. ローカルツールを取得する。
 　`dotnet tool restore`

4. SQLiteデータベースを作成する。
   `dotnet tool run dotnet-ef database update`

5. アプリケーションを起動する。
   `dotnet run`

6. 起動後、ターミナルに表示されたURLにアクセスする。
   `http://localhost:XXXX`