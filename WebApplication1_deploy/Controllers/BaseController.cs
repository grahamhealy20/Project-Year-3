using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    /* SOURCE: http://jameschambers.com/2014/06/day-14-bootstrap-alerts-and-mvc-framework-tempdata/ */
    public class BaseController : Controller
    {
        // GET: Base
        public void Success(string message, bool dismissable)
        {
            AddAlert(Helpers.AlertStyles.Success, message, dismissable);
        }

        public void Information(string message, bool dismissable = false)
        {
            AddAlert(Helpers.AlertStyles.Information, message, dismissable);
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(Helpers.AlertStyles.Warning, message, dismissable);
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(Helpers.AlertStyles.Danger, message, dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Helpers.Alert.TempDataKey)
                ? (List<Helpers.Alert>)TempData[Helpers.Alert.TempDataKey]
                : new List<Helpers.Alert>();

            alerts.Add(new Helpers.Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            TempData[Helpers.Alert.TempDataKey] = alerts;
        }
    }
}