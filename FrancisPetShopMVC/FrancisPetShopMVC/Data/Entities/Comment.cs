using System.ComponentModel.DataAnnotations.Schema;

namespace FrancisPetShopMVC.Data.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int AnimalId { get; set; }
        public Animal AnimalItem { get; set; } = null!;
        public required string CommentText { get; set; }
    }
}
