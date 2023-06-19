using FrancisPetShopMVC.Data.Entities;

namespace FrancisPetShopMVC.Services.Interfaces
{
    public interface ICommentService
    {
        public IEnumerable<string> GetComments(int id);

        public Comment AddComment(int animalId, string commentText);

    }

}
