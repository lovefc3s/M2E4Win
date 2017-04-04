# M2E4Win  
## This project MySQL Workbench Models File to Microsoft.NEt 4.5 .NET Entity Framework 6.0 C# source code Generator  

　MySQL Workbench models ファイルを参照したモデルファーストの
Entity Frameworkで使用する　C#　ソースコードを生成します。
## 依存関係、環境 (Dependencies)  
MicroSoft VisualStudio 2015 で開発しました。

NuGet サイトからパッケージ  
MySql.Data.6.9.9以降をインストールして下さい。  
.NET Entity Framework 6.0以降をインストールして下さい。  

## M2E4Win のビルド
zipファイルを解凍後、VisualStudio 2015でソリューションファイル(”M2E4Win.sin”)を読込みビルドして下さい。
  
## 入力ファイル・使用ファイル
+ File name  
  + BookDB.mwb  
  mwb形式のサンプルファイルです。
  蔵書テーブルと著者テーブルを収めました。  
  Entiy対応ソースファイルが正しく出力されるか確認してください。  
  + M2E4Mono.exe  
  Mono環境下で実行ファイルをクリックすると起動します。  
  $ mono ./M2E4Mono/bin/Debug/M2E4Mono.exe  
  
+ 入力項目  
  + MySQL WorkBench Models File  
  ファイル名を入力または選択してください。  
  + Server  
  例：’192.168.0.254','localhost:3306'　IPアドレス等　を入力してください。  
  + User ID  
  MySqlのユーザー名を入力してください。  
  + Password  
  MySqlのパスワードを入力してください。  
  + Database  
  MySqlのデータベース名を入力してください。存在確認はいたしません。存在しない時はDatabaseを自動生成します。 + Namespace  
  C#で使用するNamespaceを必ず指定します。 
  + 接続TEST  
  接続テストを行い、結果を表示します。
  + キャンセル  
  各項目を消去します。  
  
+ 出力ファイル（Output File）  
  + （mwbfilename).cs  
    mwbファイルと同じ名前でC#のソースファイル出力します。  
    プロジェクトにこの出力ファイルを追加してください。  
  + AppconfigReadme.txt  
    プロジェクトの設定ファイル"App.config" の 設定例を示します。  
    connectionStrings節の中をコピー＆ペーストして使用して下さい。  
  
+ 同梱ファイル（使用例 File)
  + book.mwb  
   MySQL Workbench Models 使用例です。
   簡単なautherテーブルとbookテーブルからビューを1つ含むモデルのEntityコードの生成を確認出来ます。  

## MySQL Workbench Models File について  
MySQLへ初回の接続が確立された後、Databaseが存在しない時はDatabaseを自動生成します。  
  
## 使用例
MySQL Workbench Models file -> "book.mwb" の場合。
```csharp  
using System;
using System.Data.Entity;
using System.Linq;
namespace bookcontext {
	class Program {
		static void Main(string[] args) {
      //  Database Auto Migrate
      Database.SetInitializer(new bookMigrateDatabaseToLatestVersion());
      //  Database None Migrate
      Database.SetInitializer(new NullDatabaseInitializer<bookDB>());
      bookDB db = new bookDB();
    }
  }
}  
```
  ```Database.SetInitializer(new bookMigrateDatabaseToLatestVersion());```と  
  ```Database.SetInitializer(new NullDatabaseInitializer<bookDB>());```　はどちらか一方を採用して下さい。  
  
### 補足事項  
  上記の使用例で```Database.SetInitializer(new bookMigrateDatabaseToLatestVersion());```を使用した場合  
ソースファイルを分割することにより、純粋なコードファーストのコードを記述してDatabase 初期化するなくテーブルの追加や変更など更新することができます。
