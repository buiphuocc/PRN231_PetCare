﻿using Application;
using Application.Services;
using Azure;
using Infrastructure;
using System.Text.Json;
namespace PRN231_PetCare.Middlewares
{
    public class ConfirmationTokenMiddleware
    {
        private readonly RequestDelegate _next;


        public ConfirmationTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // create area service temporary
            using (var scope = context.RequestServices.CreateScope())
            {
                // Get the IUnitOfWork from the temporary service scope
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var token = context.Request.Query["token"];

                if (!string.IsNullOrEmpty(token))
                {
                    var user = await unitOfWork.UserRepository.GetUserByConfirmationToken(token);

                    if (user != null && !user.isActivated)
                    {
                        // confirm
                        user.isActivated = true;
                        user.EmailConfirmToken = null;
                        await unitOfWork.SaveChangeAsync();
                        //// Send registration success email
                        //var emailSent = await Utils.SendMail.SendRegistrationSuccessEmail(user.Email);
                        //if (!emailSent)
                        //{
                        //    // Log or handle the error as needed
                        //    System.Console.WriteLine($"Failed to send registration success email to {user.Email}");
                        //}
                        //context.Response.Redirect($"https://zodiacjewerly.azurewebsites.net");
                        context.Response.Redirect("https://zodiacgems.vercel.app/login");
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}