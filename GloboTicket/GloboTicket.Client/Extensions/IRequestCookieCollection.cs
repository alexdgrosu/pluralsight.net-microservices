using System;
using GloboTicket.Web.Models;
using Microsoft.AspNetCore.Http;

namespace GloboTicket.Client.Extensions
{
    public static class RequestCookieCollection
    {
        public static Guid GetCurrentBasketId(this IRequestCookieCollection cookies, Settings settings)
        {
            if (!Guid.TryParse(cookies[settings.BasketIdCookieName], out Guid baskeId))
            {
                return Guid.Empty;
            }

            return baskeId;
        }
    }
}
