using Firebase.Database;
using Firebase.Database.Offline;
using Firebase.Database.Query;
using Newtonsoft.Json;
using SlimWaist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SlimWaist.Helpers
{
    public class FirebaseDbHelper
    {
        private FirebaseClient firebaseUsersClient=new FirebaseClient("https://calfit-users-2025-default-rtdb.firebaseio.com/");

        private FirebaseClient firebaseStorageClient=new FirebaseClient("https://calfit-storage-2025-default-rtdb.firebaseio.com/");

        public async Task<bool> InsertInCalfitStorageAsync<T>(string userKey,T data) where T : class
        {
            try
            {
                //var ff = Preferences.Get("firebaseObjectkey", "");
                var f= await firebaseStorageClient.Child("User").Child(userKey).Child(typeof(T).Name).PostAsync(JsonConvert.SerializeObject(data));
                //Preferences.Set("firebaseObjectkey", f.Key);

                //var ff = Preferences.Get("firebaseObjectkey","");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> InsertUserInCalfitUsersAsync<T>(T data) where T : class
        {
            try
            {
                //var ff = Preferences.Get("firebaseObjectkey", "");
                
                await firebaseUsersClient.Child(typeof(T).Name).PostAsync(JsonConvert.SerializeObject(data));
                
                //Preferences.Set("firebaseObjectkey", f.Key);
                //var ff = Preferences.Get("firebaseObjectkey","");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<string> InsertUserInCalfitStorageAndReturnKeyAsync<T>(T data) where T : class
        {
            try
            {
                //var ff = Preferences.Get("firebaseObjectkey", "");

                var firebaseObject = await firebaseStorageClient.Child(typeof(T).Name).PostAsync(JsonConvert.SerializeObject(data));

                return firebaseObject.Key;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task GetMembershipAsync(string email)         {
            try
            {
                //var membership = 
                //    await firebaseClient.Child("Membership")
                //    .OrderBy("Email")
                //    .EqualTo(email)
                //    .LimitToFirst(1)
                //    .OnceSingleAsync<Membership>();

                //var membership = await firebaseClient.Child("Membership").AsObservable<Membership>().Where(x => x.Object.Email == email).FirstOrDefaultAsync();

                List<Membership> memberships = new List<Membership>();

                //firebaseClient.Child("Membership").AsObservable<Membership>().Subscribe(async (item) =>
                //{
                //    if (item.Object!=null)
                //    {
                //        memberships.Add(item.Object);
                //    }
                //});


            }
            catch (Exception ex)
            {

            }
        }

    }
}
