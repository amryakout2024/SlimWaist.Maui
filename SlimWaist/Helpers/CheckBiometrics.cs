using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimWaist.Helpers
{
    public class CheckBiometrics
    {
        /*
        install plugin.maui.biometric
        in mauiprogram add 
        using Plugin.Maui.Biometric;
        builder.Services.AddSingleton<IBiometric>(BiometricAuthenticationService.Default);
        make dependency injection in the login page
        IBiometric biometric
        private readonly IBiometric _biometric;
        _biometric = biometric;
        */
        public async void CheckFingerPrint()
        {
            //var result = await _biometric.AuthenticateAsync
            //    (
            //       new AuthenticationRequest()
            //       {
            //           Title = "please authenticate",
            //           NegativeText = "finger print",
            //           Description = "put your finger"
            //       }, CancellationToken.None
            //    );
            //_biometric.GetAuthenticationStatusAsync().Wait();

            //if (result.Status == BiometricResponseStatus.Success)
            //{
            //    //to do
            //}

        }
    }
}
