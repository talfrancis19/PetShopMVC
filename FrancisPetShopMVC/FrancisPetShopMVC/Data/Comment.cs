using System.ComponentModel.DataAnnotations.Schema;
using FrancisPetShopMVC.Data.Entities;

namespace FrancisPetShopMVC.Data
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int AnimalId { get; set; }
        public Animal AnimalItem { get; set; } = null!;
        public required string CommentText { get; set; }
    }
}
