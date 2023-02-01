using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NMS_API_N.DbContext;
using NMS_API_N.Extension;
using NMS_API_N.IServices;
using NMS_API_N.Model.DTO;
using NMS_API_N.Model.Entities;
using NMS_API_N.Model.FetchDTO;
using NMS_API_N.Unit_Of_Work;
using System.Linq.Expressions;

namespace NMS_API_N.Controllers
{
#nullable disable
    [Authorize]
    public class AccountSettingsController : BaseApiController
    {
        private readonly IUnitOfWork _uot;
        private readonly UserManager<User> _userManager;
        private readonly IEmailServices _emailServices;

        public AccountSettingsController(IUnitOfWork uot,
                                        UserManager<User> userManager,
                                        IEmailServices emailServices)
        {
            _uot = uot;
            _userManager = userManager;
            _emailServices = emailServices;

        }


        [HttpPost("Change-Password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto forgotPassword)
        {
            var user = await GetUserByUserName(User.GetUserName());

            var checkPassowrd = await _userManager.CheckPasswordAsync(user, forgotPassword.CurrentPassword);

            if (!checkPassowrd) return BadRequest("Your current password is not correct");

            if (forgotPassword.CurrentPassword == forgotPassword.NewPassword) return BadRequest("Current password and new password must not be same");

            var result = await _userManager.ChangePasswordAsync(user, forgotPassword.CurrentPassword, forgotPassword.NewPassword);

            if (result.Succeeded) return Ok(new SuccessMessageDto { Message = "Password Changed Successfully" });

            return BadRequest("Something Bad happened while changing password");
        }

        [HttpPost("change-username")]
        public async Task<ActionResult> ChangeUsername(ChangeUsernameDto changeUsername)
        {
            var username = await GetUserByUserName(changeUsername.Username);
          
            if (username != null) return BadRequest("Someone already taken the username");

            var getUser = await _userManager.FindByIdAsync(User.GetUserId());

            getUser.UserName = changeUsername.Username.ToLower();

            var result = await _userManager.UpdateAsync(getUser);

            if (result.Succeeded) return Ok(new SuccessMessageDto { Message = "Username changed successfully" });

            return BadRequest("Something bad happened while changing the username");
        }

        [HttpPost("change-email")]
        public async Task<ActionResult> ChangeEmail(ChangeEmailDto changeEmail)
        {
            var email = await _userManager.FindByEmailAsync(changeEmail.Email);

            if (email != null) return BadRequest("This email already taken");

            var getUser = await _userManager.FindByIdAsync(User.GetUserId());

            getUser.Email= changeEmail.Email;

            var result = await _userManager.UpdateAsync(getUser);

            if (result.Succeeded) return Ok(new SuccessMessageDto { Message = "Email changed successfully" });

            return BadRequest("Something bad happened while changing the email");

        }


        [HttpPost("SendCodeToEmail")]
        [AllowAnonymous]
        public async Task<ActionResult> SendCodeToEmail(SendCodeToEmail emailDto)
        {
            var checkEmail = await CheckEmail(emailDto.Email);

            if (checkEmail == null) return BadRequest("The email you entered is not found");

            var randomNumber = new Random();

            var emailCode = randomNumber.Next(100000, 999999);

            var emailBody = PasswordResetEmailBody(checkEmail.UserName, emailCode);

            var sendCode = _emailServices.SendMail(checkEmail.Email, "Password Reset Code", emailBody);

            if (!sendCode) return BadRequest("Something bad happened with send code to email");

            checkEmail.EmailCode = emailCode;

            if (await _uot.Complete())
            {
                var successData = new ReturnedValueDto
                {
                    Value = checkEmail.Email,
                };

                return Ok(successData);
            }

            return BadRequest("Something Wrong happened while Sending Code");

        }


        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            var checkEmail = await CheckEmail(resetPassword.Email);

            if (checkEmail == null)
            {
                var data = new ReturnedValueDto
                {
                    Message = "MAIL_NOT_FOUND"
                };

                return Ok(data);
            }

            if (int.Parse(resetPassword.Code) <= 0) return BadRequest("0 or negative value identified");

            if (checkEmail.EmailCode != int.Parse(resetPassword.Code)) return BadRequest("Wrong code identified.");

            var removeCurrentPassword = await _userManager.RemovePasswordAsync(checkEmail);

            if (!removeCurrentPassword.Succeeded) return BadRequest("Something bad happened while resetting password");

            var result = await _userManager.AddPasswordAsync(checkEmail, resetPassword.NewPassword);

            if (result.Succeeded)
            {
                checkEmail.EmailCode = 0;
                await _uot.Complete();

                return Ok(new SuccessMessageDto() { Message = "Password Changed Successfully." });
            }

            return BadRequest("Something Bad happened while resetting password.");
        }

        private string PasswordResetEmailBody(string userName, int code)
        {
            return "     <div>" +
                          "        <h2>N-M-S</h2>" +
                          "        <h3>Hello, " + userName + "</h3>" +
                          "        <p>Please enter the code below to reset your password</p>" +
                          "         <div style='background-color:gainsboro;padding:10px;'>" +
                          "            <h2>" + code + "</h2>" +
                          "         </div>" +
                          "      </div>";
        }

        private async Task<User> CheckEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        private async Task<User> GetUserByUserName(string username)
        {

            return await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == username.ToLower());
        }

    }
}
