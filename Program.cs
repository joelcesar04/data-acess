using BaltaDataAcessAuthor.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BaltaDataAcessAuthor
{
  class Program
  {
    static void Main(string[] args)
    {
      const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True";

      using (var connection = new SqlConnection(connectionString))
      {
        // CreateAuthor(connection);
        UpdateAuthor(connection);
        ListAuthor(connection);
      }
    }

    static void ListAuthor(SqlConnection connection)
    {
      var authors = connection.Query<Author>("SELECT [Name], [Title], [Email], [Url] FROM [Author]");
      foreach (var item in authors)
      {
        Console.WriteLine(@$"Nome: {item.Name} 
          Título: {item.Title}
          E-mail: {item.Email}
          Url: {item.Url}
          ");
      }
    }

    static void CreateAuthor(SqlConnection connection)
    {
      var newAuthor = new Author
      {
        Id = Guid.NewGuid(),
        Name = "Joel Andrade",
        Title = "Estudante",
        Image = "https://baltaio.blob.core.windows.net/static/images/authors/joelandrade.jpg",
        Bio = "N/A",
        Url = "joel-andrade",
        Email = "hello@balta.io",
        Type = 1
      };

      var insertSql = @"
        INSERT INTO 
          [Author]
        VALUES (
          @Id,
          @Name,
          @Title,
          @Image,
          @Bio,
          @Url,
          @Email,
          @Type
        )";

      var rows = connection.Execute(insertSql, new
      {
        newAuthor.Id,
        newAuthor.Name,
        newAuthor.Title,
        newAuthor.Image,
        newAuthor.Bio,
        newAuthor.Url,
        newAuthor.Email,
        newAuthor.Type
      });
      Console.WriteLine($"{rows} linhas foram afetadas.");
    }
  
    static void UpdateAuthor(SqlConnection connection)
    {
      var udpateSql = "UPDATE [Author] SET [Email] = @Email WHERE [Id] = @Id";

      var rows = connection.Execute(udpateSql, new
      {
        Email = "tiago@balta.io",
        Id = "5cec296f-e717-9a0a-437b-bf5900000000"
      });
      Console.WriteLine($"{rows} linhas foram afetadas");
    }
  }
}