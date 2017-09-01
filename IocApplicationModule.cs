using Autofac;
using Core;
using SampleApplication.Views;

namespace SampleApplication
{
    public class IocApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<Repository>().As<IRepository>().AsSelf().SingleInstance();

            builder.RegisterType<SampleItemValidator>().As<IModelValidator<SampleItem>>().AsSelf();

            builder.RegisterType<MainViewModel>().Keyed<IViewModel>(Constants.Navigation.MainPage);
            builder.RegisterType<ItemViewModel>().Keyed<IViewModel>(Constants.Navigation.ItemPage);
            builder.RegisterType<AuthViewModel>().Keyed<IViewModel>(Constants.Navigation.AuthPage);
            builder.RegisterType<WelcomeViewModel>().Keyed<IViewModel>(Constants.Navigation.WelcomePage);

            builder.RegisterType<MainPage>().Keyed<IView>(Constants.Navigation.MainPage);
            builder.RegisterType<WelcomePage>().Keyed<IView>(Constants.Navigation.WelcomePage);
            builder.RegisterType<AuthPage>().Keyed<IView>(Constants.Navigation.AuthPage);
            builder.RegisterType(typeof(ItemPage)).Keyed(Constants.Navigation.ItemPage, typeof(IView));
        }
    }
}