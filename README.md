# M2E4Win  
## This project MySQL Workbench Models File to Microsoft.NEt 4.5 .NET Entity Framework 6.0 C# source code generator  

　MySQL Workbench models ファイルを参照したモデルファーストの
Entity Frameworkで使用する　C#　ソースコードを生成します。

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
  + （mwbｆｉｌｅname).ｃｓ  
    mwbファイルと同じ名前でC#のソースファイル出力します。  
    プロジェクトにこの出力ファイルを追加してください。  
  + AppconfigReadme.txt  
    プロジェクトの設定ファイル"App.config" の 設定例を示します。  
    <connectionStrings>節の中をコピー＆ペーストして使用して下さい。  
  
+ 同梱ファイル（使用例 File)
  + book.mwb  
   MySQL Workbench Models 使用例です。
   簡単なautherテーブルとbookテーブルからビューを1つ含むモデルのEntityコードの生成を確認出来ます。  

### MySQL WorkBench Models File について  
MySQLへ初回の接続が確立された後、Databaseが存在しない時はDatabaseを自動生成します。  
