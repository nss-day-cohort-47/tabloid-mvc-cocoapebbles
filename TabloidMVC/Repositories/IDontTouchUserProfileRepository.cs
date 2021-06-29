using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IDontTouchUserProfileRepository
    {
        UserProfile GetByEmail(string email);
        void CreateUser(UserProfile user);
    }
}