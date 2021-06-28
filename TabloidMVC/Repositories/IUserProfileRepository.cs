﻿using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetByEmail(string email);
        void CreateUser(UserProfile user);
        UserProfile GetUserById(int id);
    }
}