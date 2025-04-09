﻿namespace Common
{
    public interface IRequestHandler<TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken = default);
    }
}
