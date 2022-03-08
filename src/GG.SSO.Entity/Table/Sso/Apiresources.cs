using System;
using System.Data.CRUD;
using MySql.Data.MySqlClient;

namespace GG.SSO.Entity.Table.Sso
{
	[DataClassCRUD("sso","apiresources")]
	public class ApiResources : Entity<ApiResources>
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

		[DataMemberCRUD("AllowedAccessTokenSigningAlgorithms", MySqlDbType.VarChar, 100)]
		public string AllowedAccessTokenSigningAlgorithms { get; set; }

		[DataMemberCRUD("ShowInDiscoveryDocument", MySqlDbType.Bit, 1)]
		public bool ShowInDiscoveryDocument { get; set; }

		[DataMemberCRUD("RequireResourceIndicator", MySqlDbType.Bit, 1)]
		public bool RequireResourceIndicator { get; set; }

		[DataMemberCRUD("Created", MySqlDbType.DateTime, 0)]
		public DateTime Created { get; set; }

		[DataMemberCRUD("Updated", MySqlDbType.DateTime, 0)]
		public DateTime? Updated { get; set; }

		[DataMemberCRUD("LastAccessed", MySqlDbType.DateTime, 0)]
		public DateTime? LastAccessed { get; set; }

		[DataMemberCRUD("IsEditable", MySqlDbType.Bit, 1)]
		public bool IsEditable { get; set; }

		[DataMemberCRUD("IsDeleted", MySqlDbType.Bit, 1)]
		public bool IsDeleted { get; set; }

		public ApiResources()
		{}

		public ApiResources(int id,bool enabled,string name,string displayName,string description,string allowedAccessTokenSigningAlgorithms,bool showInDiscoveryDocument,bool requireResourceIndicator,DateTime created,DateTime? updated,DateTime? lastAccessed,bool isEditable, bool isDeleted)
		{
			Id = id;
			Enabled = enabled;
			Name = name;
			DisplayName = displayName;
			Description = description;
			AllowedAccessTokenSigningAlgorithms = allowedAccessTokenSigningAlgorithms;
			ShowInDiscoveryDocument = showInDiscoveryDocument;
			RequireResourceIndicator = requireResourceIndicator;
			Created = created;
			Updated = updated;
			LastAccessed = lastAccessed;
			IsEditable = isEditable;
			IsDeleted = isDeleted;
		}
	}
}
