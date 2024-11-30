using Microsoft.AspNetCore.Mvc;

namespace LogisticaSolutions.Helper
{
    public static class AlertHelper
    {
        public static void SetAlert(Controller controller, string type, string message)
        {
            controller.TempData["AlertType"] = type; // success or error
            controller.TempData["AlertMessage"] = message;
        }
    }
}
