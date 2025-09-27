using Firebase.Auth;
using Firebase.Auth.Providers;
using SlimWaist.Languages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimWaist.Helpers
{
    public class FirebaseAuthHelper
    {
        private FirebaseAuthClient firebaseAuthClient = new FirebaseAuthClient(new FirebaseAuthConfig
        {
            ApiKey = "AIzaSyDIYYeYYG5nE-AE6ZYrfIabHl7qMkM3E7o",
            AuthDomain = "calfit-users-2025.firebaseapp.com",
            Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
        });

        public async Task<bool> SignUpWithEmail(string email, string password)
        {
            try
            {
                var result = await firebaseAuthClient.CreateUserWithEmailAndPasswordAsync(email, password);
                return result != null;
            }
            catch (FirebaseAuthException ex)
            {
                return false;
            }
        }

        public async Task<bool> SignInWithEmail(string email, string password)
        {
            try
            {
                var result = await firebaseAuthClient.SignInWithEmailAndPasswordAsync(email, password);
                return result != null;
            }
            catch (FirebaseAuthException ex)
            {
                return false;
            }
        }
    }
}
