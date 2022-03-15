using GG.Portafolio.Entity.Table.Blog;
using Moq;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.CRUD;
using System.Data.CRUD.MySql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.Portafolio.Shared.Test
{
    public static class MockIDataBaseConnection
    {
        private static readonly object _objectLock = new();
        private static Mock<IDataBaseConnection> _mockIDataBaseConnection;

        public static Mock<IDataBaseConnection> GetMockIConnectionDataBase()
        {
            lock (_objectLock)
            {
                if (!ConnectionDataBaseCollection.Connections.Contains("MainConnection"))
                {
                    _mockIDataBaseConnection = new();
                    MySqlStatements mySqlStatements = new();

                    _mockIDataBaseConnection.Setup(m => m.Statements.FormatColumn).Returns(mySqlStatements.FormatColumn);
                    _mockIDataBaseConnection.Setup(m => m.Statements.FormatTable).Returns(mySqlStatements.FormatTable);
                    _mockIDataBaseConnection.Setup(m => m.Statements.SelectText).Returns(mySqlStatements.SelectText);
                    _mockIDataBaseConnection.Setup(m => m.Statements.SelectWhereText).Returns(mySqlStatements.SelectWhereText);
                    _mockIDataBaseConnection.Setup(m => m.Statements.InsertText).Returns(mySqlStatements.InsertText);
                    _mockIDataBaseConnection.Setup(m => m.Statements.GetValueIdentity).Returns(mySqlStatements.GetValueIdentity);
                    _mockIDataBaseConnection.Setup(m => m.Statements.UpdateText).Returns(mySqlStatements.UpdateText);
                    _mockIDataBaseConnection.Setup(m => m.Statements.DeleteWhereText).Returns(mySqlStatements.DeleteWhereText);
                    _mockIDataBaseConnection.Setup(m => m.Statements.TruncateText).Returns(mySqlStatements.TruncateText);
                    _mockIDataBaseConnection.Setup(m => m.Statements.MaxText).Returns(mySqlStatements.MaxText);
                    _mockIDataBaseConnection.Setup(m => m.Statements.MaxWhereText).Returns(mySqlStatements.MaxWhereText);
                    _mockIDataBaseConnection.Setup(m => m.Statements.CountText).Returns(mySqlStatements.CountText);
                    _mockIDataBaseConnection.Setup(m => m.Statements.CountWhereText).Returns(mySqlStatements.CountWhereText);
                    _mockIDataBaseConnection.Setup(m => m.Statements.CreateTable).Returns(mySqlStatements.CreateTable);

                    _mockIDataBaseConnection.Setup(m => m.ExecuteReader(It.IsAny<string>())).Returns(new DataSet());
                    _mockIDataBaseConnection.Setup(m => m.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<IEnumerable<IDataParameter>>())).Returns(0);
                    _mockIDataBaseConnection.Setup(m => m.ExecuteNonQuery(It.IsAny<string>())).Returns(0);
                    _mockIDataBaseConnection.Setup(m => m.ExecuteScalar(It.IsAny<string>())).Returns(0);
                    _mockIDataBaseConnection.Setup(m => m.ExecuteScalar(It.IsAny<string>(), It.IsAny<IEnumerable<IDataParameter>>())).Returns(0);

                    _mockIDataBaseConnection.Setup(m => m.Save(It.IsAny<ICollection<IDataParameter>>(), It.IsAny<DataMemberCRUD>(), It.IsAny<string>(), It.IsAny<object>()))
                        .Callback<ICollection<IDataParameter>, DataMemberCRUD, string, object>((p, d, n, v) => p.Add(new MySqlParameter(n, v) { Value = v }));

                    ConnectionDataBaseCollection.Connections.Add("MainConnection", _mockIDataBaseConnection.Object);
                }
            }

            return _mockIDataBaseConnection;
        }

        public static List<TEntity> GetListEmpty<TEntity>(IEnumerable<IDataParameter> parameters, params string[] parameterNames) where TEntity : IEntity
        {
            if (parameters.Any(x => parameterNames.Any(y => x.ParameterName.Contains(y))))
            {
                var param = parameters.Where(x => parameterNames.Any(y => x.ParameterName.Contains(y)));

                if (param.Any(x => string.IsNullOrWhiteSpace(x.Value?.ToString())))
                {
                    return new List<TEntity>();
                }
            }
            
            return null;
        }

        public static void SetBlogEntity()
        {
            _mockIDataBaseConnection
               .Setup(m => m.ExecuteReader<Blog>(It.IsAny<string>(), It.IsAny<IEnumerable<IDataParameter>>(), It.IsAny<IEnumerable<PropertyType>>()))
              .Returns<string, IEnumerable<IDataParameter>, IEnumerable<PropertyType>>((s, p, pr) =>
              {
                  var empty = GetListEmpty<Blog>(p, nameof(Blog.Title), nameof(Blog.Id));

                  return empty ?? new List<Blog>()
                      {
                           new Blog
                           {
                               Create = DateTime.Now.Ticks,
                               Dsc = "Test",
                               Edit = DateTime.Now.Ticks,
                               Id = Guid.NewGuid().ToString(),
                               IsActive = true,
                               Title = "Test",
                               User_Id = Guid.NewGuid().ToString(),
                           },
                      };
              });
        }

        public static void SetBlogDetailEntity()
        {
            _mockIDataBaseConnection
               .Setup(m => m.ExecuteReader<BlogDetail>(It.IsAny<string>(), It.IsAny<IEnumerable<IDataParameter>>(), It.IsAny<IEnumerable<PropertyType>>()))
              .Returns<string, IEnumerable<IDataParameter>, IEnumerable<PropertyType>>((s, p, pro) =>
              {
                  var empty = GetListEmpty<BlogDetail>(p, nameof(BlogDetail.Blog_Id));

                  return empty ?? new List<BlogDetail>
                  {
                        new BlogDetail{ Blog_Id = Guid.NewGuid().ToString(), Detail = "Test"}
                  };
              });
        }

        public static void SetUserEntity()
        {
            _mockIDataBaseConnection
                .Setup(m => m.ExecuteReader<User>(It.IsAny<string>(), It.IsAny<IEnumerable<IDataParameter>>(), It.IsAny<IEnumerable<PropertyType>>()))
                .Returns<string, IEnumerable<IDataParameter>, IEnumerable<PropertyType>>((s, p, pro) =>
                {
                    var empty = GetListEmpty<User>(p, nameof(User.Email),
                        nameof(User.Id), nameof(User.Name));

                    return empty ?? new List<User>
                    {
                        new User {Email = "tests@gmail.com", Id = Guid.NewGuid().ToString(), Name ="Test" }
                    };
                });
        }

        public static void SetBlogCommentsEntity()
        {
            _mockIDataBaseConnection
                .Setup(m => m.ExecuteReader<BlogComments>(It.IsAny<string>(), It.IsAny<IEnumerable<IDataParameter>>(), It.IsAny<IEnumerable<PropertyType>>()))
                .Returns(new List<BlogComments>());
        }
    }
}
