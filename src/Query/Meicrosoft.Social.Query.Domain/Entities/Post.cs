namespace Meicrosoft.Social.Query.Domain.Entities;

[Table("Post")]
public class Post
{
    [Key]
    public Guid Id { get; set; }
    public string Author { get; set; }
    public DateTime DatePosted { get; set; }
    public string Message { get; set; }
    public int Likes { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
}
