using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2E4Win
{
	public partial class MyColumn
	{
		public MyColumn() {
			id = 0;
			table = 0;
			autoIncrement = 0;
			linkid = "";
		}
		public int id { get; set; }
		public int table  { get; set; } 
		public int autoIncrement { get; set; }
		public string characterSetName { get; set; }
		public string collationName { get; set; }
		public string datatypeExplicitParams { get; set; }
		public string defaultValue { get; set; }
		public int defaultValueIsNull { get; set; }
		public int isNotNull { get; set; }
		public int length { get; set; }
		public int precision { get; set; }
		public int scale { get; set; }
		public string comment { get; set; }
		public string name { get; set; }
		public string oldName { get; set; }
		public string SimpleDatatype { get; set; }
		public string linkid { get; set; }
	}
	public partial class MyTable
	{
		public MyTable() {
			Columns = new List<MyColumn>();
			Indexs = new List<MyIndex>();
		}
		public object this[string index]{
			get{
				object rtn = null;
				switch (index)
				{
					case "avgRowLength": rtn = avgRowLength; break;
					case "tableEngine": rtn = tableEngine; break;
					case "name": rtn = name; break;
					case "link": rtn = link; break;
				}
				return rtn;
			}
			set{
				switch (index)
				{
					case "avgRowLength": avgRowLength = (string)value; break;
					case "tableEngine": tableEngine = (string)value; break;
					case "name": name = (string)value; break;
					case "link": link = (string)value; break;
				}
			}
		}
		public string link { get; set; }
		public int id { get; set; }
		public string avgRowLength { get; set; }
		public string tableEngine { get; set; }
		public string name { get; set; }
		public List<MyColumn> Columns { get; set; }
		public List<MyIndex> Indexs { get; set; }
		
	}
	public partial class MyRoutineParam {
		public int id { get; set; }
		public string linkid { get; set; }
		public string name { get; set; }
		public string datatype { get; set; }
		public string paramType { get; set; }
		
	}
	public partial class MyRoutine
	{
		public MyRoutine() {
			Params = new List<MyRoutineParam>();
		}
		public int id { get; set; }
		public string name { get; set; }
		public string sqlBody { get; set; }
		public List<MyRoutineParam> Params { get; set; }
	}
	public partial class MyUserDatatype
	{
		public int id { get; set; }
		public string flags { get; set; }
		public string sqlDefinition { get; set; }
		public string actualType { get; set; }
		public string name { get; set; }
	}
	public partial class MyIndex
	{
		public MyIndex() {
			IndexColumn = new List<MyIndexColumn>();
		}
		public int id { get; set; }
		public string indexKind { get; set; }
		public int keyBlockSize { get; set; }
		public string withParser { get; set; }
		public int deferability { get; set; }
		public string comment { get; set; }
		public string indexType { get; set; }
		public int isPrimary { get; set; }
		public string name { get; set; }
		public int unique { get; set; }
		public string oldName { get; set; }
		public string owner { get; set; }
		public List<MyIndexColumn> IndexColumn { get; set; }
	}
	public partial class MyIndexColumn
	{
		public int id { get; set; }
		public int Indexid { get; set; }
		public int columnLength { get; set; }
		public string comment { get; set; }
		public int descend { get; set; }
		public string referencedColumn { get; set; }
		public string name { get; set; }
		public string owner { get; set; }
	}
	public partial class MyView : MyTable
	{
		public MyView():base(){
		}
		public int algorithm { get; set; }
		public int isReadOnly { get; set; }
		public string oldModelSqlDefinition { get; set; }
		public string oldServerSqlDefinition { get; set; }
		public int withCheckCondition { get; set; }
		public string definer { get; set; }
		public string sqlBody { get; set; }
		public int commentedOut { get; set; }
		public DateTime createDate { get; set; }
		public DateTime lastChangeDate { get; set; }
		public int modelOnly { get; set; }
		public string oldName { get; set; }
	}
}
