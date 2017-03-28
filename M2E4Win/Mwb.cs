using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using M2E4Win.Properties;
namespace M2E4Win
{
	//
	//	MySQL Workbench Model File
	//
	public class Mwb
	{
		public string Filename { get; set; }
		public string Server { get; set; }
		public string UserID { get; set; }
		public string Password { get; set; }
		public string Database { get; set; }
		public string Name { get; set; }
		private MyTable _table = null;
		private MyView _view = null;
		private MyColumn _column = null;
		private List<MyColumn> Columns;
		private List<MyTable> Tables;
		private List<MyView> Views;
		private bool dbe = false;
		private int clevel = 0;
		private List<MyUserDatatype> Usertype;
		private List<MyRoutine> Routines;
		private List<MyIndex> Indexes;
		private string tab;
		private string nl;
		private string tit;
		private static string bd = "\"";
		private static string execute = "context.Database.ExecuteSqlCommand";
		private string lp = "(";
		private string rp = ")";
		private string sc = ";";
		private string prop = " { get; set; }";
		private string _init = "_Initialization_()";

		public Mwb () {
			_table = null;
			_column = null;
			tab = "\t";
			nl = Environment.NewLine;
			Columns = new List<MyColumn>();
			Tables = new List<MyTable>();
			Views = new List<MyView>();
			Usertype = new List<MyUserDatatype>();
			MyUserDatatype typ = new MyUserDatatype();
			Usertype.Add(typ);
			typ.id = 1;
			typ.name = "VARCHAR";
			typ.actualType = "com.mysql.rdbms.mysql.datatype.varchar";
			typ.flags = "";
			typ.sqlDefinition = "verchar";
			Routines = new List<MyRoutine>();
			Indexes = new List<MyIndex>();
			tit = "";
			Database = "";
			
		}
		public bool Load(string filename = "") {
			bool ret = false;
			if(filename.Length > 0) Filename = filename;
			string zipPath = Filename;
			string extractPath = Path.GetTempPath() + Resources.DirSp + "M2E4Win" + Resources.DirSp;
			Directory.CreateDirectory(extractPath);
			List<string> lst = new List<string>();
			using (ZipArchive archive = ZipFile.OpenRead(zipPath))
			{
				foreach (ZipArchiveEntry entry in archive.Entries)
				{
					if (entry.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
					{
						lst.Add(extractPath + entry.FullName);
						entry.ExtractToFile(Path.Combine(extractPath, entry.FullName),true);
					}
				}
			}
			// 上記までが解凍の処理 	//Up to above Expand process 
			// 以下XML処理   //XML process
			//
			if (lst.Count < 1)	return false;
			XmlReader red = XmlReader.Create(lst[0]);
			XmlDocument doc = new XmlDocument();
			doc.Load(red);
			//red.Close();
			XmlNode nod = doc.ChildNodes[1];
			//ノード処理  //Node process
			NodeProcess1(nod,1);
			if (_table != null)
			{
				Tables.Add(_table);
			}
			nod = nod.NextSibling;
			//red.Close();
			Tables.Clear();
			XDocument xd = XDocument.Load(lst[0]);
			IEnumerable<XElement> ele = xd.Elements();
			List<XElement> ere2 = ele.ToList();
			XElement ere1 = ere2[0];
			if (ere1.HasElements == true) {
				NodeProcess2(ere1, 1);
			}
			setViews();
			return ret;
		}
		private void setViews() {
			foreach (MyView view in Views) {
				int count = 0;
				string sql = view.sqlDefinition;
				view.Columns.Clear();
				int i = 0;
				string wk = sql;
				i = wk.IndexOf("select ");
				if (i < 0) {
					i = wk.IndexOf("Select ");
				}
				if (i < 0) {
					i = wk.IndexOf("SELECT ");
				}
				if (i > -1)  i = i + 7;  else break;
				int j = wk.IndexOf("from ");
				if (j < 0) {
					j = wk.IndexOf("From ");
				}
				if (j < 0) {
					j = wk.IndexOf("FROM ");
				}
				if (j < 0) break;
				wk = wk.Substring(i, wk.Length-(j-7));
				//  set Columns 
				while (sql.Length > i) {
					j = wk.IndexOf(',');
					int l = j - 1;
					if (l < 0) {
						i = sql.Length + 1;
						break;
					}
					string token = "";
					if (j > -1) token = wk.Substring(0, l);
					else token = wk;
					if (wk.Length <= token.Length + 2) {
						i = sql.Length + 1;
					} else {
						wk = wk.Substring(token.Length + 2);
					}
					token = token.Replace("`", "");
					count++;
					MyColumn col = new MyColumn();
					col.id = view.Columns.Count + 1;
					int spt = token.IndexOf("AS");
					if (spt > -1)
						col.name = token.Substring(spt + 3);
					else
						col.name = "Column" + count.ToString("000");
					int itbl = token.LastIndexOf(".");
					string tbl = "";
					if (itbl > -1) {
						col.name = token.Substring(itbl+1);
						tbl = token.Substring(0, itbl);
					}
					if (tbl.LastIndexOf(".") > -1) tbl = tbl.Substring(tbl.LastIndexOf(".") + 1,tbl.Length- tbl.LastIndexOf(".")-1);
					MyTable stb = getTable(tbl);
					if (stb != null) {
						col.tablelink = stb.link;
						MyColumn rcl = getTableColumn(stb, col.name);
						if (rcl != null) {
							col.linkid = rcl.linkid;
						}
					}
					view.Columns.Add(col);
					i = i + l;
				}
			}
		}
		private MyColumn getTableColumn(MyTable tbl, string colname) {
			MyColumn ret = null;
			foreach (MyColumn col in tbl.Columns) {
				if (col.name == colname) {
					ret = col;
					break;
				}
			}
			return ret;
		}
		private MyTable getTable(string tblname) {
			MyTable ret = null;
			foreach (MyTable tbl in Tables) {
				if (tbl.name == tblname) {
					ret = tbl;
					break;
				}
			}
			return ret;
		}
		//
		//	Entity Code Gen
		//
		public string GetSourceCode(){
			tit = Filename;
			tit = tit.Substring(tit.LastIndexOf(Resources.DirSp)+1,tit.LastIndexOf(".")- tit.LastIndexOf(Resources.DirSp) - 1);
			string ret = Resources.Header1 + nl
				+ Resources.Header2 + nl
				+ Resources.Header3 + nl
				+ Resources.Header4 + nl;
			ret = ret + Resources.ComSp + tab +tit + ".cs" + nl + nl;
			ret = ret + "NameSpace " + Name + Resources.SB + nl
				+ tab + "[DbConfigurationType(typeof(MySqlEFConfiguration))]" + nl
				+ tab + "public partial class " + tit + "DB : DbContext {" + nl
				+ tab + tab + "public " + tit + "DB() : base(" + bd + "name=" + tit + "ConnectionS" + bd + ") {" + nl
				+ tab + tab + tab + _init + ";" + nl
				+ tab + tab + "}" + nl
				+ tab + tab + "public " + tit + "DB(string ConnectionString) : base( ConnectionString ){ " + nl
				+ tab + tab + tab + _init + ";" + nl
				+ tab + tab + "}" + nl
				+ tab + tab + "public " + tit + "DB(MySqlConnection Connection) : base(Connection.ConnectionString) {" + nl
				+ tab + tab + tab + _init + ";" + nl
				+ tab + tab + "}" + nl
				+ tab + tab + "private void "+ _init + " {" + nl;
			if (Routines.Count > 0) {
				ret = ret + tab + tab + tab + tit + "Connection = new MySqlConnection();" + nl;
			}
			foreach (MyRoutine rtn in Routines) {
				ret = ret + tab + tab + tab + rtn.name + " = new MySqlCommand();" + nl;
				ret = ret + tab + tab + tab + rtn.name + ".Connection = (MySqlConnection)"
					+ "Database.Connection;" + nl;
				ret = ret + tab + tab + tab + rtn.name + ".CommandText = \"CALL " + rtn.name;
				if (rtn.Params.Count > 0) {
					ret = ret + " (\"" + nl;
				} else {
					ret = ret + ";\";" + nl;
				}
				int wk1 = 0;
				foreach (MyRoutineParam prm in rtn.Params) { 
					wk1++;
					ret = ret + tab + tab + tab + tab + "+ \"`" + prm.name + "`";
					if (wk1 != rtn.Params.Count)
						ret = ret + ",\"" + nl;
					else
						ret = ret + "\"" + nl + tab + tab + tab + tab + "+ \");\";" + nl;
				}
				foreach (MyRoutineParam prm in rtn.Params) {
					ret = ret + tab + tab + tab + rtn.name + ".Parameters.Add(new MySqlParameter(\""
						+ prm.name + "\",";
					int len = prm.datatype.LastIndexOf("(");
					if (len < 1) len = prm.datatype.Length;
					//len--;
					string type = prm.datatype.Substring(0,len);
					type = type.ToLower();
					type = paramtype(type);
					ret = ret + type + "));" + nl;
				}
			}
			ret = ret + tab + tab + Resources.EB + nl;
			foreach (MyTable tbl in Tables){
				ret = ret + tab + tab + "public DbSet<" + tbl.name + "> " + tbl.name + "s " + prop + nl;
			}
			ret = ret + nl;
			if(Routines.Count > 0)
				ret = ret + tab + tab + "private MySqlConnection " + tit + "Connection;" + nl;
			foreach (MyRoutine rtn_name in Routines) {
				ret = ret + tab + tab + "public MySqlCommand " + rtn_name.name + ";" + nl;
			}
			ret = ret + nl + tab + "}" + nl + nl;
			foreach (MyTable tbl in Tables){

				ret = ret + tab + "public partial class " + tbl.name + " {" + nl;
				MyIndexColumn icx = null;
				MyIndex ixx = null;
				foreach(MyColumn col in tbl.Columns){
					icx = null;
					ixx = null;
					foreach (MyIndex idx in Indexes) {
						foreach (MyIndexColumn icl in idx.IndexColumn) {
							if (col.linkid == icl.referencedColumn) {
								icx = icl;
								break;
							}
						}
						if (icx != null) {
							ixx = idx;
							break;
						}
					}
					if ((icx != null) && (ixx != null)) {
						if (ixx.isPrimary > 0) {
							if (col.autoIncrement > 0) {
								ret = ret + tab + tab + "[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]" + nl;
							} else {
								ret = ret + tab + tab + "[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]" + nl;
							}
						}
					}
					MyUserDatatype typ = Usertype.Find(x => x.actualType==col.SimpleDatatype);
					string styp = "";
					if (typ != null) styp = cstype(typ);
					if (styp.Length < 1) {
						styp = col.SimpleDatatype.Substring(col.SimpleDatatype.LastIndexOf(".")+1, col.SimpleDatatype.Length - col.SimpleDatatype.LastIndexOf(".")-1);
						styp = cstype(styp);
					}
					if (styp != "string") {
						if (col.isNotNull == 0)	{
							ret = ret + tab + tab + "public Nullable<" + styp + "> " + col.name + prop + nl;
						}
						else {
							ret = ret + tab + tab + "public " + styp + " " + col.name + prop + nl;
						}
					}
					else {
						ret = ret + tab + tab + "public " + styp + " " + col.name + prop + nl;
					}
				}
				ret = ret + tab + Resources.EB + nl;
			}
			ret = ret + tab + "public partial class " + tit + "CreateDatabaseIfNotExists : CreateDatabaseIfNotExists<" + tit + "> {" + nl
				+ tab + tab + "public " + tit + "CreateDatabaseIfNotExists() :base() {" + nl
				+ tab + tab + Resources.EB + nl
				+ tab + tab + "protected override void Seed(" + tit + " context) {" + nl
				+ tab + tab + tab + "base.Seed(context);" + nl;
			foreach (MyView view in Views) {
				ret = ret + tab + tab + tab + execute + lp + bd + view.sqlDefinition + bd + rp + sc + nl;
			}
			foreach (MyRoutine rtn in Routines) {
				ret = ret + tab + tab + tab + execute + lp + bd + rtn.sqlDefinition + bd + rp + sc + nl;
			}
			ret = ret + tab + tab + Resources.EB + nl
				+ tab + Resources.EB + nl;
			ret = ret + tab + "internal sealed class " + tit + "MigrationsConfigration : DbMigrationsConfiguration <" + tit + "DB> {" + nl
				+ tab + tab + "public " + tit + "MigrationsConfigration() {" + nl
				+ tab + tab + tab + "AutomaticMigrationsEnabled = true;" + nl
				+ tab + tab + tab + "AutomaticMigrationDataLossAllowed = true;" + nl
				+ tab + tab + Resources.EB + nl
				+ tab + tab + "protected override void Seed(" + tit + "DB context) {" + nl;
			foreach (MyView view in Views) {
				ret = ret + tab + tab + tab + execute + lp + nl;
				ret = ret + SqlFormat(view.sqlDefinition,4) + tab + tab + tab + rp + sc + nl;
			}
			foreach (MyRoutine rtn in Routines) {
				//ret = ret + tab + tab + tab + execute + lp + bd + rtn.sqlDefinition + bd + rp + sc + nl;
				ret = ret + tab + tab + tab + execute + lp + nl;
				ret = ret + SqlFormat(rtn.sqlDefinition, 4) + tab + tab + tab + rp + sc + nl;
			}
			ret = ret + tab + tab + tab + "base.Seed(context);" + nl
				+ tab + tab + Resources.EB + nl
				+ tab + Resources.EB + nl
				+ Resources.EB + nl;
			string savename = Filename;
			savename = savename.Substring(0,savename.LastIndexOf(Resources.DirSp));
			savename = savename + Resources.DirSp + tit + ".cs";
			StreamWriter wrt =  File.CreateText(savename);
			wrt.Write(ret);
			wrt.Close();
			savename = savename.Substring(0, savename.LastIndexOf(Resources.DirSp));
			savename = savename + Resources.DirSp + "RreadmeAppconfig.txt";
			wrt = File.CreateText(savename);
			wrt.Write(Resources.configxml1);
			wrt.Write(tit + "ConnectionS\" connectionString=");
			//wrt.Write(Resources.Config2);
			MySql.Data.MySqlClient.MySqlConnectionStringBuilder bld = new MySql.Data.MySqlClient.MySqlConnectionStringBuilder();
			bld.Server = Server;
			bld.UserID = UserID;
			bld.Password = Password;
			bld.Database = Database;
			wrt.Write("\"" + bld.ConnectionString + "\"");
			wrt.Write(Resources.configxml2);
			wrt.Close();
			return ret;
		}
		public string paramtype(string type) {
			string rtn = "";
			switch (type) {
			case "bit": rtn = "MySqlDbType.Bit"; break;
			case "datetime": rtn = "MySqlDbType.DateTime"; break;
			case "decimal": rtn = "MySqlDbType.Decimal"; break;
			case "float": rtn = "MySqlDbType.Float"; break;
			case "double": rtn = "MySqlDbType.Double"; break;
			case "varchar": rtn = "MySqlDbType.VarChar"; break;
			case "short": rtn = "MySqlDbType.Int16"; break;
			case "int": rtn = "MySqlDbType.Int32"; break;
			case "long": rtn = "MySqlDbType.Int64"; break;
			case "text": rtn = "MySqlDbType.Text"; break;
			}
			return rtn;
		}
		public string cstype(string type) {
			string ret = type;

			switch (type) {
			case "int": break;
			case "string": break;
			case "short": break;
			case "long": break;
			case "bit": ret = "bool"; break;
			case "char": break;
			case "byte": break;
			case "tinyint": ret = "byte"; break;
			case "smallint": ret = "short"; break;
			case "mediumint": ret = "int"; break;
			case "bigint": ret = "long"; break;
			case "float": break;
			case "double": break;
			case "decimal": break;
			case "varchar": ret = "string"; break;
			case "binary": ret = "object"; break;
			case "varbinary": ret = "object"; break;
			case "tinytext": ret = "string"; break;
			case "text": ret = "string"; break;
			case "mediumtext": ret = "string"; break;
			case "longtext": ret = "string"; break;
			case "tinyblob": ret = "char"; break;
			case "blob": ret = "char"; break;
			case "mediumblob": ret = "char"; break;
			case "longblob": ret = "char"; break;
			case "datetime": ret = "DateTime"; break;
			case "datetime_f": ret = "DateTime"; break;
			case "date": ret = "DateTime"; break;
			case "time": ret = "DateTime"; break;
			case "time_f": ret = "DateTime"; break;
			case "year": ret = "DateTime"; break;
			case "timestamp": ret = "DateTime"; break;
			case "timestamp_f": ret = "DateTime"; break;
			case "geometry": ret = "object"; break;
			case "point": ret = "object"; break;
			case "curve": ret = "object"; break;
			case "linestring": ret = "object"; break;
			case "surface": ret = "object"; break;
			case "polygon": ret = "object"; break;
			case "geometrycollection": ret = "object"; break;
			case "multipoint": ret = "object"; break;
			case "multicurve": ret = "object"; break;
			case "multilinestring": ret = "object"; break;
			case "multisurface": ret = "object"; break;
			case "multipolygon": ret = "object"; break;
			case "enum": ret = "object"; break;
			case "set": ret = "object"; break;
			}
			return ret;
		}
		public string cstype(MyUserDatatype type) {
			string ret = "int";
			if (type == null) return ret;
			switch (type.name)
			{
			case "NUMERIC": ret = "decimal"; break;
			case "DECIMAL": ret = "decimal"; break;
			case "INT1" : ret = "bool"; break; 
			case "INT2" : ret = "short"; break;
			case "MIDDLEINT": break;
			case "INTEGER": break;
			case "INT3": break;
			case "INT4" : break;
			case "INT8" : ret = "long"; break;
			case "FLOAT4" : ret = "float"; break;
			case "FLOAT8" : ret = "double"; break;
			case "LONG": ret = "string"; break;
			case "LONG VARCHAR": ret = "string"; break;
			case "VARCHAR": ret = "string"; break;
			case "TEXT": ret = "string"; break;
			case "CHARACTER": ret = "byte"; break;
			default: ret = ""; break;
			}
			return ret;
		}
		//	
		//	SQL 文字列　整形
		//  
		private string SqlFormat(string sql ,int tc=0) {
			string ret =  "";
			string wk = sql;
			wk = wk.Replace("\r\n", "\n");
			int count = 0; 
			while (wk.Length > 0) {
				string line = "";
				int index = wk.IndexOf("\n");
				if (index < 0) {
					line = wk;
					wk = "";
				} else {
					line = wk.Substring(0,index);
					wk = wk.Substring(index+1);
				}
				for (int i = 0; i < tc; i++) {
					ret = ret + tab;
				}
				if (count > 0) ret = ret + "+ ";
				ret = ret + bd + line + bd + nl;
				count++;
			}
			return ret;
		}
		//
		//	ノードの再帰的処理１
		//
		private void NodeProcess1(XmlNode nod,int level) {
			if (nod.Attributes != null)
			{
				foreach (XmlAttribute atr in nod.Attributes)
				{
					if ((atr.Name == "struct-name") && (atr.Value == "db.mysql.Table")) {
						if (_table != null) {
							Tables.Add(_table);
						}
						_table = new MyTable();
					} else if ((atr.Name == "struct-name") && (atr.Value == "db.mysql.Column")) {
						clevel = level;
						if (_column != null) {
							Columns.Add(_column);
							_table.Columns.Add(_column);
						}
						_column = new MyColumn();
					} else if ((atr.Name == "key") && (atr.Value == "autoIncrement")) {
						_column.autoIncrement = int.Parse(nod.FirstChild.Value);
					} else if ((atr.Name == "key") && (atr.Value == "tableEngine")) {
						_table.tableEngine = nod.FirstChild.Value;
						dbe = true;
					} else if ((atr.Name == "key") && (atr.Value == "name")) {
						if (dbe)
						{
							if((_table != null) && (nod.FirstChild != null)){
								_table.name = nod.FirstChild.Value;
								dbe = false;
							}
						}
						else
						{
							if ((_column != null) && (nod.FirstChild != null))
							{
								_column.name = nod.FirstChild.Value;
							}
						}
					}
				}
			}
			foreach (XmlNode nod1 in nod.ChildNodes)
			{
				NodeProcess1(nod1,level+1);
			}
			if (clevel == level) {
				if (_column != null)
				{
					Columns.Add(_column);
					_table.Columns.Add(_column);
					_column = null;
				}
				clevel = 0;
			}
		}
		//
		//	ノードの再帰的処理２
		//
		private void NodeProcess2(XElement ere,int level) {
			//IEnumerable<XNode> nodes = nod.Nodes();
			//List<XNode> nod2 = nodes.ToList();
			if (ere.HasAttributes==true) {
				List<XAttribute> latr = ere.Attributes().ToList();
				foreach (XAttribute atr in latr) { 
					XName nam = atr.Name;
					if((nam.ToString() == "struct-name") && (atr.Value == "db.UserDatatype")){
						if (ere.HasElements) {
							GetUserDatatype(ere.Elements().ToList());
						}
					}
					else if ((nam.ToString() == "struct-name") && (atr.Value == "db.mysql.Table"))	{
						XName id2 = XName.Get("id");
						XAttribute atr2 = ere.Attribute(id2);
						string linkid = "";
						if (atr2 != null)
						{
							linkid = atr2.Value;
						}
						if (ere.HasElements)
						{
							GetTable(ere.Elements().ToList(),linkid);
						}
					}
					else if ((nam.ToString() == "struct-name") && (atr.Value == "db.mysql.Column")) {
						_table = Tables[Tables.Count - 1];
						XName id2 = XName.Get("id");
						XAttribute atr2 = ere.Attribute(id2);
						string linkid = "";
						if (atr2 != null) {
							linkid = atr2.Value;
						}
						if (ere.HasElements)
						{
							GetColumn(ere.Elements().ToList() , linkid);
						}
					}
					else if ((nam.ToString() == "struct-name") && (atr.Value == "db.mysql.Routine"))
					{
						MyRoutine rot = new MyRoutine();
						Routines.Add(rot);
						rot.id = Routines.Count;
						IEnumerable<XElement> els = ere.Elements();
						if (els != null)
						{
							foreach (XElement ere3 in els)
							{
								if (ere3.HasAttributes)
								{
									XAttribute atr2 = ere3.Attribute(XName.Get("key"));
									string val = "";
									if (atr2 != null) val = atr2.Value;
									if (val == "name") rot.name = ere3.Value;
									else if (val == "sqlBody") rot.sqlBody = ere3.Value;
									else if (val == "sqlDefinition") rot.sqlDefinition = ere3.Value;
								}
							}
						}
					} else if ((nam.ToString() == "struct-name") && (atr.Value == "db.mysql.RoutineParam")) {
						MyRoutineParam prm = new MyRoutineParam();
						MyRoutine rot = Routines[Routines.Count - 1];
						if (rot != null) {
							rot.Params.Add(prm);
							prm.id = rot.Params.Count;
							IEnumerable<XElement> els = ere.Elements();
							if (els != null) {
								foreach (XElement ele4 in els) {
									if (ele4.HasAttributes) {
										XAttribute atr3 = ele4.Attribute(XName.Get("key"));
										string val = "";
										if (atr3 != null) val = atr3.Value;
										if (val == "name") prm.name = ele4.Value;
										else if (val == "datatype") prm.datatype = ele4.Value;
										else if (val == "paramType") prm.paramType = ele4.Value;
									}
								}
							}
						}
					}
					else if ((ere.Name == "value") && (nam.ToString() == "struct-name") && (atr.Value == "db.mysql.Index")) {
						MyIndex idx = new MyIndex();
						Indexes.Add(idx);
						idx.id = Indexes.Count;
						IEnumerable<XElement> els = ere.Elements();
						if (els != null) {
							foreach (XElement ele4 in els) {
								if (ele4.HasAttributes) {
									XAttribute atr3 = ele4.Attribute(XName.Get("key"));
									string val = "";
									if (atr3 != null) val = atr3.Value;
									if (val == "name") idx.name = ele4.Value;
									else if (val == "indexKind") idx.indexKind = ele4.Value;
									else if (val == "keyBlockSize") idx.keyBlockSize = int.Parse(ele4.Value);
									else if (val == "withParser") idx.withParser = ele4.Value;
									else if (val == "deferability") idx.deferability = int.Parse(ele4.Value);
									else if (val == "comment") idx.comment = ele4.Value;
									else if (val == "indexType") idx.indexType = ele4.Value;
									else if (val == "isPrimary") idx.isPrimary = int.Parse(ele4.Value);
									else if (val == "unique") idx.unique = int.Parse(ele4.Value);
									else if (val == "oldName") idx.oldName = ele4.Value;
									else if (val == "owner") idx.owner = ele4.Value;
								}
							}
						}
					}
					else if ((ere.Name == "value") && (nam.ToString() == "struct-name") && (atr.Value == "db.mysql.IndexColumn")) {
						MyIndexColumn col = new MyIndexColumn();
						MyIndex idx = Indexes[Indexes.Count-1];
						if (idx != null)
						{
							idx.IndexColumn.Add(col);
							IEnumerable<XElement> els = ere.Elements();
							if (els != null)
							{
								foreach (XElement ele4 in els)
								{
									if (ele4.HasAttributes)
									{
										XAttribute atr3 = ele4.Attribute(XName.Get("key"));
										string val = "";
										if (atr3 != null) val = atr3.Value;
										if (val == "name") col.name = ele4.Value;
										else if (val == "columnLength") col.columnLength = int.Parse(ele4.Value);
										else if (val == "comment") col.comment = ele4.Value;
										else if (val == "descend") col.descend = int.Parse(ele4.Value);
										else if (val == "referencedColumn") col.referencedColumn = ele4.Value;
										else if (val == "owner") col.owner = ele4.Value;
									}
								}
							}
						}
					}
					else if ((ere.Name == "value")&&((nam.ToString() == "struct-name") && (atr.Value == "db.mysql.View"))) {
						MyView view = new MyView();
						Views.Add(view);
						view.id = Views.Count;
						if (view != null) {
							IEnumerable<XElement> els = ere.Elements();
							if (els != null) {
								foreach (XElement ele4 in els) {
									if (ele4.HasAttributes) {
										XAttribute atr3 = ele4.Attribute(XName.Get("key"));
										string val = "";
										if (atr3 != null) val = atr3.Value;
										if (val == "name") view.name = ele4.Value;
										else if (val == "algorithm") view.algorithm = int.Parse(ele4.Value);
										else if (val == "isReadOnly") view.isReadOnly = int.Parse(ele4.Value);
										else if (val == "oldModelSqlDefinition") view.oldModelSqlDefinition = ele4.Value;
										else if (val == "oldServerSqlDefinition") view.oldServerSqlDefinition = ele4.Value;
										else if (val == "withCheckCondition") view.withCheckCondition = int.Parse(ele4.Value);
										else if (val == "definer") view.definer = ele4.Value;
										else if (val == "sqlBody") view.sqlBody = ele4.Value;
										else if (val == "sqlDefinition") view.sqlDefinition = ele4.Value;
										else if (val == "commentedOut") view.commentedOut = int.Parse(ele4.Value);
										else if (val == "createDate") view.createDate = DateTime.Parse(ele4.Value);
										else if (val == "lastChangeDate") view.lastChangeDate = DateTime.Parse(ele4.Value);
										else if (val == "modelOnly") view.modelOnly = int.Parse(ele4.Value);
										else if (val == "oldName") view.oldName = ele4.Value;
									}
								}
							}
						}
					}
				}
			}
			if (ere.HasElements == true) { 
				List<XElement> chd = ere.Elements().ToList();
				foreach (XElement ere1 in chd)
				{
					NodeProcess2(ere1, level + 1);
				}
			}
		}
		private void GetTable(List<XElement> chd,string link) {
			MyTable tbl = new MyTable();
			_table = tbl;
			tbl.link = link;
			foreach (var ere in chd)
			{
				if (ere.HasAttributes)
				{
					XAttribute atr = ere.Attribute((XName)"key");
					if(atr!=null){
						switch (atr.Value)
						{
						case "avgRowLength": tbl.avgRowLength = ere.Value; break;
						case "tableEngine": tbl.tableEngine = ere.Value; break;
						case "name": tbl.name = ere.Value; break;
						default: break;
						}
					}
				}
			}
			Tables.Add(tbl);
			_table = null;
		}
		private void GetColumn(List<XElement> chd, string linkid) {
			MyColumn col = new MyColumn();
			col.linkid = linkid;
			foreach (var ere in chd)
			{
				if (ere.HasAttributes)
				{
					XAttribute atr = ere.Attribute((XName)"key");
					if (atr != null)
					{
						switch (atr.Value)
						{
						case "autoIncrement": col.autoIncrement = int.Parse(ere.Value); break;
						case "characterSetName": col.characterSetName = ere.Value; break;
						case "collationName": col.collationName = ere.Value; break;
						case "datatypeExplicitParams": col.datatypeExplicitParams = ere.Value; break;
						case "defaultValue": col.defaultValue = ere.Value; break;
						case "defaultValueIsNull": col.defaultValueIsNull = int.Parse(ere.Value); break;
						case "isNotNull": col.isNotNull = int.Parse(ere.Value); break;
						case "length": col.length = int.Parse(ere.Value); break;
						case "precision": col.precision = int.Parse(ere.Value); break;
						case "scale": col.scale = int.Parse(ere.Value); break;
						case "comment": col.comment = ere.Value; break;
						case "name": col.name = ere.Value; break;
						case "oldName": col.oldName = ere.Value; break;
						default: break;
						}
					}
					atr = ere.Attribute((XName)"struct-name");
					if (atr != null)
					{
						if (atr.Value == "db.SimpleDatatype")
						{
							col.SimpleDatatype = ere.Value;
						}
					}
				}
			}
			if (_table != null) {
				_table.Columns.Add(col);
			}
		}
		private void GetUserDatatype(List<XElement> chd)
		{
			MyUserDatatype typ = new MyUserDatatype();
			foreach (var ere in chd) {
				if (ere.HasAttributes) { 

					XAttribute atr = ere.Attribute((XName)"key");
					switch (atr.Value) {
					case "actualType": typ.actualType = ere.Value; break;
					case "flags": typ.flags = ere.Value; break;
					case "sqlDefinition": typ.sqlDefinition = ere.Value; break;
					case "name": typ.name = ere.Value; break;
					default: break;
					}
				}
			}
			Usertype.Add(typ);
		}
		private MySql.Data.MySqlClient.MySqlDbType GetMySqlDbType(string type){
			MySql.Data.MySqlClient.MySqlDbType ret = MySql.Data.MySqlClient.MySqlDbType.Int32;
			switch (type) {
			case "Binary":
			case "binary":
				ret = MySqlDbType.Binary;
				break;
			case "Bit":
			case "bit":
				ret = MySqlDbType.Bit;
				break;
			case "Blob":
			case "blob":
				ret = MySqlDbType.Blob;
				break;
			case "Byte":
			case "byte":
				ret = MySqlDbType.Byte;
				break;
			case "Date":
			case "date":
				ret = MySqlDbType.Date;
				break;
			case "DateTime":
			case "dateTime":
			case "Datetime":
			case "datetime":
				ret = MySqlDbType.DateTime;
				break;
			case "Decimal":
			case "decimal":
				ret = MySqlDbType.Decimal;
				break;
			case "Double":
			case "double":
				ret = MySqlDbType.Double;
				break;
			case "Enum":
			case "enum":
				ret = MySqlDbType.Enum;
				break;
			case "Float":
			case "float":
				ret = MySqlDbType.Float;
				break;
			case "Geometry":
			case "geometry":
				ret = MySqlDbType.Geometry;
				break;
			case "Guid":
			case "guid":
				ret = MySqlDbType.Guid;
				break;
			case "Int16":
			case "int16":
				ret = MySqlDbType.Int16;
				break;
			case "Int24":
			case "int24":
				ret = MySqlDbType.Int24;
				break;
			case "Int32":
			case "int32":
				ret = MySqlDbType.Int32;
				break;
			case "Int64":
			case "int64":
				ret = MySqlDbType.Int64;
				break;
			case "JSON":
			case "json":
				ret = MySqlDbType.JSON;
				break;
			case "LongBlob":
			case "longblob":
				ret = MySqlDbType.LongBlob;
				break;
			case "LongText":
			case "longtext":
				ret = MySqlDbType.LongText;
				break;
			case "MediumBlob":
			case "mediumblob":
				ret = MySqlDbType.MediumBlob;
				break;
			case "MediumText":
			case "mediumtext":
				ret = MySqlDbType.MediumText;
				break;
			case "Newdate":
			case "newdate":
				ret = MySqlDbType.Newdate;
				break;
			case "NewDecimal":
			case "newdecimal":
				ret = MySqlDbType.NewDecimal;
				break;
			case "Set":
			case "set":
				ret = MySqlDbType.Set;
				break;
			case "String":
			case "string":
				ret = MySqlDbType.String;
				break;
			case "Text":
			case "text":
				ret = MySqlDbType.Text;
				break;
			case "Time":
			case "time":
				ret = MySqlDbType.Time;
				break;
			case "Timestamp":
			case "timestamp":
				ret = MySqlDbType.Timestamp;
				break;
			case "TinyBlob":
			case "tinyblob":
				ret = MySqlDbType.TinyBlob;
				break;
			case "TinyText":
			case "tinytext":
				ret = MySqlDbType.TinyText;
				break;
			case "UByte":
			case "ubyte":
				ret = MySqlDbType.UByte;
				break;
			}
			return ret;
		}
	}
}

