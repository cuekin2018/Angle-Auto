using LegacyApp.Model;
using System;
using System.Collections.Generic;


namespace LegacyApp.Common
{
    public interface IUserDataAccessWrapper
    {
        void AddUser(User user);
    }

    public class UserDataAccessWrapper : IUserDataAccessWrapper
    {
        public UserDataAccessWrapper()
        {
        }

        public void AddUser(User user)
        {
            UserDataAccess.AddUser(user);
        }
    }
}
