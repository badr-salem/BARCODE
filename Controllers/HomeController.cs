using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GenerateQrCode.Models;
using System.IO;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Mail;
using System.Net;

namespace GenerateQrCode.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexEN()
        {
            return View();
        }



        [HttpPost]
        public IActionResult ConvertToQR(string InputText)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator oQRCodeGenerator = new QRCodeGenerator();
                QRCodeData oQRCodeData = oQRCodeGenerator.CreateQrCode(InputText, QRCodeGenerator.ECCLevel.Q);
                QRCode oQRCode = new QRCode(oQRCodeData);

                using (Bitmap oBitmap = oQRCode.GetGraphic(20))
                {
                    oBitmap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCode = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
            return View("Index");
        }

        [HttpPost]
        public IActionResult ContactUs(SendMailDto sendMailDto)
        {
            if (!ModelState.IsValid) return View("Index");

            try
            {
                MailMessage mail = new MailMessage();
                // you need to enter your mail address
                mail.From = new MailAddress("Your_Sender_Email@gmail.com");

                //To Email Address - your need to enter your to email address
                mail.To.Add("Your_Receiver_Email@gmail.com");

                mail.Subject = sendMailDto.Subject;



                mail.IsBodyHtml = true;

                string content = "Name : " + sendMailDto.Name;
                content += "<br/> Email : " + sendMailDto.Email;
                content += "<br/> Phone Number : " + sendMailDto.PhoneNumber;
                content += "<br/> Subject : " + sendMailDto.Subject;
                content += "<br/> Message : " + sendMailDto.Message;

                mail.Body = content;


                //create SMTP instant

                //you need to pass mail server address and you can also specify the port number if you required
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

                //Create nerwork credential and you need to give from email address and password
                NetworkCredential networkCredential = new NetworkCredential("Your_Sender_Email@gmail.com", "Pass Of Sender Email");
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                smtpClient.Port = 587; // this is default port number - you can also change this
                smtpClient.EnableSsl = true; // if ssl required you need to enable it
                smtpClient.Send(mail);

                ViewBag.Message = "شكرا لتواصلك معنا ";
                

                // now i need to create the from 
                ModelState.Clear();

            }
            catch (Exception ex)
            {
                //If any error occured it will show
                ViewBag.Message = ex.Message.ToString();
            }
            return View("Index");
        }


        [HttpPost]
        public IActionResult ConvertToQREN(string InputText)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator oQRCodeGenerator = new QRCodeGenerator();
                QRCodeData oQRCodeData = oQRCodeGenerator.CreateQrCode(InputText, QRCodeGenerator.ECCLevel.Q);
                QRCode oQRCode = new QRCode(oQRCodeData);

                using (Bitmap oBitmap = oQRCode.GetGraphic(20))
                {
                    oBitmap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCode = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
            return View("IndexEN");
        }

        [HttpPost]
        public IActionResult ContactUsEN(SendMailDto sendMailDto)
        {
            if (!ModelState.IsValid) return View("IndexEN");

            try
            {
                MailMessage mail = new MailMessage();
                // you need to enter your mail address
                mail.From = new MailAddress("Your_Sender_Email@gmail.com");

                //To Email Address - your need to enter your to email address
                mail.To.Add("Your_Receiver_Email@gmail.com");

                mail.Subject = sendMailDto.Subject;



                mail.IsBodyHtml = true;

                string content = "Name : " + sendMailDto.Name;
                content += "<br/> Email : " + sendMailDto.Email;
                content += "<br/> Phone Number : " + sendMailDto.PhoneNumber;
                content += "<br/> Subject : " + sendMailDto.Subject;
                content += "<br/> Message : " + sendMailDto.Message;

                mail.Body = content;


                //create SMTP instant

                //you need to pass mail server address and you can also specify the port number if you required
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

                //Create nerwork credential and you need to give from email address and password
                NetworkCredential networkCredential = new NetworkCredential("Your_Sender_Email@gmail.com", "Pass Of Sender Email");
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                smtpClient.Port = 587; // this is default port number - you can also change this
                smtpClient.EnableSsl = true; // if ssl required you need to enable it
                smtpClient.Send(mail);

               
                ViewBag.MessageEN = "Thank You For Contact With Us";

                // now i need to create the from 
                ModelState.Clear();

            }
            catch (Exception ex)
            {
                //If any error occured it will show
                ViewBag.Message = ex.Message.ToString();
            }
            return View("IndexEN");
        }






    }
}
