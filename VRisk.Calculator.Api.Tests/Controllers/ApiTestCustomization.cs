using AutoFixture;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;

namespace VRisk.Calculator.Api.Tests.Controllers
{
    public class ApiTestCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register<ICompositeMetadataDetailsProvider>(() => new ApiCompositeMetadataDetailsProvider());
            fixture.Inject(new ViewDataDictionary(fixture.Create<DefaultModelMetadataProvider>(), fixture.Create<ModelStateDictionary>()));
        }

        private class ApiCompositeMetadataDetailsProvider : ICompositeMetadataDetailsProvider
        {
            public void CreateBindingMetadata(BindingMetadataProviderContext context)
            {
            }

            public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
            {
            }

            public void CreateValidationMetadata(ValidationMetadataProviderContext context)
            {
            }
        }
    }
}
