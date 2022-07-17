﻿namespace ElevateOTT.Application.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    #region Private Fields

    private readonly ILogger<TRequest> _logger;

    #endregion Private Fields

    #region Public Constructors

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogError(ex, "ElevateOTT Request: Unhandled Exception for Request {RequestName} {RequestPath}", requestName, request);
            var errorString = ex.InnerException?.ToString() ?? ex.Message;
            throw new Exception(ex.ToString());
        }
    }

    #endregion Public Methods
}