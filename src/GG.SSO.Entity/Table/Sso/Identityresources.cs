using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GGPuntoYComa.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","identityresources")]
	public class IdentityResources : Entity<IdentityResources>
	{
		[DataMemberCRUD("Id", MySqlDbType.Int32, 10, true, true)]
		public int Id { get; set; }

		[DataMemberCRUD("Enabled", MySqlDbType.Bit, 1)]
		public bool Enabled { get; set; }

		[DataMemberCRUD("Name", MySqlDbType.VarChar, 200)]
		public string Name { get; set; }

		[DataMemberCRUD("DisplayName", MySqlDbType.VarChar, 200)]
		public string DisplayName { get; set; }

		[DataMemberCRUD("Description", MySqlDbType.VarChar, 1000)]
		public string Description { get; set; }

		[DataMemberCRUD("Required", MySqlDbType.Bit, 1)]
		public bool Required { get; set; }

		[DataMemberCRUD("Emphasize", MySqlDbType.Bit, 1)]
		public bool Emphasize { get; set; }

		[DataMemberCRUD("ShowInDiscoveryDocument", MySqlDbType.Bit, 1)]
		public bool ShowInDiscoveryDocument { get; set; }

		[DataMemberCRUD("Created", MySqlDbType.DateTime, 0)]
		public DateTime Created { get; set; }

		[DataMemberCRUD("Updated", MySqlDbType.DateTime, 0)]
		public DateTime? Updated { get; set; }

		[DataMemberCRUD("IsEditable", MySqlDbType.Bit, 1)]
		public bool IsEditable { get; set; }

		public IdentityResources()
		{}

		public IdentityResources(int id,bool enabled,string name,string displayName,string description,bool required,bool emphasize,bool showInDiscoveryDocument,DateTime created,DateTime? updated,bool isEditable)
		{
			Id = id;
			Enabled = enabled;
			Name = name;
			DisplayName = displayName;
			Description = description;
			Required = required;
			Emphasize = emphasize;
			ShowInDiscoveryDocument = showInDiscoveryDocument;
			Created = created;
			Updated = updated;
			IsEditable = isEditable;
		}
	}
}
