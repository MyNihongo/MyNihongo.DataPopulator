using LinqToDB;
using MyNihongo.DataPopulator.Database.Models.Grammar;

namespace MyNihongo.DataPopulator.Databases;

internal sealed class GrammarDatabase : DatabaseBase
{
	public GrammarDatabase(string connectionString)
		: base(connectionString)
	{
	}

	public ITable<GrammarRuleMasterDataDatabaseRecord> RuleMasterData => GetTable<GrammarRuleMasterDataDatabaseRecord>();

	public ITable<GrammarRuleContentDatabaseRecord> RuleContent => GetTable<GrammarRuleContentDatabaseRecord>();

	public ITable<GrammarRuleSearchByKanjiDatabaseRecord> RuleSearchByKanji => GetTable<GrammarRuleSearchByKanjiDatabaseRecord>();

	public ITable<GrammarRuleRuleSearchByRomajiDatabaseRecord> RuleSearchByRomaji => GetTable<GrammarRuleRuleSearchByRomajiDatabaseRecord>();

	public ITable<GrammarRuleSearchByLanguageDatabaseRecord> RuleSearchByLanguage => GetTable<GrammarRuleSearchByLanguageDatabaseRecord>();
}
