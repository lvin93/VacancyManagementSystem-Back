﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Commands
{
    public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
     where TRequest : ICommand<TResponse>
    { }

    public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest, Unit>
        where TRequest : ICommand
    { }
}
