﻿using CorrelationIdLibrary.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrelationIdLibrary.Helpers
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private const string _correlationIdHeader = "X-Correlation-Id";
        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, ICorrelationIdGenerator correlationIdGenerator)
        {
            var correlationId = GetCorrelationIdTrace(context, correlationIdGenerator);
            AddCorrelationIdToResponse(context, correlationId);

            await _next(context);
        }

        private static string GetCorrelationIdTrace(HttpContext context, ICorrelationIdGenerator correlationIdGenerator)
        {
            if(context.Request.Headers.TryGetValue(_correlationIdHeader, out var correlationId))
            {
                correlationIdGenerator.Set(correlationId);
                return correlationId;
            }
            return correlationIdGenerator.Get();
        }

        private static void AddCorrelationIdToResponse(HttpContext context, string correlationId)
        {
            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add(_correlationIdHeader, new []{correlationId});
                return Task.CompletedTask;
            });
        }

    }
}
