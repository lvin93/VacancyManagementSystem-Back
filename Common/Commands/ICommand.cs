﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Commands
{
    public interface ICommand<out TResponse> : IRequest<TResponse> { }
    public interface ICommand : IRequest<Unit> { }
}
