
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonClasses {
  public class TodoItemData {
    [Column("id")]
    [JsonProperty("id")]
    public int Id { get; set; }
    [Column("text")]
    [JsonProperty("text")]
    public string Text { get; set; } = "";
    [Column("complete")]
    [JsonProperty("complete")]
    public bool Complete { get; set; } = false;
  }
}
