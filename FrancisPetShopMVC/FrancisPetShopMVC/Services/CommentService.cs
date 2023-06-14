using FirstMvc.Data;

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
    }

}
