// IMvxSourceBindingFactoryExtensionHost.cs

// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
//
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

namespace MvvmCross.Binding.Bindings.Source.Construction
{
    using System.Collections.Generic;

    public interface IMvxSourceBindingFactoryExtensionHost
    {
        IList<IMvxSourceBindingFactoryExtension> Extensions { get; }
    }
}