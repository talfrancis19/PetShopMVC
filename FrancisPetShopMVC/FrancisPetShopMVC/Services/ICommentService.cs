namespace FrancisPetShopMVC.Services
{
    public interface ICommentService
    {
        public IEnumerable<string> GetComments(int id);
       
    }

}
