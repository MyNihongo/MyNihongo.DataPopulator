using LinqToDB.Data;

namespace MyNihongo.DataPopulator.Databases;

internal abstract class DatabaseBase : DataConnection
{
	protected DatabaseBase(string connectionString)
		: base("Microsoft.Data.SqlClient", connectionString)
	{
	}
}