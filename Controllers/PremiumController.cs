using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using ExcelDataReader;
using PremiumCalculator.Helpers;
using PremiumCalculator.Models;

namespace PremiumCalculator.Controllers
{
    public class PremiumController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(new List<PremiumModel>());
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            var users = new List<PremiumModel>();
            var fileName = System.Web.HttpContext.Current.Server.MapPath("./App_Data/rating1.xlsx");

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read()) //Each row of the file
                    {
                        try
                        {


                            int alphaValue = 0, betaValue = 0;

                            var alpha = reader.GetValue(0)?.ToString();
                            bool isAlphaNull = string.IsNullOrWhiteSpace(alpha);

                            var beta = reader.GetValue(1)?.ToString();
                            bool isBetaNull = string.IsNullOrWhiteSpace(beta);

                            if (isAlphaNull && isBetaNull)
                                throw new Exception("Alpha & Beta values cannot be null");

                            if (!isAlphaNull) alphaValue = Convert.ToInt32(alpha);

                            if (!isBetaNull) betaValue = Convert.ToInt32(beta);

                            double agePoints = 0, heightPoints = 0;

                            try
                            {
                                agePoints = PremiumHelper.GetAgePoints(Convert.ToInt32(reader.GetValue(2).ToString()));
                                heightPoints = PremiumHelper.GetHeightPoints(Convert.ToInt32(reader.GetValue(3).ToString()));

                            }
                            catch (Exception)
                            {
                                // ignored
                            }

                            var ratingModel = new PremiumModel
                            {
                                Alpha = alphaValue,
                                Beta = betaValue,
                                AgePoints = agePoints,
                                HeightPoints = heightPoints
                            };

                            ratingModel.Rating = PremiumHelper.GetRating(ratingModel);

                            users.Add(ratingModel);
                        }


                        catch (Exception)
                        {
                            ViewBag.Error = "Supplied input is not valid.";
                            return View("Error");
                        }

                    }

                }
            }
            return View(users);
        }
    }
}