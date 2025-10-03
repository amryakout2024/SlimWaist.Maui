using Firebase.Database;
using Firebase.Database.Offline;
using Firebase.Database.Query;
using Newtonsoft.Json;
using SlimWaist.Models;
using SlimWaist.Models.FirebaseDto;
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
        private string dbUsers = "https://calfit-users-2025-default-rtdb.firebaseio.com/";

        private string dbStorage = "https://calfit-storage-2025-default-rtdb.firebaseio.com/";

        private FirebaseClient firebaseUsersClient = new FirebaseClient("https://calfit-users-2025-default-rtdb.firebaseio.com/");

        private FirebaseClient firebaseStorageClient = new FirebaseClient("https://calfit-storage-2025-default-rtdb.firebaseio.com/");

        //----Firebase * storage----//

        public async Task<bool> InsertAsync<T>(string userKey, T data) where T : class
        {
            try
            {
                //var ff = Preferences.Get("firebaseObjectkey", "");
                var f = await firebaseStorageClient.Child("User").Child(userKey).Child(typeof(T).Name).PostAsync(JsonConvert.SerializeObject(data));
                //Preferences.Set("firebaseObjectkey", f.Key);

                //var ff = Preferences.Get("firebaseObjectkey","");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<T>> GetAsync<T>(string userKey) where T : class
        {
            try
            {
                using (HttpClient client = new())
                {

                    HttpResponseMessage response = await client.GetAsync($"{dbStorage}{typeof(User).Name}/{userKey}/{typeof(T).Name}.json");
                    response.EnsureSuccessStatusCode();
                    List<T> list = new List<T>();

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBodyContent = await response.Content.ReadAsStringAsync();

                        var objects = JsonConvert.DeserializeObject<Dictionary<string, T>>(responseBodyContent).ToList();

                        foreach (var obj in objects)
                        {
                            list.Add(obj.Value);
                        }
                    }
                    return list;

                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("", ex.Message, "ok");
                return new List<T>();
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

        //----Firebase * users----//

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

        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                using (HttpClient client = new())
                {
                    HttpResponseMessage response = await client.GetAsync($"{dbUsers}{typeof(User).Name}.json");

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBodyContent = await response.Content.ReadAsStringAsync();

                        var objects = JsonConvert.DeserializeObject<Dictionary<string, User>>(responseBodyContent);

                        List<User> users = new List<User>();

                        foreach (var user in objects)
                        {
                            users.Add(user.Value);
                        }
                        return users;
                        // Process the users as needed
                    }
                    else
                    {
                        return new List<User>();
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                return new List<User>();
            }
        }
    }
}
