using System;
namespace Cryptollet.Common.Navigation
{
    public interface IViewModelCompletion<T>
    {
        event EventHandler<T> Completed;
    }
}
