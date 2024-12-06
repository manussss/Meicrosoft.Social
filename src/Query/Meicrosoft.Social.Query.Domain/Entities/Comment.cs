namespace Meicrosoft.Social.Query.Domain.Entities;

[Table("Comment")]
public class Comment
{
    [Key]
    public Guid Id { get; set; }
    public string Username { get; set; }
    public DateTime CommentDate { get; set; }
    public string CommentText { get; set; }
    public bool Edited { get; set; }
    public Guid PostId { get; set; }
    [JsonIgnore]
    public virtual Post Post { get; set; }
}
