﻿using InventoryAPI.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace InventoryAPI.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger<LoggingBehaviour<TRequest>> _logger;
    private readonly ICurrentUserService _currentUserService;
    // private readonly IIdentityService _identityService;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest>> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
        // _identityService = identityService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId ?? string.Empty;
        string userName = string.Empty;

        if (!string.IsNullOrEmpty(userId))
        {
            // userName = await _identityService.GetUserNameAsync(userId);
        }

        _logger.LogInformation("InventoryAPI Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, userId, userName, request);
    }
}