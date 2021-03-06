﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Authentication.Twitter;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.Internal;

namespace Microsoft.Framework.DependencyInjection
{
    /// <summary>
    /// Extension methods for using <see cref="TwitterAuthenticationMiddleware"/>
    /// </summary>
    public static class TwitterAuthenticationExtensions
    {
        public static IServiceCollection ConfigureTwitterAuthentication([NotNull] this IServiceCollection services, [NotNull] Action<TwitterAuthenticationOptions> configure)
        {
            return services.ConfigureTwitterAuthentication(configure, optionsName: "");
        }

        public static IServiceCollection ConfigureTwitterAuthentication([NotNull] this IServiceCollection services, [NotNull] Action<TwitterAuthenticationOptions> configure, string optionsName)
        {
            return services.Configure(configure, optionsName);
        }

        public static IServiceCollection ConfigureTwitterAuthentication([NotNull] this IServiceCollection services, [NotNull] IConfiguration config)
        {
            return services.ConfigureTwitterAuthentication(config, optionsName: "");
        }

        public static IServiceCollection ConfigureTwitterAuthentication([NotNull] this IServiceCollection services, [NotNull] IConfiguration config, string optionsName)
        {
            return services.Configure<TwitterAuthenticationOptions>(config, optionsName);
        }
    }
}
