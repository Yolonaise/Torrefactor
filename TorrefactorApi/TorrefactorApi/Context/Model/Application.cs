using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TorrefactorApi.Context
{
  public class Application
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string ApiKey { get; set; }
    public string ApplicationName { get; set; }
    public string ApplicationDescription { get; set; }
    public string ApplicationSecret { get; set; }
  }
}
