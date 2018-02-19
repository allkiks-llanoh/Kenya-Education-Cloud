﻿using KEC.Curatiom.Web.UI.Helpers;
using KEC.Curatiom.Web.UI.Models;
using Microsoft.Graph;
using Resources;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KEC.Curatiom.Web.UI.Controllers
{
    public class UsersController : Controller
    {
        UsersService usersService = new UsersService();
        // Get all users.
        public async Task<ActionResult> GetUsers()
        {
            ResultsViewModel results = new ResultsViewModel();
            try
            {

                // Initialize the GraphServiceClient.
                GraphServiceClient graphClient = SDKHelper.GetAuthenticatedClient();

                // Get users.
                results.Items = await usersService.GetUsers(graphClient);
            }
            catch (ServiceException se)
            {
                if (se.Error.Message == Resource.Error_AuthChallengeNeeded) return new EmptyResult();
                return RedirectToAction("Index", "Error", new { message = string.Format(Resource.Error_Message, Request.RawUrl, se.Error.Code, se.Error.Message) });
            }
            return View("Users", results);
        }

        // Get the current user's profile.
        public async Task<ActionResult> GetMe()
        {
            ResultsViewModel results = new ResultsViewModel();
            try
            {

                // Initialize the GraphServiceClient.
                GraphServiceClient graphClient = SDKHelper.GetAuthenticatedClient();

                // Get the current user's profile.
                results.Items = await usersService.GetMe(graphClient);
            }
            catch (ServiceException se)
            {
                if (se.Error.Message == Resource.Error_AuthChallengeNeeded) return new EmptyResult();
                return RedirectToAction("Index", "Error", new { message = string.Format(Resource.Error_Message, Request.RawUrl, se.Error.Code, se.Error.Message) });
            }
            return View("Users", results);
        }

        // Get the current user's manager.
        public async Task<ActionResult> GetMyManager()
        {
            ResultsViewModel results = new ResultsViewModel();
            try
            {

                // Initialize the GraphServiceClient.
                GraphServiceClient graphClient = SDKHelper.GetAuthenticatedClient();

                // Get the current user's manager.
                results.Items = await usersService.GetMyManager(graphClient);
            }

            // Throws exception if manager is null, with Request_ResourceNotFound code.
            catch (ServiceException se)
            {
                if (se.Error.Message == Resource.Error_AuthChallengeNeeded) return new EmptyResult();
                return RedirectToAction("Index", "Error", new { message = string.Format(Resource.Error_Message, Request.RawUrl, se.Error.Code, se.Error.Message) });
            }
            return View("Users", results);
        }

        // Get the current user's photo. 
        public async Task<ActionResult> GetMyPhoto()
        {
            ResultsViewModel results = new ResultsViewModel(false);
            try
            {

                // Initialize the GraphServiceClient.
                GraphServiceClient graphClient = SDKHelper.GetAuthenticatedClient();

                // Get my photo.
                results.Items = await usersService.GetMyPhoto(graphClient);
            }

            // Throws exception if photo is null, with itemNotFound code.
            catch (ServiceException se)
            {
                if (se.Error.Message == Resource.Error_AuthChallengeNeeded) return new EmptyResult();
                return RedirectToAction("Index", "Error", new { message = string.Format(Resource.Error_Message, Request.RawUrl, se.Error.Code, se.Error.Message) });
            }
            return View("Users", results);
        }

        // Create a new user in the signed-in user's tenant.
        // This snippet requires an admin work account. 
        public async Task<ActionResult> CreateUser()
        {
            ResultsViewModel results = new ResultsViewModel();
            try
            {

                // Initialize the GraphServiceClient.
                GraphServiceClient graphClient = SDKHelper.GetAuthenticatedClient();

                // Add the user.
                results.Items = await usersService.CreateUser(graphClient);
            }
            catch (ServiceException se)
            {
                if (se.Error.Message == Resource.Error_AuthChallengeNeeded) return new EmptyResult();
                return RedirectToAction("Index", "Error", new { message = string.Format(Resource.Error_Message, Request.RawUrl, se.Error.Code, se.Error.Message) });
            }
            return View("Users", results);
        }

        // Get a specified user.
        public async Task<ActionResult> GetUser(string id)
        {
            ResultsViewModel results = new ResultsViewModel();
            try
            {

                // Initialize the GraphServiceClient.
                GraphServiceClient graphClient = SDKHelper.GetAuthenticatedClient();

                // Get the user.
                results.Items = await usersService.GetUser(graphClient, id);
            }
            catch (ServiceException se)
            {
                if (se.Error.Message == Resource.Error_AuthChallengeNeeded) return new EmptyResult();
                return RedirectToAction("Index", "Error", new { message = string.Format(Resource.Error_Message, Request.RawUrl, se.Error.Code, se.Error.Message) });
            }
            return View("Users", results);
        }

        // Get a specified user's photo.
        public async Task<ActionResult> GetUserPhoto(string id)
        {
            ResultsViewModel results = new ResultsViewModel(false);
            try
            {

                // Initialize the GraphServiceClient.
                GraphServiceClient graphClient = SDKHelper.GetAuthenticatedClient();

                // Get the user's photo.
                results.Items = await usersService.GetUserPhoto(graphClient, id);
            }

            // Throws an exception when requesting the photo for unlicensed users (such as those created by this sample), with message "The requested user '<user-name>' is invalid."
            catch (ServiceException se)
            {
                if (se.Error.Message == Resource.Error_AuthChallengeNeeded) return new EmptyResult();
                return RedirectToAction("Index", "Error", new { message = string.Format(Resource.Error_Message, Request.RawUrl, se.Error.Code, se.Error.Message) });
            }
            return View("Users", results);
        }

        // Get the direct reports of a specified user.
        public async Task<ActionResult> GetDirectReports(string id)
        {
            ResultsViewModel results = new ResultsViewModel();
            try
            {

                // Initialize the GraphServiceClient.
                GraphServiceClient graphClient = SDKHelper.GetAuthenticatedClient();

                // Get user's direct reports.
                results.Items = await usersService.GetDirectReports(graphClient, id);
            }
            catch (ServiceException se)
            {
                if (se.Error.Message == Resource.Error_AuthChallengeNeeded) return new EmptyResult();
                return RedirectToAction("Index", "Error", new { message = string.Format(Resource.Error_Message, Request.RawUrl, se.Error.Code, se.Error.Message) });
            }
            return View("Users", results);
        }

        // Update a user.
        // This snippet changes the user's display name. 
        // This snippet requires an admin work account. 
        public async Task<ActionResult> UpdateUser(string id, string name)
        {
            ResultsViewModel results = new ResultsViewModel(false);
            try
            {

                // Initialize the GraphServiceClient.
                GraphServiceClient graphClient = SDKHelper.GetAuthenticatedClient();

                // Change user display name.
                results.Items = await usersService.UpdateUser(graphClient, id, name);
            }
            catch (ServiceException se)
            {
                if (se.Error.Message == Resource.Error_AuthChallengeNeeded) return new EmptyResult();
                return RedirectToAction("Index", "Error", new { message = string.Format(Resource.Error_Message, Request.RawUrl, se.Error.Code, se.Error.Message) });
            }
            return View("Users", results);
        }

        // Delete a user. Warning: This operation cannot be undone.
        // This snippet requires an admin work account. 
        public async Task<ActionResult> DeleteUser(string id)
        {
            ResultsViewModel results = new ResultsViewModel(false);
            try
            {

                // Initialize the GraphServiceClient.
                GraphServiceClient graphClient = SDKHelper.GetAuthenticatedClient();

                // Make sure that the current user is not selected.
                results.Items = await usersService.DeleteUser(graphClient, id);
            }
            catch (ServiceException se)
            {
                if (se.Error.Message == Resource.Error_AuthChallengeNeeded) return new EmptyResult();
                return RedirectToAction("Index", "Error", new { message = string.Format(Resource.Error_Message, Request.RawUrl, se.Error.Code, se.Error.Message) });
            }
            return View("Users", results);
        }
    }
}