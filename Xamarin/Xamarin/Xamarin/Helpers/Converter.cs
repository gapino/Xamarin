using Lands.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Model;

namespace Xamarin.Helpers
{
    public static class Converter
    {
        public static UserLocal ToUserLocal(User user)
        {
            return new UserLocal
            {
                Email = user.Email,
                FirstName = user.FirstName,
                ImagePath = user.ImagePath,
                LastName = user.LastName,
                Telephone = user.Telephone,
                UserId = user.UserId,
            };
        }
    }
}
