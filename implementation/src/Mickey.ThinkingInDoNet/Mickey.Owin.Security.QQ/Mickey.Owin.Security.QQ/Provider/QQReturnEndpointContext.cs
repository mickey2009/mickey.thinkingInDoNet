﻿using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mickey.Owin.Security.QQ.Provider
{
    public class QQReturnEndpointContext : ReturnEndpointContext
    {
        public QQReturnEndpointContext(
            IOwinContext context,
            AuthenticationTicket ticket)
            : base(context, ticket)
        {

        }
    }
}
