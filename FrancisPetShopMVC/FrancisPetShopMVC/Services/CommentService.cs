using FirstMvc.Data;
using FrancisPetShopMVC.Data.Entities;
using FrancisPetShopMVC.Services.Interfaces;

namespace FrancisPetShopMVC.Services
{
    public class CommentService : ICommentService
    {
        private readonly MyContext context;
        public CommentService(MyContext context)
        {
            this.context = context;
        }
        public IEnumerable<string> GetComments(int id)
        {
            var comments = context.Comments.Where(x => x.AnimalId == id).ToList();
            List<string> commentTexts = new List<string>();
            foreach (var comment in comments)
            {
                commentTexts.Add(comment.CommentText);
            }
            return commentTexts;
        }
        public Comment AddComment(int animalId, string commentText)
        {
            if (string.IsNullOrEmpty(commentText))
            {
                throw new ArgumentException("Comment text cannot be empty");
            }

            var animal = context.Animals.FirstOrDefault(a => a.AnimalId == animalId);
            if (animal == null)
            {
                throw new ArgumentException($"Animal with ID {animalId} not found");
            }

            var newComment = new Comment
            {
                CommentText = commentText,
                AnimalId = animalId, // Set the foreign key value
                                     // Set other properties of the comment as needed
            };

            context.Comments.Add(newComment);
            context.SaveChanges();

            return newComment;
        }
    }

}
